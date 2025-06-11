using MOM.WebInterface.Models.DTO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MOM.WebInterface.App_DB
{
    public static class Utility
    {

        public static List<EquipmentDto> GetEquipmentsTreeIterative(List<EquipmentDto> equipmentsFlat)
        {
            void PreOrder(ref EquipmentDto root)
            {
                if (root == null)
                    return;

                Stack<EquipmentDto> s = new Stack<EquipmentDto>();
                s.Push(root);

                while (s.Count > 0)
                {
                    EquipmentDto curr = s.Pop();

                    // visita della radice: raccolta dei figli del sottoalbero
                    curr.Children = equipmentsFlat.Where(e => e.IdParent == curr.IdEquipment).ToList();

                    if (curr.Children.Count > 0)
                    {
                        foreach (EquipmentDto child in curr.Children)
                        {
                            s.Push(child);
                        }
                    }
                }

                return;
            }



            Stack<EquipmentDto> stackMaster = new Stack<EquipmentDto>();

            List<EquipmentDto> Roots = equipmentsFlat.Where(e => (e.IdParent == null) || (e.IdParent <= 0)).ToList();
            foreach (EquipmentDto root in Roots)
            {
                stackMaster.Push(root);
            }

            while (stackMaster.Count > 0)
            {
                EquipmentDto root = stackMaster.Pop();
                PreOrder(ref root);
            }

            return Roots;

            //int Level = Roots.Max(r => r.Level);

            //while (equipmentsFlat.Count > 0)
            //{
            //    equipmentsFlat.RemoveAll(r => Roots.Any(a => a.IdEquipment == r.IdEquipment));
            //    foreach (PlantModelTreeDtoBase item in equipmentsFlat)
            //    {
            //        item.Children = equipmentsFlat.Where(e => e.IdParent ==  item.IdEquipment).ToList();
            //        Level = Roots.Max(r => r.Level);
            //    }
            //}
        }

        public static List<int> PreOrder(EquipmentDto root, ref List<EquipmentDto> equipmentsFlat)
        {
            List<int> res = new List<int>();
            if (root == null)
                return res;
            Stack<EquipmentDto> s = new Stack<EquipmentDto>();
            s.Push(root);

            while (s.Count > 0)
            {
                EquipmentDto curr = s.Pop();
                // res.Add(curr.data); // visita la radice

                // visita della radice: raccolta dei figli del sottoalbero
                curr.Children = equipmentsFlat.Where(e => e.IdParent == curr.IdEquipment).ToList();

                if (curr.Children.Count > 0)
                {
                    foreach (EquipmentDto child in curr.Children)
                    {
                        s.Push(child);
                    }
                }
                //if (curr.right != null)
                //    s.Push(curr.right);
                //if (curr.left != null)
                //    s.Push(curr.left);
            }

            return res;
        }

        public static List<EquipmentDto> GetPlantModelTreeFlatEquipmentDto(string parametrouno, string parametrodue)
        {
            BusinessService_DBEntities db = new BusinessService_DBEntities();
            List<EquipmentDto> result = null;

            try
            {
                var parametro_uno = new SqlParameter("@parametro_uno", parametrouno);
                var parametro_due = new SqlParameter("@parametro_due", parametrodue);

                result = db.Database.SqlQuery<EquipmentDto>("EXEC dbo.PlantModelSubTree @parametro_uno, @parametro_due", parametro_uno, parametro_due).ToList();
            }
            catch (Exception ex)
            {
                //log.Error(ex);
            }

            if ((result == null) || (result.Count == 0))
            {
                return null;
            }

            else
            {
                return result;
            }
        }

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