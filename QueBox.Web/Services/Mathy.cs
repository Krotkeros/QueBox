// Mathy.cs
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.IO;

// USAMOS SixLabors.ImageSharp en lugar de System.Drawing para compatibilidad con Blazor WASM
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing; 

namespace QueBox.Services
{
    // El constructor debe ser público si se usa para inyección de dependencias
    public class Mathy : IMathy
    {
        // --- Métodos Estáticos (Funciones Matemáticas) ---

        public static List<object> LinspaceList(double start, double stop, int num, bool endpoint = true)
        {
            if (num < 0) throw new ArgumentOutOfRangeException(nameof(num), "Number of samples must be non-negative.");
            if (num == 0) return new List<object>();

            List<object> result = new List<object>(num);
            if (num == 1) { result.Add(start); return result; }

            double step = endpoint ? (stop - start) / (num - 1) : (stop - start) / num;

            for (int i = 0; i < num; i++)
            {
                result.Add(start + i * step);
            }
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

            List<object> X = new List<object>();
            List<object> Y = new List<object>();

            for (int i = 0; i < numY; i++)
            {
                List<object> rowX = new List<object>();
                for (int j = 0; j < numX; j++)
                {
                    rowX.Add(xValues[j]);
                }
                X.Add(rowX);
            }

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

        public static List<object> CreateListOfListOfValCapacity(int count, float val)
        {
            List<object> onesList = new List<object>(count);
            for (int i = 0; i < count; i++) { onesList.Add(val); }
            List<object> onesListList = new List<object>(count);
            for (int i = 0; i < count; i++) { onesListList.Add(onesList); }
            return onesListList;
        }

        public static List<object> CreateListOfListOfZerosCapacity(int count)
        {
            List<object> zerosList = new List<object>(count); 
            for (int i = 0; i < count; i++) { zerosList.Add(0.0); }
            List<object> zerosListList = new List<object>(count); 
            for (int i = 0; i < count; i++) { zerosListList.Add(zerosList); }
            return zerosListList;
        }


        // --- Implementación de IMathy (Procesamiento de Imágenes con ImageSharp) ---

        public async Task<List<Object>> GetGrayscalePixelsFromUrl(string imageUrl)
        {
            try
            {
                // 1. Download the image bytes
                byte[] imageBytes = await DownloadImageBytes(imageUrl);

                if (imageBytes.Length == 0)
                {
                    // Si no se pudo descargar, devolver una matriz vacía
                    return new List<Object>();
                }

                // 2. Cargar y procesar la imagen con ImageSharp
                // Usamos MemoryStream porque LoadAsync requiere un Stream o un path.
                using (var ms = new MemoryStream(imageBytes))
                using (var image = await Image.LoadAsync<Rgba32>(ms))
                {
                    // Convertir a escala de grises
                    image.Mutate(x => x.Grayscale());
                    
                    // 3. Extraer pixel data como una matriz 2D
                    return GetGrayscalePixelListImageSharp(image);
                }
            }
            catch (Exception ex)
            {
                // Este catch ahora captura los errores de ImageSharp o descarga
                Console.WriteLine($"Error general al procesar la imagen: {ex.Message}");
                // Devolver una matriz vacía si hay un fallo
                return new List<Object>(); 
            }
        }

        private async Task<byte[]> DownloadImageBytes(string imageUrl)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    return await client.GetByteArrayAsync(imageUrl);
                }
                catch (HttpRequestException)
                {
                    Console.WriteLine($"Error al descargar la imagen desde {imageUrl}.");
                    return new byte[0]; 
                }
            }
        }

        // Nuevo método para extraer píxeles de SixLabors.ImageSharp
        private List<Object> GetGrayscalePixelListImageSharp(Image<Rgba32> image)
        {
            List<Object> pixelMatrix = new List<Object>(); 

            // Los píxeles de ImageSharp se acceden por [x, y]
            // Plotly espera [fila (Y), columna (X)]
            for (int y = 0; y < image.Height; y++) // Iteración por filas (eje Z/Y)
            {
                List<Object> rowPixels = new List<Object>();
                for (int x = 0; x < image.Width; x++) // Iteración por columnas (eje X)
                {
                    Rgba32 pixel = image[x, y];
                    
                    // Convertir el valor de R (que es igual a G y B en escala de grises) a double
                    // y normalizarlo (opcional, pero Plotly funciona bien con valores 0-255 o 0-1)
                    // Usaremos 0-255, Plotly lo interpreta como intensidad.
                    rowPixels.Add((double)pixel.R); 
                }
                
                // Agregamos la fila completa a la matriz
                pixelMatrix.Add(rowPixels); 
            }
            return pixelMatrix;
        }

        // Los métodos obsoletos de System.Drawing se eliminan, ya que ImageSharp los reemplaza:
        // private Bitmap BytesToBitmap(byte[] imageBytes) { ... }
        // private Bitmap ConvertToGrayscale(Bitmap originalBitmap) { ... }
        // private List<Object> GetGrayscalePixelList(Bitmap grayscaleBitmap) { ... }
    }
}