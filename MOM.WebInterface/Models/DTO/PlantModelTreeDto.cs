using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MOM.WebInterface.Models.DTO {
    public class PlantModelTreeDto {

         //Level ParentId ObjectId EquipmentPath EquipmentPathDescription EquipmentDescription

        public int EquipmentId { get; set; }
        public int ParentId { get; set; }
        public int Level { get; set; }
        public int IdTable { get; set; }
        public string EquipmentPath { get; set; } = "EquipmentPath";
        public string ObjectTypeId { get; set; }
        public string EquipmentDescription { get; set; }
        public string EquipmentPathDescription { get; set; } = "EquipmentPathDescription";
        public int ObjectId { get; set; } = 99;
        public List<PlantModelTreeDto> Children { get; set; } = new List<PlantModelTreeDto>();
    }
}