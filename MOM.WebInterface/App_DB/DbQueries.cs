using log4net.Core;
using MOM.WebInterface.Models.Assembly;
using MOM.WebInterface.Models.DTO;
using MOM.WebInterface.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MOM.WebInterface.App_DB
{
    public static class DbQueries
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(typeof(DbQueries));

        public static class PlantModel
        {
            internal static List<PlantModelTreeDto> GetTree()
            {
                log.Debug("GetTree");
                using (var context = new BusinessService_DBEntities())
                {
                    //var clientIdParameter = new SqlParameter("@ClientId", 4);

                    //var result = context.Database
                    //    .SqlQuery<ResultForCampaign>("GetResultsForCampaign @ClientId", clientIdParameter)
                    //    .ToList();
                    var result = context.Database
                        .SqlQuery<PlantModelTreeDto>("PlantModelTree")
                        .ToList();

                    return result;
                }
            }


            internal static List<PlantModelTreeDtoBase> GetTreeWS()
            {
                log.Debug("GetTree");
                using (var context = new BusinessService_DBEntities())
                {
                    //var clientIdParameter = new SqlParameter("@ClientId", 4);

                    //var result = context.Database
                    //    .SqlQuery<ResultForCampaign>("GetResultsForCampaign @ClientId", clientIdParameter)
                    //    .ToList();
                    var result = context.Database
                        .SqlQuery<PlantModelTreeDtoBase>("PlantModelTreeWS")
                        .ToList();

                    return result;
                }
            }


            /// <summary>
            /// recuper la WorkStation a cui appartiene la WorkPlace 
            /// </summary>
            /// <param name="codiceWP"></param>
            /// <returns></returns>
            internal static string GetCodiceWS_byCodiceWP(string codiceWP)
            {
                log.Debug($"GetCodiceWS_byCodiceWP(\"{codiceWP}\")");
                using (var context = new BusinessService_DBEntities())
                {
                    //var clientIdParameter = new SqlParameter("@ClientId", 4);

                    //var result = context.Database
                    //    .SqlQuery<ResultForCampaign>("GetResultsForCampaign @ClientId", clientIdParameter)
                    //    .ToList();
                    var WpWs = context.Database
                        .SqlQuery<WpWsDto>("WpWs")
                        //.ToList()
                        .Where(w => w.Workplace == codiceWP)
                        .FirstOrDefault();

                    return WpWs.CodiceStazione;
                }
            }




            internal static List<A_Stazioni> GetStazioni(A_TrattiFrontEnd tratto)
            {
                log.Debug($"GetStazioni(\"{tratto.DisplayName}\")");

                using (var context = new UteDigitaleEntities())
                {
                    string CodiceFirstWorkStation = GetCodiceWS_byCodiceWP(tratto.FirstWorkplace);

                    IOrderedQueryable<A_Stazioni> query;

                    if (string.IsNullOrEmpty( tratto.FirstNotIncludedWorkPlace))
                    {

                        query = from s in context.A_Stazioni
                                where s.Cancellato == 0
                                && s.Codice.Contains(tratto.Code)
                                && s.Codice.CompareTo(CodiceFirstWorkStation) >= 0 // s.Codice >= CodiceFirstWorkStation
                                orderby s.Ordine ascending
                                select s;
                    }
                    else
                    {
                        string CodiceFirstNotIncludedWorkStation = GetCodiceWS_byCodiceWP(tratto.FirstNotIncludedWorkPlace);

                        query = from s in context.A_Stazioni
                                where s.Cancellato == 0
                                && s.Codice.Contains(tratto.Code)
                                && s.Codice.CompareTo(CodiceFirstWorkStation) >= 0 // s.Codice >= tratto.FirstWorkplace
                                && s.Codice.CompareTo(CodiceFirstNotIncludedWorkStation) < 0  // s.Codice < CodiceFirstNotIncludedWorkStation
                                orderby s.Ordine ascending
                                select s;
                    }

                    //List<A_Stazioni> result = query.Distinct().ToList().Take(tratto.CountStazioni);

                    var result = query.Distinct().ToList().Take(tratto.CountStazioni);

                    return result.ToList<A_Stazioni>();
                }
            }



            //internal static List<EquipmentViewModel> GetEquipmentList(int level)
            //{
            //    log.Debug($"GetEquipmentList(level: {level})");

            //    using (MelfiSimEntities db = new MelfiSimEntities())
            //    {
            //        var query = from pm in db.PlantModel
            //                    where pm.Level == level
            //                    select new EquipmentViewModel
            //                    {
            //                        Level = pm.Level,
            //                        SAPCode = pm.SAPCode,
            //                        EquipmentDescription = pm.EquipmentDescription,
            //                        EquipmentId = pm.EquipmentId
            //                    };
            //        return query.ToList();
            //    }

            //}




            //internal static List<EquipmentDto> GetPlantModelFlat()
            //{
            //    return GetPlantModelFlat(1);
            //}

            //internal static List<EquipmentDto> GetPlantModelFlat(int startLevel)
            //{
            //    log.Debug($"GetPlantModelFlat(startLevel: {startLevel})");

            //    List<EquipmentDto> PlantModelFlat = null;


            //    try
            //    {
            //        using (MelfiSimEntities db = new MelfiSimEntities())
            //        {
            //            var query = from pm in db.PlantModel
            //                        orderby pm.Level ascending, pm.EquipmentId ascending, pm.DisplayOrder ascending

            //                        select new EquipmentDto
            //                        {
            //                            PagesList = null,
            //                            id = 0,  // progressivo 0-based *** NON E' ancora GESTITO ***
            //                            Level = pm.Level,
            //                            EquipmentId = pm.EquipmentId,
            //                            SAPCode = pm.SAPCode.Trim(),  // "A153CVE",
            //                            SAPDescription = string.Empty, // pm.SAPDescription,  // "A153CVE - VERNICIATURA",
            //                            SAPLongDescription = string.Empty, // pm.SAPLongDescription,  // "A153CVE - Unità Operativa - VERNICIATURA",
            //                            EquipmentDescription = pm.EquipmentDescription.Trim(),  // "VERNICIATURA",
            //                            EquipmentLongDescription = pm.EquipmentLongDescription.Trim(),  // "Unità Operativa - VERNICIATURA",
            //                            EquipmentPathDescription = string.Empty, // pm.EquipmentPathDescription,  // "VERNICIATURA",
            //                            LogicalOrder = pm.LogicalOrder,
            //                            ParentId = pm.Parent,
            //                            IsManual = pm.IsManual,
            //                            IsSettable = pm.IsSettable,
            //                            FlgMonitor = pm.flgMonitor,
            //                            IPAddress = pm.IPAddress,  //"127.0.0.1",
            //                            Windows = null,  // NULL
            //                            GOLTags = null,  // NULL

            //                            //Children = null
            //                        };
            //            PlantModelFlat = query.ToList(); // PlantModelFlat = await query.ToListAsync();
            //        }
            //    }
            //    catch (Exception ex)
            //    {
            //        log.Debug($"GetPlantModelFlat(startLevel: {startLevel}) - ERROR");
            //        log.Error(ex);
            //        PlantModelFlat = null;
            //        Console.WriteLine(ex.ToString());
            //    }

            //    return PlantModelFlat;
            //}


        }
    }

}
