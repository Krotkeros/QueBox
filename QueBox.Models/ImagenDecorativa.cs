using Dapper.Contrib.Extensions;

namespace QueBox.Models
{
    [Table("dbo.ImagenDecorativa")]
    public class ImagenDecorativa
    {
        [Key]
        public int ID_IMG { get; set; }
        public string Url { get; set; }
        public float Alto { get; set; }
        public float Ancho { get; set; }
    }

}