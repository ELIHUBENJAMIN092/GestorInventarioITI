﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Productos.Server.Models
{
    public class Rol
    {
        public int Id { get; set; }

        [MaxLength(50, ErrorMessage = "El campo {0} debe tener máximo {1} caracteres.")]
        public string Nombre { get; set; } = null;

        [MaxLength(200, ErrorMessage = "El campo {0} debe tener máximo {1} caracteres.")]
        public string Descripcion { get; set; } = null;
    }
}