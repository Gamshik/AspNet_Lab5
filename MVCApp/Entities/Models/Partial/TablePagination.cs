using Entities.Models.Partial.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models.Partial
{
    public class TablePagination
    {
        public ControllerInfo ControllerInfo { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int PageSize { get; set; }
        public int TotalSize { get; set; }

        public bool HaveNext { get; set; }
        public bool HavePrev { get; set; }
    }
}
