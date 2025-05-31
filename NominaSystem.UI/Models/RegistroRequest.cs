using System.ComponentModel.DataAnnotations;

namespace NominaSystem.UI.Models
{
    public class RegisterRequest
    {
        [Required(ErrorMessage = "El nombre de usuario es obligatorio")]
        public string UsuarioNombre { get; set; } = "";

        [Required(ErrorMessage = "La contraseña es obligatoria")]
        [MinLength(6, ErrorMessage = "Debe tener al menos 6 caracteres")]
        public string Contrasena { get; set; } = "";
    }

}
