using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using QuestPDF.Drawing;
using QuestPDF.Previewer;
using NominaSystem.Domain.Entities;
using static QuestPDF.Helpers.Colors;

public class DocumentoNominaEmpleado
{
    private readonly Nomina _nomina;

    public DocumentoNominaEmpleado(Nomina nomina)
    {
        _nomina = nomina;
    }

    public byte[] Generar()
    {
        var salarioBase = _nomina.SalarioBase > 0
            ? _nomina.SalarioBase
            : _nomina.Empleado?.Cargo?.SalarioBase ?? 0;

        var igss = _nomina.IGSS;
        var totalPago = salarioBase + _nomina.Bonificaciones + _nomina.HorasExtras - _nomina.Descuentos - igss;

        return Document.Create(container =>
        {
            container.Page(page =>
            {
                page.Margin(40);
                page.Size(PageSizes.A4);
                page.PageColor(Colors.White);
                page.DefaultTextStyle(x => x.FontSize(12));

                page.Header()
                    .Text("Recibo de Nómina")
                    .FontSize(20)
                    .SemiBold()
                    .FontColor(Colors.Blue.Medium);

                page.Content().PaddingVertical(10).Column(col =>
                {
                    col.Item().Text($"Empleado: {_nomina.Empleado?.Nombre ?? "Desconocido"}").Bold();

                    col.Item().PaddingTop(15).Table(table =>
                    {
                        table.ColumnsDefinition(columns =>
                        {
                            columns.RelativeColumn(1);
                            columns.RelativeColumn(1);
                        });

                        void Row(string label, string value)
                        {
                            table.Cell().Element(CellStyle).Text(label).SemiBold().AlignLeft().FontSize(12);
                            table.Cell().Element(CellStyle).Text(value).AlignRight().FontSize(12);
                        }

                        Row("Período:", $"{_nomina.PeriodoInicio:dd/MM/yyyy} - {_nomina.PeriodoFin:dd/MM/yyyy}");
                        Row("Salario Base:", salarioBase.ToString("C"));
                        Row("Horas Extras:", _nomina.HorasExtras.ToString("C"));
                        Row("Bonificaciones:", _nomina.Bonificaciones.ToString("C"));
                        Row("Descuentos:", _nomina.Descuentos.ToString("C"));
                        Row("Descuento IGSS:", igss.ToString("C"));  // ✅ NUEVA FILA
                        Row("Total a Pagar:", totalPago.ToString("C"));

                        IContainer CellStyle(IContainer container)
                            => container.BorderBottom(1).BorderColor(Colors.Grey.Lighten2).PaddingVertical(5);
                    });

                    col.Item().PaddingTop(25).Text("Gracias por su esfuerzo y compromiso.").Italic();
                });

                page.Footer()
                    .AlignCenter()
                    .Text(txt =>
                    {
                        txt.Span("SISTEMA DE NOMINA").FontSize(10);
                    });
            });
        }).GeneratePdf();
    }
}

