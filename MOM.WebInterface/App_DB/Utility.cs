using MOM.WebInterface.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MOM.WebInterface.App_DB
{
    public static class Utility
    {
        public static List<PlantModelTreeDto> GetEquipmentsTree(ref List<PlantModelTreeDto> equipmentsFlat)
        {
            // e' la prima ricerca
            // contiene la lista delle radici degli alberi - dovrebbe essercene solo una
            List<PlantModelTreeDto> children = equipmentsFlat.Where(e => (e.ParentId == null) || (e.ParentId <= 0)).ToList();

            foreach (PlantModelTreeDto child in children)
            {
                AddDescendants(child, ref equipmentsFlat);
            }
            return children;
        }

        public static void AddDescendants(PlantModelTreeDto node, ref List<PlantModelTreeDto> equipmentsFlat)
        {
            node.Children = equipmentsFlat.Where(e => e.ParentId == node.EquipmentId).ToList();
            foreach (PlantModelTreeDto child in node.Children)
            {
                AddDescendants(child, ref equipmentsFlat);
            }
        }


    }
}