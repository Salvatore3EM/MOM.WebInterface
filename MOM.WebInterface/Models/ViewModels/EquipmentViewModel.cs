using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MOM.WebInterface.Models.ViewModels
{
    public class EquipmentViewModel
    {
        [Required(ErrorMessage = "informazione obbligatoria")]
        public int EquipmentId { get; set; }
        public int Level { get; set; }
        public string EquipmentDescription { get; set; }
        public string SAPCode { get; set; }
    }
}