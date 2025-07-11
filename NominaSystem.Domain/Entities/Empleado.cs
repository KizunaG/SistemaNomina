﻿using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace NominaSystem.Domain.Entities;

public class Empleado
{
    public int Id { get; set; }
    public required string Nombre { get; set; }
    public DateTime? FechaNacimiento { get; set; }
    public string? Direccion { get; set; }
    public string? Telefono { get; set; }
    public required string DPI { get; set; }
    public required string EstadoLaboral { get; set; }
    public DateTime? FechaIngreso { get; set; }

    [NotMapped]
    public bool ExpedienteCompleto { get; set; }

    // Llaves foráneas
    public int? ID_Cargo { get; set; }
    public int? ID_Departamento { get; set; }

    // Propiedades de navegación
    public Cargo? Cargo { get; set; }
    public Departamento? Departamento { get; set; }

}
