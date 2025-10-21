using Dapper.Contrib.Extensions;

namespace QueBox.Models
{
    [Table("dbo.Capa")]
    public class Capa
    {
        [Key]
        public int Id_Capa { get; set; }
        [ForeignKey("Diseno")]
        public int Id_Diseno { get; set; }
        public int Numero { get; set; }
    }

}
