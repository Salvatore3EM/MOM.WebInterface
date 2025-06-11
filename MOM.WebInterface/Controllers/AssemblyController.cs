using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Xml.Serialization;
using System.Xml;
using MOM.WebInterface.Models;
using MOM.WebInterface.Models.Assembly;
using Newtonsoft.Json;
using System.Resources;
using System.Reflection;
using System.Drawing.Drawing2D;
using MOM.WebInterface.App;
using System.Runtime.CompilerServices;
using System.Text;
using MOM.WebInterface.App_DB;

namespace MOM.WebInterface.Controllers
{
    public class AssemblyController : ApiController
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(typeof(AssemblyController));


        // GET: api/PlantModels
        public HttpResponseMessage Get()
        {
            return DebugGet();

            log.Debug("Get");
            string result;
            try
            {
                //LineaDto linea = null;
                A_TrattiFrontEnd tratto = null;

                //string fullFileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"ConfigurazionePlant.xml");
                //Plant plant = LoadPlantConfig(fullFileName);

                OpState opState = new OpState();
                List<Anomalia> anomalie = opState.CaricaAnomalie(); // 1.
                List<Vettura> vetture = opState.CaricaTutto();      // 2.


                // dall'elenco dei CIS presenti tra i traguardi D-R e R-E estrai quelli
                // a. nei tratti e
                // b. nei buffer

                log.Debug("estrae i CIS nei Tratti");
                //json = Resources.GlobalResource.TRIM1;
                //linea = JsonConvert.DeserializeObject<Linea>(json);
                //linea = GetLinea("TRIM1");
                tratto = GetTrattoFrontEnd("TRIM1");
                List<Vettura> CisSeqTrim1 = opState.GetCisInTratto(tratto, vetture);
                List<Vettura> CisSeqTrim2 = opState.GetCisInTratto(GetTrattoFrontEnd("TRIM2"), vetture);
                List<Vettura> CisSeqBufferTR1TR2 = opState.GetCisInBuffer( CisSeqTrim1,  CisSeqTrim2,  vetture);

                List<Vettura> CisSeqCh1 = opState.GetCisInTratto(GetTrattoFrontEnd("CH1"), vetture);
                List<Vettura> CisSeqBufferTR2_CH1 = opState.GetCisInBuffer(CisSeqTrim2, CisSeqCh1, vetture);

                List<Vettura> CisSeqDCKSUP = opState.GetCisInTratto(GetTrattoFrontEnd("DCKSUP"), vetture);
                List<Vettura> CisSeqBufferCH1_DCKSUP = opState.GetCisInBuffer(CisSeqCh1, CisSeqDCKSUP, vetture);

                List<Vettura> CisSeqCH2 = opState.GetCisInTratto(GetTrattoFrontEnd("CH2"), vetture);
                List<Vettura> CisSeqBufferDCKSUP_CH2 = opState.GetCisInBuffer(CisSeqDCKSUP, CisSeqCH2, vetture);

                List<Vettura> CisSeqGLZ = opState.GetCisInTratto(GetTrattoFrontEnd("GLZ"), vetture);
                List<Vettura> CisSeqBufferCH2_GLZ = opState.GetCisInBuffer(CisSeqCH2, CisSeqGLZ, vetture);

                List<Vettura> CisSeqCH3 = opState.GetCisInTratto(GetTrattoFrontEnd("CH3"), vetture);
                List<Vettura> CisSeqBufferGLZCH3 = opState.GetCisInBuffer(CisSeqGLZ, CisSeqCH3, vetture);

                List<Vettura> CisSeqFN1 = opState.GetCisInTratto(GetTrattoFrontEnd("FN1"), vetture);
                List<Vettura> CisSeqBufferCH3FN1 = opState.GetCisInBuffer(CisSeqCH3, CisSeqFN1, vetture);

                List<Vettura> CisSeqFN2 = opState.GetCisInTratto(GetTrattoFrontEnd("FN2"), vetture);
                List<Vettura> CisSeqBufferFN1FN2 = opState.GetCisInBuffer(CisSeqFN1, CisSeqFN2, vetture);

                List<Vettura> CisSeqBufferTestinline = opState.GetCisTestInLine();

                log.Debug("estrai i CIS nei Sub-Tratti / Sub-Linee");

                //
                // Subline
                //
                List<Vettura> CisSeqDOO = opState.GetCisInTratto(GetTrattoFrontEnd("DOO"), vetture);
                //List<Vettura> CisSeqBufferDOOFN1 = opState.GetCisInBuffer_DOO_FN1(ref CisSeqDOO, ref CisSeqFN1, ref vetture);
                List<Vettura> CisSeqBufferDOOFN1 = opState.GetCisInBuffer("DOO-FN1",  CisSeqDOO,  CisSeqFN1,  vetture);

                List<Vettura> CisSeqDSB = opState.GetCisInTratto(GetTrattoFrontEnd("DSB"), vetture);
                //List<Vettura> CisSeqBufferDSBTR2 = opState.GetCisInBuffer_DSB_TR2(ref CisSeqDSB, ref CisSeqTrim2, ref vetture);
                List<Vettura> CisSeqBufferDSBTR2 = opState.GetCisInBuffer("DSB-TR2", CisSeqDSB, CisSeqTrim2, vetture);

                List<Vettura> CisSeqFRE = opState.GetCisInTratto(GetTrattoFrontEnd("FRE"), vetture);
                //List<Vettura> CisSeqBufferFRECH2 = opState.GetCisInBuffer_FRE_CH2(ref CisSeqFRE, ref CisSeqCH2, ref vetture);
                List<Vettura> CisSeqBufferFRECH2 = opState.GetCisInBuffer("FRE-CH2", CisSeqFRE, CisSeqCH2, vetture);

                List<Vettura> CisSeqPRT = opState.GetCisInTratto(GetTrattoFrontEnd("PRT"), vetture);
                List<Vettura> CisSeqBufferPRTCH2 = opState.GetCisInBuffer("PRT-CH2", CisSeqPRT, CisSeqCH2, vetture);


                log.Debug("estrae i CIS nelle Meccaniche");

                //
                // Meccanica
                //
                List<Vettura> CisSeqUMC = opState.GetCisInTratto(GetTrattoFrontEnd("UMC"), vetture);
                List<Vettura> CisSeqDSP = opState.GetCisInTratto(GetTrattoFrontEnd("DSP"), vetture);
                List<Vettura> CisSeqBufferUMCDSP = opState.GetCisInBuffer("UMC-DSP", CisSeqUMC, CisSeqDSP, vetture);

                List<Vettura> CisSeqGOMA = opState.GetCisInTratto(GetTrattoFrontEnd("GOMA"), vetture);
                List<Vettura> CisSeqBufferDSPGOMA = opState.GetCisInBuffer("DSP-GOMA", CisSeqDSP, CisSeqGOMA, vetture);

                List<Vettura> CisSeqGOMP = opState.GetCisInTratto(GetTrattoFrontEnd("GOMP"), vetture);
                List<Vettura> CisSeqDCKINF = opState.GetCisInTratto(GetTrattoFrontEnd("DCKINF"), vetture);
                List<Vettura> CisSeqBufferGOMPDCKINF = opState.GetCisInBuffer("GOMP-DCKINF", CisSeqDSP, CisSeqGOMA, vetture);


                result = JsonConvert.SerializeObject(CisSeqTrim1, Newtonsoft.Json.Formatting.Indented);

            }
            catch (Exception ex)
            {
                log.Error(ex);
                result = "{\"EquipmentList\":[],\"ErrorList\":[\"" + ex.Message + "\"]}";
            }

            var response = Request.CreateResponse(HttpStatusCode.OK);
            response.Content = new StringContent(result, System.Text.Encoding.UTF8, "application/json");
            return response;
        }






        private LineaDto GetLinea(string Nome)
        {
            // Create a resource manager to retrieve resources.
            global::System.Resources.ResourceManager rm = new global::System.Resources.ResourceManager("Resources.GlobalResource", global::System.Reflection.Assembly.Load("App_GlobalResources"));
            //ResourceManager rm = new ResourceManager("items", Assembly.GetExecutingAssembly());


            // Retrieve the value of the string resource named "welcome".
            // The resource manager will retrieve the value of the  
            // localized resource using the caller's current culture setting.
            string jsonValue = rm.GetString(Nome);

            LineaDto linea = JsonConvert.DeserializeObject<LineaDto>(jsonValue);
            return linea;
        }

        private A_TrattiFrontEnd GetTrattoFrontEnd(string displayName)
        {
            using (var context = new BusinessService_DBEntities())
            {
                var query = from t in context.A_TrattiFrontEnd
                            where t.DisplayName == displayName.ToUpper()
                            select t;

                A_TrattiFrontEnd result = query.FirstOrDefault();

                return result;
            }

        }






        //private Plant LoadPlantConfig(string fullFilePath)
        //{
        //    XmlSerializer ser = new XmlSerializer(typeof(Plant));
        //    Plant plant;
        //    using (XmlReader reader = XmlReader.Create(fullFilePath))
        //    {
        //        plant = (Plant)ser.Deserialize(reader);
        //    }

        //    return plant;
        //}


        private int CountAnomalieVetture(List<Vettura> vetture)
        {
            int result = 0;
            foreach(Vettura v in vetture)
            {
                result += v.ListaAnomalia.Count;
            }
            return result;
        }




        private HttpResponseMessage DebugGet()
        {
            log.Debug("DebugGet");
            string result;
            try
            {
                StringBuilder tempResult = new StringBuilder();

                OpState opState = new OpState();
                List<Anomalia> anomalie = opState.CaricaAnomalie(); // 1.
                List<Vettura> vetture = opState.CaricaTutto();      // 2.

                log.Debug("caricamento Linee");
                //json = Resources.GlobalResource.TRIM1;
                //linea = JsonConvert.DeserializeObject<Linea>(json);

                int start = 0;
                A_TrattiFrontEnd tratto = GetTrattoFrontEnd("TRIM1");
                //start = Debug_GetCisInLinea(tratto, start,  vetture, out List<Vettura> CisSeqTrim1);
                //start = Debug_GetCisInLinea(GetTrattoFrontEnd("TRIM2"), start,  vetture, out List<Vettura> CisSeqTrim2);

                start = Debug_GetCisInLinea(tratto, start, vetture, anomalie, out List<Vettura> CisSeqTrim1);

                start = Debug_GetCisInLinea(GetTrattoFrontEnd("TRIM2"), start, vetture, anomalie, out List<Vettura> CisSeqTrim2);


                //start = Debug_GetCisInBuffer(10, start,  vetture, out List<Vettura> CisSeqBufferTR1TR2);

                //start = Debug_GetCisInLinea(GetTrattoFrontEnd("CH1"), start, vetture, out List<Vettura> CisSeqCh1);
                //start = Debug_GetCisInBuffer(10, start, vetture, out List<Vettura> CisSeqBufferTR2_CH1);
                //start = Debug_GetCisInLinea(GetTrattoFrontEnd("DCKSUP"), start, vetture, out List<Vettura> CisSeqDCKSUP);
                //start = Debug_GetCisInBuffer(10, start, vetture, out List<Vettura> CisSeqBufferCH1_DCKSUP);
                //start = Debug_GetCisInLinea(GetTrattoFrontEnd("CH2"), start, vetture, out List<Vettura> CisSeqCH2);

                //start = Debug_GetCisInBuffer(10, start, vetture, out List<Vettura> CisSeqBufferDCKSUP_CH2);
                //start = Debug_GetCisInLinea(GetTrattoFrontEnd("GLZ"), start, vetture, out List<Vettura> CisSeqGLZ);

                //start = Debug_GetCisInBuffer(10, start, vetture, out List<Vettura> CisSeqBufferCH2_GLZ);
                //start = Debug_GetCisInLinea(GetTrattoFrontEnd("CH3"), start, vetture, out List<Vettura> CisSeqCH3);

                //start = Debug_GetCisInBuffer(10, start, vetture, out List<Vettura> CisSeqBufferGLZCH3);
                //start = Debug_GetCisInLinea(GetTrattoFrontEnd("FN1"), start, vetture, out List<Vettura> CisSeqFN1);

                //start = Debug_GetCisInBuffer(10, start, vetture, out List<Vettura> CisSeqBufferCH3FN1);
                //start = Debug_GetCisInLinea(GetTrattoFrontEnd("FN2"), start, vetture, out List<Vettura> CisSeqFN2);

                //start = Debug_GetCisInBuffer(10, start, vetture, out List<Vettura> CisSeqBufferFN1FN2);
                //start = Debug_GetCisInBuffer(25, start, vetture, out List<Vettura> CisSeqBufferTestinline);

                //log.Debug("caricamento Sub-Linee");

                ////
                //// Subline
                ////
                //start = Debug_GetCisInLinea(GetTrattoFrontEnd("DOO"), start, vetture, out List<Vettura> CisSeqDOO);
                //start = Debug_GetCisInBuffer(10, start, vetture, out List<Vettura> CisSeqBufferDOOFN1);

                //start = Debug_GetCisInLinea(GetTrattoFrontEnd("DSB"), start, vetture, out List<Vettura> CisSeqDSB);
                //start = Debug_GetCisInBuffer(10, start, vetture, out List<Vettura> CisSeqBufferDSBTR2);

                //start = Debug_GetCisInLinea(GetTrattoFrontEnd("FRE"), start, vetture, out List<Vettura> CisSeqFRE);
                //start = Debug_GetCisInBuffer(10, start, vetture, out List<Vettura> CisSeqBufferFRECH2);

                //start = Debug_GetCisInLinea(GetTrattoFrontEnd("PRT"), start, vetture, out List<Vettura> CisSeqPRT);
                //start = Debug_GetCisInBuffer(10, start, vetture, out List<Vettura> CisSeqBufferPRTCH2);


                //log.Debug("caricamento Meccaniche");

                ////
                //// Meccanica
                ////
                //start = Debug_GetCisInLinea(GetTrattoFrontEnd("UMC"), start, vetture, out List<Vettura> CisSeqUMC);
                //start = Debug_GetCisInLinea(GetTrattoFrontEnd("DSP"), start, vetture, out List<Vettura> CisSeqDSP);
                //start = Debug_GetCisInBuffer(10, start, vetture, out List<Vettura> CisSeqBufferUMCDSP);

                //start = Debug_GetCisInLinea(GetTrattoFrontEnd("GOMA"), start, vetture, out List<Vettura> CisSeqGOMA);
                //start = Debug_GetCisInBuffer(10, start, vetture, out List<Vettura> CisSeqBufferDSPGOMA);

                //start = Debug_GetCisInLinea(GetTrattoFrontEnd("GOMP"), start, vetture, out List<Vettura> CisSeqGOMP);
                //start = Debug_GetCisInLinea(GetTrattoFrontEnd("DCKINF"), start, vetture, out List<Vettura> CisSeqDCKINF);
                //start = Debug_GetCisInBuffer(10, start, vetture, out List<Vettura> CisSeqBufferGOMPDCKINF);


                tempResult.Append("{\"trim1\":");
                tempResult.Append(JsonConvert.SerializeObject(CisSeqTrim1, Newtonsoft.Json.Formatting.Indented));
                tempResult.Append(",\"trim2\":");
                tempResult.Append(JsonConvert.SerializeObject(CisSeqTrim2, Newtonsoft.Json.Formatting.Indented));
                tempResult.Append("}");

                result = tempResult.ToString();
            }
            catch (Exception ex)
            {
                log.Error(ex);
                result = "{\"EquipmentList\":[],\"ErrorList\":[\"" + ex.Message + "\"]}";
            }

            var response = Request.CreateResponse(HttpStatusCode.OK);
            response.Content = new StringContent(result, System.Text.Encoding.UTF8, "application/json");
            return response;
        }


        private int Debug_GetCisInLinea(A_TrattiFrontEnd tratto, int start, List<Vettura> vetture, List<Anomalia> anomalie, out List<Vettura> vettureInLinea)
        {
            List<A_Stazioni> StazioniTratto = DbQueries.PlantModel.GetStazioni(tratto);

            vettureInLinea = new List<Vettura>();
            int offset = start;
            int localIndex = 0;
            while (offset < tratto.CountStazioni + start)
            {
                if (offset >= tratto.CountStazioni + start)
                {
                    break;
                }
                vetture[offset].Stazione = StazioniTratto.ElementAt(localIndex).Codice;
                vettureInLinea.Add(vetture[offset]);
                List<Anomalia> anomalieVettura = new List<Anomalia>();
                anomalieVettura = anomalie.Where(a => a.Cis == vetture[offset].Cis).ToList();

                Anomalia testanomalia = new Anomalia();
                if (offset == tratto.CountStazioni + start - 7)
                {
                    testanomalia.Tesis = "test";
                    testanomalia.Descrizione = "testdescr";
                    testanomalia.Cis = "testcis";
                    anomalieVettura.Add(testanomalia);
                }


                vetture[offset].ListaAnomalia = anomalieVettura;
                offset++;
                localIndex++;
            }
            return offset;
        }

        //private int  Debug_GetCisInBuffer(A_TrattiFrontEnd tratto, int start, List<Vettura> vetture, out List<Vettura> vettureInBuffer)
        //{
        //    return Debug_GetCisInLinea(tratto, start, vetture, out vettureInBuffer);

        //}




    }
}
