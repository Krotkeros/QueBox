using Dapper.Contrib.Extensions;
using System.Collections.Generic;

namespace QueBox.Models
{
    [Table("dbo.Capa")]
    public class Capa
    {
        [Key]
        public int Id_Capa { get; set; }
        public int Id_Diseno { get; set; }
        public int Numero { get; set; }
    }

}
