using System;

namespace SystemZarzadzaniaProdukcja
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== SYSTEM ZARZĄDZANIA PRODUKCJĄ ===");
            Console.Write("Podaj nazwę firmy: ");
            string nazwaFirmy = Console.ReadLine();

            int liczbaPracownikow = PobierzLiczbeOdUzytkownika("Podaj liczbę pracowników: ");
            string[] pracownicy = DodajPracownikow(liczbaPracownikow);

            Console.WriteLine("\nLista pracowników:");
            foreach (string p in pracownicy)
                Console.WriteLine($"- {p}");

            int liczbaMaszyn = PobierzLiczbeOdUzytkownika("\nPodaj liczbę maszyn: ");
            int[][] moceMaszyn = DodajMaszyny(liczbaMaszyn);

            Console.WriteLine("\nMoce maszyn:");
            for (int i = 0; i < moceMaszyn.Length; i++)
            {
                Console.Write($"Maszyna {i + 1}: ");
                for (int j = 0; j < moceMaszyn[i].Length; j++)
                    Console.Write(moceMaszyn[i][j] + " ");
                Console.WriteLine();
            }

            int suma = 0;
            double srednia = 0;
            ObliczProdukcje(moceMaszyn, ref suma, out srednia);

            Console.WriteLine($"\nŁączna produkcja: {suma}");
            Console.WriteLine($"Średnia moc produkcyjna: {srednia:F2}");

            Console.WriteLine("\nNajsilniejsza maszyna:");
            WyswietlNajlepszaMaszyne(moceMaszyn);

            Console.WriteLine("\nNaciśnij dowolny klawisz, aby zakończyć...");
            Console.ReadKey();
        }

        static int PobierzLiczbeOdUzytkownika(string prompt)
        {
            int liczba;
            Console.Write(prompt);
            while (!int.TryParse(Console.ReadLine(), out liczba))
            {
                Console.WriteLine("Błędna wartość. Proszę podać poprawną liczbę.");
                Console.Write(prompt);
            }
            return liczba;
        }

        static string[] DodajPracownikow(int liczba)
        {
            string[] tab = new string[liczba];
            for (int i = 0; i < liczba; i++)
            {
                Console.Write($"Podaj imię pracownika {i + 1}: ");
                tab[i] = Console.ReadLine();
            }
            return tab;
        }

        static int[][] DodajMaszyny(int liczbaMaszyn)
        {
            int[][] maszyny = new int[liczbaMaszyn][];
            for (int i = 0; i < liczbaMaszyn; i++)
            {
                int param = PobierzLiczbeOdUzytkownika($"Podaj liczbę parametrów mocy dla maszyny {i + 1}: ");
                maszyny[i] = new int[param];

                for (int j = 0; j < param; j++)
                {
                    maszyny[i][j] = PobierzLiczbeOdUzytkownika($"Podaj moc parametru {j + 1}: ");
                }
            }
            return maszyny;
        }

        static void ObliczProdukcje(int[][] maszyny, ref int suma, out double srednia)
        {
            int licznik = 0;
            suma = 0;

            foreach (var m in maszyny)
            {
                foreach (var wartosc in m)
                {
                    suma += wartosc;
                    licznik++;
                }
            }

            srednia = (licznik > 0) ? (double)suma / licznik : 0;
        }

        static void WyswietlNajlepszaMaszyne(int[][] maszyny)
        {
            if (maszyny.Length == 0)
            {
                Console.WriteLine("Brak maszyn do wyświetlenia.");
                return;
            }

            int maxSuma = int.MinValue;
            int indeks = -1;

            for (int i = 0; i < maszyny.Length; i++)
            {
                int suma = Suma(maszyny[i]);
                if (suma > maxSuma)
                {
                    maxSuma = suma;
                    indeks = i;
                }
            }

            Console.Write($"Maszyna {indeks + 1} (łączna moc: {maxSuma}): ");
            foreach (var x in maszyny[indeks])
                Console.Write(x + " ");
            Console.WriteLine();
        }

        static int Suma(int[] tab)
        {
            int s = 0;
            foreach (var x in tab)
                s += x;
            return s;
        }

        static double Suma(double[] tab)
        {
            double s = 0;
            foreach (var x in tab)
                s += x;
            return s;
        }
    }
}
