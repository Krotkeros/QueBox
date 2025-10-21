using Dapper.Contrib.Extensions;

namespace QueBox.Models
{
    [Table("dbo.Capa")]
    public class Capa
    {
        [Key]
        public int Id_Capa { get; set; }
        public int Id_Img { get; set; }
        public int Numero { get; set; }
    }

}
