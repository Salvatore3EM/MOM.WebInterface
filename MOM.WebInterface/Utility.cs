using MOM.WebInterface.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Web.Mvc.Html;
using System.Xml.Linq;

namespace MOM.WebInterface
{
    public static class Utility
    {

        public static List<PlantModelTreeDtoBase> GetEquipmentsTreeIterative(List<PlantModelTreeDtoBase> equipmentsFlat)
        {
            void PreOrder(ref PlantModelTreeDtoBase root)
            {
                if (root == null)
                    return;

                Stack<PlantModelTreeDtoBase> s = new Stack<PlantModelTreeDtoBase>();
                s.Push(root);

                while (s.Count > 0)
                {
                    PlantModelTreeDtoBase curr = s.Pop();

                    // visita della radice: raccolta dei figli del sottoalbero
                    curr.Children = equipmentsFlat.Where(e => e.IdParent == curr.IdEquipment).ToList();

                    if (curr.Children.Count > 0)
                    {
                        foreach (PlantModelTreeDtoBase child in curr.Children)
                        {
                            s.Push(child);
                        }
                    }
                }

                return;
            }



            Stack<PlantModelTreeDtoBase> stackMaster = new Stack<PlantModelTreeDtoBase>();

            List<PlantModelTreeDtoBase> Roots = equipmentsFlat.Where(e => (e.IdParent == null) || (e.IdParent <= 0)).ToList();
            foreach (PlantModelTreeDtoBase root in Roots)
            {
                stackMaster.Push(root);
            }

            while (stackMaster.Count > 0)
            {
                PlantModelTreeDtoBase root = stackMaster.Pop();
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

        public static List<int> PreOrder(PlantModelTreeDtoBase root, ref List<PlantModelTreeDtoBase> equipmentsFlat)
        {
            List<int> res = new List<int>();
            if (root == null)
                return res;
            Stack<PlantModelTreeDtoBase> s = new Stack<PlantModelTreeDtoBase>();
            s.Push(root);

            while (s.Count > 0)
            {
                PlantModelTreeDtoBase curr = s.Pop();
                // res.Add(curr.data); // visita la radice

                // visita della radice: raccolta dei figli del sottoalbero
                curr.Children = equipmentsFlat.Where(e => e.IdParent == curr.IdEquipment).ToList();

                if (curr.Children.Count > 0)
                {
                    foreach(PlantModelTreeDtoBase child in curr.Children)
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
