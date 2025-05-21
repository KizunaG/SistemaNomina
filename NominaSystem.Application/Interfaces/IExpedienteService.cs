using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NominaSystem.Application.Interfaces
{
    public interface IExpedienteService
    {
        Task<string> ValidarExpedienteAsync(int empleadoId);
    }

}
