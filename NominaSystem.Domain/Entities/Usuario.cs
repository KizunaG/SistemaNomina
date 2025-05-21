using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NominaSystem.Domain.Entities;

public class Usuario
{
    public int Id { get; set; }

    public required string UsuarioNombre { get; set; }

    public string Correo { get; set; }

    public required string Contrasena { get; set; }

    public int ID_Rol { get; set; }

    public Rol Rol { get; set; }
    public int ID_Empleado { get; set; }
}
