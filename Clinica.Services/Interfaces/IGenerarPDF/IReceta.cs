using QuestPDF.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinica.Services.Interfaces.IGenerarPDF
{
    public interface IReceta
    {
        void Compose(IContainer container,int id);
    }
}
