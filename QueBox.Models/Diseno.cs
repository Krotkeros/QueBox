using Dapper.Contrib.Extensions;

namespace QueBox.Models
{
    [Table("dbo.diseno")]
    public class Diseno
    {
        [Key]   
        public int Id_Diseno { get; set; }
        [Foreign Key]
        public int Id_Usuario { get; set; }
        public float Largo { get; set; }
        public float Alto { get; set; }
        public float Ancho { get; set; }
        public string Nombre { get; set; }

    }
}
