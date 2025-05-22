using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MOM.WebInterface.Models
{
    // visita prefissa


    public class EquipmentModel
    {
        public string PagesList { get; set; } = null;
        public int id { get; set; } // progressivo 0-based
        public int Level { get; set; }
        public int EquipmentId { get; set; }
        public string SAPCode { get; set; } // "A153CVE",
        public string SAPDescription { get; set; } // "A153CVE - VERNICIATURA",
        public string SAPLongDescription { get; set; } // "A153CVE - Unità Operativa - VERNICIATURA",
        public string EquipmentDescription { get; set; } // "VERNICIATURA",
        public string EquipmentLongDescription { get; set; } // "Unità Operativa - VERNICIATURA",
        public string EquipmentPathDescription { get; set; } // "VERNICIATURA",
        public int ParentId { get; set; }
        public bool IsManual { get; set; }
        public bool IsSettable { get; set; }
        public bool FlgMonitor { get; set; }
        public string IPAddress { get; set; } //"127.0.0.1",
        public string Windows { get; set; } // NULL
        public string GOLTags { get; set; } // NULL
    }
}