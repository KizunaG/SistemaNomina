using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NominaSystem.Application.DTOs
{
    public class RegisterRequest
    {
        
        [Required]
        public string UsuarioNombre { get; set; } = "";

        [Required]
        [MinLength(6)]
        public string Contrasena { get; set; } = "";
    }
}


