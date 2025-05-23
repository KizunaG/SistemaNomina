using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using NominaSystem.Application.Interfaces;
using NominaSystem.Application.DTOs;
using NominaSystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace NominaSystem.Infrastructure.Services
{
    public class ReporteService : IReporteService
    {
        static ReporteService()
        {
            QuestPDF.Settings.License = LicenseType.Community;
        }
        private readonly ApplicationDbContext _context;

        public ReporteService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<byte[]> GenerarReporteNominaPorPeriodoAsync(ReporteNominaPeriodoRequest request)
        {
            var nominas = await _context.Nominas
                .Where(n => n.PeriodoInicio >= request.FechaInicio && n.PeriodoFin <= request.FechaFin)
                .Include(n => n.Empleado)
                .ToListAsync();

            var pdf = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Margin(30);
                    page.Size(PageSizes.A4);
                    page.PageColor(Colors.White);
                    page.DefaultTextStyle(x => x.FontSize(12));

                    page.Header().Text($"Reporte de Nómina ({request.FechaInicio:dd/MM/yyyy} - {request.FechaFin:dd/MM/yyyy})")
                                 .FontSize(16).SemiBold().AlignCenter().FontColor(Colors.Blue.Medium);

                    page.Content().Table(table =>
                    {
                        // Columnas
                        table.ColumnsDefinition(columns =>
                        {
                            columns.ConstantColumn(30);  // #
                            columns.RelativeColumn(2);   // Empleado ID
                            columns.RelativeColumn();    // Periodo
                            columns.RelativeColumn();    // Salario
                            columns.RelativeColumn();    // Bonos
                            columns.RelativeColumn();    // Descuentos
                            columns.RelativeColumn();    // Total
                        });

                        // Encabezado
                        table.Header(header =>
                        {
                            header.Cell().Element(CellStyle).Text("#");
                            header.Cell().Element(CellStyle).Text("Empleado");
                            header.Cell().Element(CellStyle).Text("Periodo");
                            header.Cell().Element(CellStyle).Text("Salario Base");
                            header.Cell().Element(CellStyle).Text("Bonificaciones");
                            header.Cell().Element(CellStyle).Text("Descuentos");
                            header.Cell().Element(CellStyle).Text("Total Pago");

                            static IContainer CellStyle(IContainer container) =>
                                container.DefaultTextStyle(x => x.SemiBold()).Padding(5).Background(Colors.Grey.Lighten2).BorderBottom(1);
                        });

                        int i = 1;
                        foreach (var n in nominas)
                        {
                            table.Cell().Element(Cell).Text(i++.ToString());
                            table.Cell().Element(Cell).Text(n.ID_Empleado.ToString());
                            table.Cell().Element(Cell).Text($"{n.PeriodoInicio:dd/MM/yyyy} - {n.PeriodoFin:dd/MM/yyyy}");
                            table.Cell().Element(Cell).Text(n.SalarioBase.ToString("C", CultureInfo.CurrentCulture));
                            table.Cell().Element(Cell).Text(n.Bonificaciones.ToString("C", CultureInfo.CurrentCulture));
                            table.Cell().Element(Cell).Text(n.Descuentos.ToString("C", CultureInfo.CurrentCulture));
                            table.Cell().Element(Cell).Text(n.TotalPago.ToString("C", CultureInfo.CurrentCulture));
                        }

                        static IContainer Cell(IContainer container) =>
                            container.Padding(5).BorderBottom(1).BorderColor(Colors.Grey.Lighten2);
                    });

                    page.Footer().AlignCenter().Text($"Generado el {DateTime.Now:dd/MM/yyyy HH:mm}");
                });
            }).GeneratePdf();

            return pdf;
        }
    }
}
