using Dapper.Contrib.Extensions;

namespace MiApi.Models
{
    [Table("dbo.Personas")]
    public class Persona
    {
        [Key]
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int Edad { get; set; }

        [Computed]
        public string DatosCompletos => $"({Id}){Nombre} Edad=> {Edad}";
    }
}
