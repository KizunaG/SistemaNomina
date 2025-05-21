using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NominaSystem.Application.Interfaces
{
    using NominaSystem.Domain.Entities;
    public interface IAuthService
        {
            Task<Usuario?> ValidarCredencialesAsync(string correo, string contrasena);
            string GenerarToken(Usuario usuario);
        }
    }

