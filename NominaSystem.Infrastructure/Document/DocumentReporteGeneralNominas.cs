using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using NominaSystem.Application.DTOs;

public class DocumentoReporteGeneralNominas
{
    private readonly List<NominaDto> _nominas;

    public DocumentoReporteGeneralNominas(List<NominaDto> nominas)
    {
        _nominas = nominas;
    }

    public byte[] Generar()
    {
        return Document.Create(container =>
        {
            container.Page(page =>
            {
                page.Margin(30);
                page.Size(PageSizes.A4);
                page.PageColor(Colors.White);
                page.DefaultTextStyle(x => x.FontSize(11));

                page.Header()
                    .Text("Reporte General de Nóminas")
                    .FontSize(18)
                    .SemiBold()
                    .FontColor(Colors.Blue.Medium);

                page.Content().PaddingTop(15).Table(table =>
                {
                    table.ColumnsDefinition(columns =>
                    {
                        columns.RelativeColumn(2); // Empleado
                        columns.RelativeColumn(3); // Periodo
                        columns.RelativeColumn(2); // Salario Base
                        columns.RelativeColumn(2); // Horas Extras
                        columns.RelativeColumn(2); // Bonificaciones
                        columns.RelativeColumn(2); // Descuentos
                        columns.RelativeColumn(2); // IGSS
                        columns.RelativeColumn(2); // Total Pago
                    });

                    // Encabezados
                    table.Header(header =>
                    {
                        header.Cell().Element(Encabezado).Text("Empleado");
                        header.Cell().Element(Encabezado).Text("Período");
                        header.Cell().Element(Encabezado).Text("Salario Base");
                        header.Cell().Element(Encabezado).Text("Horas Extras");
                        header.Cell().Element(Encabezado).Text("Bonificaciones");
                        header.Cell().Element(Encabezado).Text("Descuento");
                        header.Cell().Element(Encabezado).Text("Descuento IGSS");
                        header.Cell().Element(Encabezado).Text("Total Pago");
                    });

                    // Filas de datos
                    foreach (var nom in _nominas)
                    {
                        table.Cell().Element(Celda).Text(nom.NombreEmpleado);
                        table.Cell().Element(Celda).Text($"{nom.PeriodoInicio:yyyy-MM-dd} - {nom.PeriodoFin:yyyy-MM-dd}");
                        table.Cell().Element(Celda).Text(nom.SalarioBase.ToString("C"));
                        table.Cell().Element(Celda).Text(nom.HorasExtras.ToString("C"));
                        table.Cell().Element(Celda).Text(nom.Bonificaciones.ToString("C"));
                        table.Cell().Element(Celda).Text(nom.Descuentos.ToString("C"));
                        table.Cell().Element(Celda).Text(nom.IGSS.ToString("F2"));
                        table.Cell().Element(Celda).Text(nom.TotalPago.ToString("C"));
                    }

                    // Estilo para encabezado
                    static IContainer Encabezado(IContainer container) =>
                        container
                            .BorderBottom(1)
                            .BorderRight(1)
                            .BorderColor(Colors.Black)
                            .Padding(5)
                            .Background(Colors.Grey.Lighten2)
                            .ShowOnce();

                    // Estilo para celdas normales
                    static IContainer Celda(IContainer container) =>
                        container
                            .BorderBottom(1)
                            .BorderRight(1)
                            .BorderColor(Colors.Grey.Lighten2)
                            .Padding(5);
                });

                page.Footer()
                    .AlignCenter()
                    .Text("Sistema de Nómina").FontSize(10);
            });
        }).GeneratePdf();
    }
}


