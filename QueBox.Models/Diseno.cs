using Dapper.Contrib.Extensions;

namespace QueBox.Models
{
    [Table("dbo.diseno")]
    public class Diseno
    {
        [Key]   
        public int ID_diseno { get; set; }
        [Foreign Key]
        public int ID_usuario { get; set; }
        public float Largo { get; set; }
        public float Alto { get; set; }
        public float Ancho { get; set; }
        public string Nombre { get; set; }
        [Foreign Key]
        public int ID_Capa { get; set; }

    }
}
