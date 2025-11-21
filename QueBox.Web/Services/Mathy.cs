// Mathy.cs
using System.Net.Http;
using System.Threading.Tasks;
using System.Drawing;
using QueBox.Services;

using System.IO;

namespace QueBox.Services
{
    public class Mathy : IMathy
    {
        public static List<object> LinspaceList(double start, double stop, int num, bool endpoint = true)
        {
            if (num < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(num), "Number of samples must be non-negative.");
            }

            if (num == 0)
            {
                return new List<object>();
            }

            List<object> result = new List<object>(num);

            if (num == 1)
            {
                result.Add(start);
                return result;
            }

            double step;
            if (endpoint)
            {
                step = (stop - start) / (num - 1);
            }
            else
            {
                step = (stop - start) / num;
            }

            for (int i = 0; i < num; i++)
            {
                result.Add(start + i * step);
            }

            // Adjust the last element if endpoint is true to ensure it's exactly 'stop'
            // due to potential floating-point inaccuracies
            if (endpoint && num > 1)
            {
                result[num - 1] = stop;
            }

            return result;
        }

        public static Tuple<List<object>, List<object>> CreateMeshGrid(List<object> xValues, List<object> yValues)
        {
            int numX = xValues.Count;
            int numY = yValues.Count;

            // Initialize the 2D lists for X and Y grids
            List<object> X = new List<object>();
            List<object> Y = new List<object>();

            // Populate the X grid
            for (int i = 0; i < numY; i++)
            {
                List<object> rowX = new List<object>();
                for (int j = 0; j < numX; j++)
                {
                    rowX.Add(xValues[j]);
                }
                X.Add(rowX);
            }

            // Populate the Y grid
            for (int i = 0; i < numY; i++)
            {
                List<object> rowY = new List<object>();
                for (int j = 0; j < numX; j++)
                {
                    rowY.Add(yValues[i]);
                }
                Y.Add(rowY);
            }

            return Tuple.Create(X, Y);
        }

        public static List<object> CreateListOfListOfOnesCapacity(int count)
        {

            List<object> onesList = new List<object>(count); // Initialize with capacity
            for (int i = 0; i < count; i++)
            {
                onesList.Add(1);
            }

            List<object> onesListList = new List<object>(count); // Initialize with capacity
            for (int i = 0; i < count; i++)
            {
                onesListList.Add(onesList);
            }

            return onesListList;
        }

        public static List<object> CreateListOfListOfZerosCapacity(int count)
        {

            List<object> onesList = new List<object>(count); // Initialize with capacity
            for (int i = 0; i < count; i++)
            {
                onesList.Add(0);
            }

            List<object> onesListList = new List<object>(count); // Initialize with capacity
            for (int i = 0; i < count; i++)
            {
                onesListList.Add(onesList);
            }

            return onesListList;
        }


        public async Task<List<Object>> GetGrayscalePixelsFromUrl(string imageUrl)
        {
            // 1. Download the image bytes
            byte[] imageBytes = await DownloadImageBytes(imageUrl);

            // 2. Convert bytes to Bitmap
            using (Bitmap originalBitmap = BytesToBitmap(imageBytes))
            {
                // 3. Convert to grayscale
                using (Bitmap grayscaleBitmap = ConvertToGrayscale(originalBitmap))
                {
                    // 4. Extract pixel data as a list
                    return GetGrayscalePixelList(grayscaleBitmap);
                }
            }
        }

        private async Task<byte[]> DownloadImageBytes(string imageUrl)
        {
            using (HttpClient client = new HttpClient())
            {
                return await client.GetByteArrayAsync(imageUrl);
            }
        }

        private Bitmap BytesToBitmap(byte[] imageBytes)
        {
            using (MemoryStream ms = new MemoryStream(imageBytes))
            {
                return new Bitmap(Image.FromStream(ms));
            }
        }

        private Bitmap ConvertToGrayscale(Bitmap originalBitmap)
        {
            Bitmap grayscaleBitmap = new Bitmap(originalBitmap.Width, originalBitmap.Height);

            for (int x = 0; x < originalBitmap.Width; x++)
            {
                for (int y = 0; y < originalBitmap.Height; y++)
                {
                    Color pixel = originalBitmap.GetPixel(x, y);
                    int grayValue = (pixel.R + pixel.G + pixel.B) / 3;
                    Color grayPixel = Color.FromArgb(grayValue, grayValue, grayValue);
                    grayscaleBitmap.SetPixel(x, y, grayPixel);
                }
            }
            return grayscaleBitmap;
        }

        private List<Object> GetGrayscalePixelList(Bitmap grayscaleBitmap)
        {
            List<Object> pixelList = new List<Object>();


            for (int y = 0; y < grayscaleBitmap.Height; y++) // Iterate row by row
            {

                List<Object> pixelListy = new List<Object>();
                for (int x = 0; x < grayscaleBitmap.Width; x++) // Iterate column by column
                {
                    Color pixel = grayscaleBitmap.GetPixel(x, y);
                    pixelListy.Add(pixel.R); // R, G, and B will be the same for grayscale
                }
                pixelListy.Add(pixelListy);
            }
            return pixelList;
        }
    }
}