using Entities.Models.Partial.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models.Partial
{
    public class DeleteEntity
    {
        public ControllerInfo ControllerInfo { get; set; }
        public Guid? Id { get; set; }
        public string PopUpId { get; set; }
        public string ItemName { get; set; }
    }
}
