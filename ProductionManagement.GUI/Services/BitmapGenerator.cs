using System;
using System.Drawing;
using System.Drawing.Imaging;

namespace ProductionManagement.GUI.Services
{
    public static class BitmapGenerator
    {
        public static void GenerateBudgetChart(string path, decimal[] data)
        {
            int width = Math.Max(400, 60 + (data?.Length ?? 0) * 40);
            int height = 240;
            using var bmp = new Bitmap(width, height);
            using var g = Graphics.FromImage(bmp);
            g.Clear(Color.White);

            if (data == null || data.Length == 0)
            {
                g.DrawString("No data", SystemFonts.DefaultFont, Brushes.Black, 10, 10);
                bmp.Save(path, ImageFormat.Bmp);
                return;
            }

            decimal max = 1;
            foreach (var v in data) if (v > max) max = v;
            int margin = 20;
            int chartW = width - margin * 2;
            int chartH = height - margin * 2;
            int barGap = 8;
            int barWidth = Math.Max(8, (chartW / data.Length) - barGap);

            using var smallFont = new Font(SystemFonts.DefaultFont.FontFamily, 8f);
            for (int i = 0; i < data.Length; i++)
            {
                decimal val = data[i];
                int x = margin + i * (barWidth + barGap);
                int barH = (int)((val / max) * chartH);
                int y = margin + (chartH - barH);
                g.FillRectangle(Brushes.SteelBlue, x, y, barWidth, barH);
                g.DrawRectangle(Pens.Black, x, y, barWidth, barH);
                g.DrawString(val.ToString("0.##"), smallFont, Brushes.Black, x, Math.Max(2, y - 14));
            }

            bmp.Save(path, ImageFormat.Bmp);
        }
    }
}