using System.Drawing;

namespace QueBox.Services
{
    public interface IMathy
    {

        Task<List<Object>> GetGrayscalePixelsFromUrl(string imageUrl);

    }
}