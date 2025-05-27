using NominaSystem.Domain.Entities;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            page.Content().Column(col =>
            {
                col.Item().Text($"Expediente de {_empleado.Nombre}").FontSize(20).Bold();
                col.Item().Text($"DPI: {_empleado.DPI}");
                col.Item().Text($"Teléfono: {_empleado.Telefono}");
                col.Item().Text($"Dirección: {_empleado.Direccion}");
                col.Item().Text($"Estado: {_empleado.EstadoLaboral}");
                col.Item().Text($"Fecha Ingreso: {_empleado.FechaIngreso?.ToShortDateString() ?? "N/A"}");



                col.Item().Text("📄 Documentos:").Bold();
                foreach (var doc in _documentos)
                    col.Item().Text($"- {doc.Nombre}");

                col.Item().Text("🎓 Historial Académico:").Bold();
                foreach (var aca in _historial)
                    col.Item().Text($"- {aca.GradoAcademico} en {aca.Institucion}");
            });
        });
    }
}
