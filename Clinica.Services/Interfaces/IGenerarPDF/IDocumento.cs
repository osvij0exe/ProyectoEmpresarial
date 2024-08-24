using Clinica.Models.Response;
using QuestPDF.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinica.Services.Interfaces.GenerarPDF
{
    public interface IDocumento
    {
        DocumentMetadata GetMetadata();
        DocumentSettings GetSettings();

        void Compose(IDocumentContainer container,int id);

    }
}
