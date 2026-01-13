using production_management.Models;
using ProductionManagement.GUI.Models;
using ProductionManagement.GUI.Services;
using System.Collections;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows;

namespace ProductionManagement.GUI
{
    public partial class MainWindow : Window
    {
        private readonly Stack<decimal> _budgetHistory = new();
        private readonly Random _rng = new();

        public MainWindow()
        {
            InitializeComponent();
        }

        private async void StartSimulation_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!TryGetSimulationConfig(out var config)) return;

                LogBox.Clear();
                UpdateLog("--- SIMULATION STARTED ---");
                _budgetHistory.Clear();
                _budgetHistory.Push(config.Budget);

                var company = new Company(config.CompanyName, config.Budget, config.Materials);


                string basePath = AppDomain.CurrentDomain.BaseDirectory;
                await InitializeCompanyResourcesAsync(company, config, basePath);

                await RunSimulationLoopAsync(company, config);

                GenerateFinalReport(company, basePath);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Critical Error: {ex.Message}", "Simulation Failed", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async Task RunSimulationLoopAsync(Company company, SimulationConfig config)
        {
            int[][] shifts = GenerateJaggedSchedule(company.Employees.Count);

            await Task.Run(() =>
            {
                for (int day = 1; day <= config.Days; day++)
                {
                    ProcessOneDay(company, day, config, shifts);

                    Dispatcher.Invoke(() => _budgetHistory.Push(company.Budget));

                    System.Threading.Thread.Sleep(200);
                }
            });
        }

        private void ProcessOneDay(Company company, int day, SimulationConfig config, int[][] shifts)
        {
            decimal currentMarketPrice = _rng.Next(10, 50);
            bool shouldSell = currentMarketPrice > 30 || company.ProductStock > 100;

            var activeMachines = company.Machines.Where(m => _rng.NextDouble() > config.BreakdownChance).ToList();
            int brokenCount = company.Machines.Count - activeMachines.Count;

            decimal potentialProduction = CalculateProduction(activeMachines);
            int producedItems = (company.RawMaterials < potentialProduction) ? company.RawMaterials : (int)potentialProduction;

            company.ConsumeMaterials(producedItems);
            company.AddProductsToStock(producedItems);
            decimal income = 0;
            if (shouldSell && company.ProductStock > 0)
            {
                int itemsSold = company.SellProducts(200);
                income = itemsSold * currentMarketPrice;
            }

            decimal costs = company.DailyEmployeeCost() + company.DailyMachineCost() + config.FixedCost;
            company.ApplyDay(income, costs);

            if (day % 7 == 0) company.ApplyDay(1000, 0);

            foreach (var emp in company.Employees) emp.WorkDay();

            string marketStatus = shouldSell ? $"SELLING @ {currentMarketPrice:C}" : $"HOLDING @ {currentMarketPrice:C}";
            string logMsg = $"Day {day}: Budget {company.Budget:C} | Mat: {company.RawMaterials} | Stock: {company.ProductStock} | {marketStatus}";

            if (brokenCount > 0) logMsg += $" [BROKEN: {brokenCount}]";

            UpdateLog(logMsg);
        }

        private decimal CalculateProduction(List<ProductionMachine> machines)
        {
            decimal capacity = machines.Sum(m => m.ProductionCapacity);
            double efficiency = 0.8 + (_rng.NextDouble() * 0.4);
            return capacity * (decimal)efficiency;
        }

        private async Task InitializeCompanyResourcesAsync(Company company, SimulationConfig config, string basePath)
        {
            for (int i = 1; i <= config.MachinesCount; i++)
                company.Machines.Add(new ProductionMachine($"Machine #{i}", 1000, 200));

            UpdateLog($"[INIT] Installed {config.MachinesCount} machines. Stock: {config.Materials}.");

            await Task.Run(() =>
            {
                string dataPath = Path.Combine(basePath, "Data");
                try
                {
                    if (Directory.Exists(dataPath))
                    {
                        var emps = EmployeeGenerator.GenerateFromJson(config.EmployeeCount,
                            Path.Combine(dataPath, "Names.json"),
                            Path.Combine(dataPath, "LastNames.json"), 200, 400);
                        company.Employees.AddRange(emps);

                        UpdateLog($"[HR] Hired {emps.Count} employees.");
                    }
                }
                catch { UpdateLog("[WARN] Could not load employee names files."); }
            });
        }

        private void GenerateFinalReport(Company company, string path)
        {
            UpdateLog("\n=== FINAL REPORT ===");
            UpdateLog($"Final Budget: {company.Budget:C}");
            UpdateLog($"Remaining Stock: {company.RawMaterials}");
            UpdateLog("\n[DEBUG] Object Dump:");
            foreach (var prop in company.GetType().GetProperties())
            {
                var val = prop.GetValue(company);

                if (val is ICollection list && val is not string)
                    UpdateLog($" {prop.Name}: {list.Count} items");
                else
                    UpdateLog($" {prop.Name}: {val}");
            }

            try
            {
                string chartPath = Path.Combine(path, "chart.bmp");
                BitmapGenerator.GenerateBudgetChart(chartPath, _budgetHistory.ToArray());
                UpdateLog($"Chart saved to: {chartPath}");

                FileService.SaveCompany(company, Path.Combine(path, "save.json"));
            }
            catch (Exception ex) { UpdateLog($"[ERROR] Saving failed: {ex.Message}"); }
        }

        private void UpdateLog(string message)
        {
            Dispatcher.Invoke(() =>
            {
                LogBox.AppendText(message + "\n");
                LogBox.ScrollToEnd();
            });
        }

        private int[][] GenerateJaggedSchedule(int count)
        {
            int[][] schedule = new int[count][];
            for (int i = 0; i < count; i++)
            {
                int days = _rng.Next(3, 7);
                schedule[i] = new int[days];
                for (int j = 0; j < days; j++) schedule[i][j] = _rng.Next(0, 7);
            }
            return schedule;
        }

        private struct SimulationConfig
        {
            public string CompanyName;
            public decimal Budget;
            public int Days;
            public int EmployeeCount;
            public int MachinesCount;
            public decimal FixedCost;
            public int Materials;
            public double BreakdownChance;
        }

        private bool TryGetSimulationConfig(out SimulationConfig config)
        {
            config = new SimulationConfig();

            if (!Regex.IsMatch(CompanyNameBox.Text, @"^[a-zA-Z0-9\s]+$"))
            {
                MessageBox.Show("Invalid Company Name!", "Validation Error");
                return false;
            }

            try
            {
                config.CompanyName = CompanyNameBox.Text;
                config.Budget = decimal.Parse(BudgetBox.Text);
                config.Days = int.Parse(DaysBox.Text);
                config.EmployeeCount = int.Parse(EmployeesBox.Text);
                config.MachinesCount = int.Parse(MachinesCountBox.Text);
                config.FixedCost = decimal.Parse(FixedCostBox.Text);
                config.Materials = int.Parse(MaterialsBox.Text);
                config.BreakdownChance = double.Parse(BreakdownChanceBox.Text.Replace(".", ","));
                return true;
            }
            catch
            {
                MessageBox.Show("Please check all numeric fields.", "Input Error");
                return false;
            }
        }
    }
}