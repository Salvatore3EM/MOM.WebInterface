using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MOM.WebInterface.Models.DTO;

namespace MOM.WebInterface.Models.ViewModels
{
    public class PlantModelViewModel
    {
        public List<PlantModelTreeDto> EquipmentList { get; set; } = new List<PlantModelTreeDto>();
        public List<ErrorItemDto> ErrorList { get; set; } = new List<ErrorItemDto>();
    }
    //public class EquipmentTreeDto
    //{
    //    public List<EquipmentDto> Children { get; set; } = new List<EquipmentDto>();
    //}
    public class ErrorItemDto {
        public string Description { get; set; }
        public int Id { get; set; }

        public ErrorItemDto(string description, int id = 0)
        {
            Description = description;
            Id = id;
        }
    }
}