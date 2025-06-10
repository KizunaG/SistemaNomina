using NominaSystem.Domain.Entities;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System;
using System.Collections.Generic;

namespace NominaSystem.Infrastructure.Documents;

public class DocumentExpedienteEmpleado : IDocument
{
    private readonly Empleado _empleado;
    private readonly List<DocumentoEmpleado> _documentos;
    private readonly List<InformacionAcademica> _historial;

    public DocumentExpedienteEmpleado(Empleado empleado, List<DocumentoEmpleado> documentos, List<InformacionAcademica> historial)
    {
        _empleado = empleado;
        _documentos = documentos;
        _historial = historial;
    }

    public DocumentMetadata GetMetadata() => DocumentMetadata.Default;

    public void Compose(IDocumentContainer container)
    {
        container.Page(page =>
        {
            page.Margin(30);
            page.Size(PageSizes.A4);
            page.PageColor(Colors.White);
            page.DefaultTextStyle(x => x.FontSize(12));

            page.Content()
                .Column(col =>
                {
                    col.Spacing(10);

                    // Título principal
                    col.Item().Text($"Expediente de {_empleado.Nombre}")
                        .FontSize(20)
                        .Bold()
                        .Underline()
                        .FontColor(Colors.Black);

                    // Datos básicos
                    col.Item().Text($"DPI: {_empleado.DPI}");
                    col.Item().Text($"Teléfono: {_empleado.Telefono}");
                    col.Item().Text($"Dirección: {_empleado.Direccion}");
                    col.Item().Text($"Estado: {_empleado.EstadoLaboral}");
                    col.Item().Text($"Fecha Ingreso: {_empleado.FechaIngreso?.ToShortDateString() ?? "N/A"}");
                    col.Item().Text($"Cargo: {_empleado.Cargo?.NombreCargo ?? "N/A"}");
                    col.Item().Text($"Departamento: {_empleado.Departamento?.NombreDepartamento ?? "N/A"}");





                    col.Item().LineHorizontal(1).LineColor(Colors.Grey.Lighten2);

                    // Documentos
                    if (_documentos.Count > 0)
                    {
                        col.Item().Text("📄 Documentos").Bold().FontSize(14).FontColor(Colors.Blue.Medium);
                        col.Item().Table(table =>
                        {
                            table.ColumnsDefinition(columns =>
                            {
                                columns.RelativeColumn(3);
                                columns.RelativeColumn(1);
                                columns.RelativeColumn(4);
                                columns.RelativeColumn(2);
                            });

                            table.Header(header =>
                            {
                                header.Cell().Text("Nombre").Bold();
                                header.Cell().Text("Tipo").Bold();
                                header.Cell().Text("Ruta").Bold();
                                header.Cell().Text("Fecha Entrega").Bold();
                            });

                            foreach (var doc in _documentos)
                            {
                                table.Cell().Text(doc.Nombre ?? "");
                                table.Cell().Text(doc.TipoDocumento);
                                var url = $"https://localhost:50745/{doc.RutaArchivo}";
                                table.Cell().Hyperlink(url).Text("Ver documento PDF").FontColor(Colors.Blue.Medium);



                                table.Cell().Text(doc.FechaEntrega?.ToString("dd/MM/yyyy") ?? "N/A");
                            }
                        });

                        col.Item().LineHorizontal(1).LineColor(Colors.Grey.Lighten2);
                    }
                    else
                    {
                        col.Item().Text("No hay documentos registrados.");
                    }

                    // Historial Académico
                    if (_historial.Count > 0)
                    {
                        col.Item().Text("🎓 Historial Académico").Bold().FontSize(14).FontColor(Colors.Purple.Medium);
                        col.Item().Table(table =>
                        {
                            table.ColumnsDefinition(columns =>
                            {
                                columns.RelativeColumn(3);
                                columns.RelativeColumn(3);
                                columns.RelativeColumn(2);
                            });

                            table.Header(header =>
                            {
                                header.Cell().Text("Título").Bold();
                                header.Cell().Text("Institución").Bold();
                                header.Cell().Text("Fecha Graduación").Bold();
                            });

                            foreach (var aca in _historial)
                            {
                                table.Cell().Text(aca.Titulo);
                                table.Cell().Text(aca.Institucion);
                                table.Cell().Text(aca.FechaGraduacion?.ToString("dd/MM/yyyy") ?? "N/A");
                            }
                        });
                    }
                    else
                    {
                        col.Item().Text("No hay información académica registrada.");
                    }
                });

            page.Footer()
                .AlignCenter()
                .Text($"Generado el {DateTime.Now:dd/MM/yyyy HH:mm}");
        });
    }
}

