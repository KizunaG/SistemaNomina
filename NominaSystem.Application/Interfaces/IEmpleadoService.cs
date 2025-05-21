using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NominaSystem.Application.Interfaces
{
    using global::NominaSystem.Domain.Entities;

    public interface IEmpleadoService
    {
        Task<List<Empleado>> ObtenerTodosAsync();
        Task<Empleado> ObtenerPorIdAsync(int id);
        Task<Empleado> CrearAsync(Empleado empleado);
        Task<bool> ActualizarAsync(Empleado empleado);
        Task<bool> EliminarAsync(int id);
    }
}
