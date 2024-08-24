using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinica.Models
{
    public class PaginationData
    {
        public int CurrentPage { get; set; }
        public int RowsPerPage { get; set; }
        public int TotalPages { get; set; }
        public int RowCount { get; set; }
    }
}
