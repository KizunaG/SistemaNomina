using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NominaSystem.Application.DTOs;

public class LoginRequestDto
{
    public string UsuarioNombre { get; set; } = "";
    public string Contrasena { get; set; } = "";
}
