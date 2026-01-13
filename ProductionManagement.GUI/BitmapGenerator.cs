using System.IO;
using System.Text;

namespace ProductionManagement.GUI
{
    public static class BitmapGenerator
    {
        public static void GenerateBudgetChart(string filePath, List<decimal> history)
        {
            int width = 400;
            int height = 200;

            byte[] pixels = new byte[width * height * 3];
            for (int i = 0; i < pixels.Length; i++) pixels[i] = 255;

            if (history.Count > 0)
            {
                decimal max = history.Max();
                decimal min = history.Min();
                if (max == min) max = min + 1;

                int barWidth = width / Math.Max(1, history.Count);

                for (int i = 0; i < history.Count; i++)
                {
                    decimal val = history[history.Count - 1 - i];
                    int barHeight = (int)((val - min) / (max - min) * (height - 10));

                    for (int x = i * barWidth; x < (i + 1) * barWidth && x < width; x++)
                    {
                        for (int y = 0; y < barHeight; y++)
                        {
                            int index = ((height - 1 - y) * width + x) * 3;
                            if (index >= 0 && index < pixels.Length - 3)
                            {
                                pixels[index] = 0;
                                pixels[index + 1] = 0;
                                pixels[index + 2] = 255;
                            }
                        }
                    }
                }
            }

            SaveBmp(filePath, width, height, pixels);
        }

        internal static void GenerateBudgetChart(string chartPath, decimal[] decimals)
        {
            throw new NotImplementedException();
        }

        private static void SaveBmp(string path, int width, int height, byte[] data)
        {
            using FileStream fs = new(path, FileMode.Create);
            using BinaryWriter bw = new(fs);
            int fileSize = 54 + data.Length;

            bw.Write(Encoding.ASCII.GetBytes("BM"));
            bw.Write(fileSize);
            bw.Write(0);
            bw.Write(0);
            bw.Write(54);

            bw.Write(40);
            bw.Write(width);
            bw.Write(height);
            bw.Write((short)1);
            bw.Write((short)24);
            bw.Write(0);
            bw.Write(data.Length);
            bw.Write(0);
            bw.Write(0);
            bw.Write(0);
            bw.Write(0);

            bw.Write(data);
        }
    }
}