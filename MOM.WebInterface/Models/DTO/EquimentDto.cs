using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MOM.WebInterface.Models.DTO
{
    public class EquipmentDto
    {
        public int IdEquipment { get; set; }
        public int? IdParent { get; set; }
        public int Level { get; set; }
        public int CountChildren { get; set; }
        public string Table { get; set; }
        public int IdTable { get; set; }
        public string Codice { get; set; }
        public string Descrizione { get; set; }
        public string DisplayName { get; set; }
        public int Ordine { get; set; }
        public List<EquipmentDto> Children { get; set; } = new List<EquipmentDto>();
    }
}
