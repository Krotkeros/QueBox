using Dapper.Contrib.Extensions;

namespace QueBox.Models
{
    [Table("dbo.Diseno")]
    public class Diseno
    {
        [Key]   
        public int Id_Diseno { get; set; }
        [ForeignKey("Usuario")]
        public int Id_Usuario { get; set; }
        public float Largo { get; set; }
        public float Alto { get; set; }
        public float Ancho { get; set; }
        public string Nombre { get; set; }

    }
}
