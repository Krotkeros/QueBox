using Dapper.Contrib.Extensions;

namespace QueBox.Models
{
    [Table("dbo.Capa")]
    public class Capa
    {
        [Key]
        public int ID_Capa { get; set; }
        public int ID_IMG { get; set; }
        public int Numero { get; set; }
    }

}
