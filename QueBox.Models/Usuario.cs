﻿using Dapper.Contrib.Extensions;

namespace QueBox.Models
{
    [Table("dbo.Usuario")]
    public class Usuario
    {
        [Key]
        public int ID_usuario { get; set; }
        public string Usuario { get; set; }
        public string Clave { get; set; }
        public string Correo { get; set; }
    }

}