using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;
using log4net;
using Microsoft.Ajax.Utilities;
using MOM.WebInterface.Models.Assembly;
using MOM.WebInterface.Models;
using MOM.WebInterface.App_DB;

namespace MOM.WebInterface.App
{
    public class OpState
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(typeof(OpState));



        private List<Vettura> listaTotaleCis = new List<Vettura>();
        private List<Vettura> listaVettureTrim1 = new List<Vettura>();
        private List<Vettura> listaVettureTrim2 = new List<Vettura>();
        private List<Vettura> listaVettureCH1 = new List<Vettura>();
        private List<Vettura> listaVettureDCKSUP = new List<Vettura>();
        private List<Vettura> listaVettureCH2 = new List<Vettura>();
        private List<Vettura> listaVettureGLZ = new List<Vettura>();
        private List<Vettura> listaVettureCH3 = new List<Vettura>();
        private List<Vettura> listaVettureFN1 = new List<Vettura>();
        private List<Vettura> listaVettureFN2 = new List<Vettura>();
        private List<Vettura> listaVettureDOO = new List<Vettura>();
        private List<Vettura> listaVettureDSB = new List<Vettura>();
        private List<Vettura> listaVettureFRE = new List<Vettura>();
        private List<Vettura> listaVetturePRT = new List<Vettura>();
        private List<Vettura> listaVettureUMC = new List<Vettura>();
        private List<Vettura> listaVettureDSP = new List<Vettura>();
        private List<Vettura> listaVettureGOMA = new List<Vettura>();
        private List<Vettura> listaVettureGOMP = new List<Vettura>();
        private List<Vettura> listaVettureDCKINF = new List<Vettura>();
        //Buffer main line
        private List<Vettura> listaVettureBuffer_tr1tr2 = new List<Vettura>();
        private List<Vettura> listaVettureBuffer_tr2Ch1 = new List<Vettura>();
        private List<Vettura> listaVettureBuffer_Ch1DCKSUP = new List<Vettura>();
        private List<Vettura> listaVettureBuffer_DCKSUP_CH2 = new List<Vettura>();
        private List<Vettura> listaVettureBuffer_CH2GLZ = new List<Vettura>();
        private List<Vettura> listaVettureBuffer_GLZCH3 = new List<Vettura>();
        private List<Vettura> listaVettureBuffer_CH3FN1 = new List<Vettura>();
        private List<Vettura> listaVettureBuffer_FN1FN2 = new List<Vettura>();
        private List<Vettura> listaVettureBuffer_TestInLine = new List<Vettura>();

        //Buffer Meccanica
        private List<Vettura> listaVettureBuffer_UMCDSP = new List<Vettura>();
        private List<Vettura> listaVettureBuffer_DSPGOMA = new List<Vettura>();
        private List<Vettura> listaVettureBuffer_GOMAGOMP = new List<Vettura>();
        private List<Vettura> listaVettureBuffer_GOMPDCKINF = new List<Vettura>();

        //Buffer subline
        private List<Vettura> listaVettureBuffer_DOORFN1 = new List<Vettura>();
        private List<Vettura> listaVettureBuffer_DSHTR2 = new List<Vettura>();
        private List<Vettura> listaVettureBuffer_FRECH2 = new List<Vettura>();
        private List<Vettura> listaVettureBuffer_PRTCH2 = new List<Vettura>();


        private List<Anomalia> listaTotaleAnomalie = new List<Anomalia>();
        private DateTime dataRif = new DateTime(2021, 10, 1, 0, 0, 0);
        private DateTime dataRifTot = new DateTime(2021, 12, 1);

        public List<Anomalia> ListaTotaleAnomalie { get => listaTotaleAnomalie; set => listaTotaleAnomalie = value; }

        private string valoreAllestimento = "";
        public string ValoreAllestimento { get => valoreAllestimento; set => valoreAllestimento = value; }
        //private Plant p;
        //private DictionaryPlant Linee;


        // step.1

        //Test con doppia connessione a due DB
        public List<Anomalia> CaricaAnomalie()
        {
            try
            {
                BusinessService_DBEntities db = new BusinessService_DBEntities();

                var query = from a in db.F_Anomalie_Aperte2("")
                            select new Anomalia { 
                                Tesis = a.TESIS,
                                Descrizione=a.TESISDESCRIPTION,
                                Cis = a.CISNUM,
                                Anomalia_trim = a.ANOMALIA_TRIM,
                                Anomalia_mecc = a.ANOMALIA_MECC,
                                Anomalia_ch1 = a.ANOMALIA_CH1,
                                Anomalia_ch2 = a.ANOMALIA_CH2,
                                Anomalia_ch3 = a.ANOMALIA_CH3,
                                Anomalia_doo = a.ANOMALIA_DOO,
                                Anomalia_dsb = a.ANOMALIA_DSB,
                                Anomalia_fre=a.ANOMALIA_FEM,
                                Anomalia_umc=a.ANOMALIA_UMC,
                                Anomalia_drs=a.ANOMALIA_DRS,
                                Anomalia_goma=a.ANOMALIA_GOMA,
                                Anomalia_gomp=a.ANOMALIA_GOMP,
                                Anomalia_tra=a.ANOMALIA_TRA,
                                Anomalia_gra=a.ANOMALIA_GRA,
                                Anomalia_grp = a.ANOMALIA_GRP,
                                Anomalia_trp = a.ANOMALIA_TRP,
                                Anomalia_DCKSUP = a.ANOMALIA_DCKUP,
                                Anomalia_prt = a.ANOMALIA_PRT,
                                WPAopen = a.WORKPLACE_OPEN
                            };
                return query.ToList();
            }
            catch (Exception e)
            {
                Console.Write(e);
                //this.caricaAnomalie();
            }
            return null;
        }


        // step.2 

        public List<Vettura> CaricaTutto()
        {
            BusinessService_DBEntities db = new BusinessService_DBEntities();

            var query = from item in db.List_Cis_D_R()
                        select new Vettura
                        {
                            Cis = item.CIS,
                            Sequenza = int.Parse(item.NUMSEQ.ToString()),
                            Modello = item.Modello_Anag,
                            Motorizzazione = item.Descr_Motore,
                            Telaio = item.NUMTEL,
                            TipoMotore = item.MOTORIZ,
                            ModelloRaggruppato = item.MODELLO,
                            Mercato = item.MERCATO,
                            Allestimento = item.AllestSpeciale
                        };

            List<Vettura> listCisDR = query.ToList();

            foreach (Vettura vettura in listCisDR)
            {
                vettura.ListaAnomalia = listaTotaleAnomalie.Where(x => x.Cis ==  vettura.Cis).ToList();
            }



            // aggiunta degli elementi del tratto R-E
            var query1 = from v in db.List_Cis_R_E()
                         select new Vettura
                         {
                             Cis = v.CIS,
                             Sequenza = int.Parse(v.NUMSEQ.ToString()),
                             Modello = v.Modello_Anag,
                             Motorizzazione = v.Descr_Motore,
                             Telaio = v.NUMTEL,
                             TipoMotore = v.MOTORIZ,
                             ModelloRaggruppato = v.MODELLO,
                             Mercato = v.MERCATO,
                             Allestimento = v.AllestSpeciale
                         };

            List<Vettura> listCisRE = query1.Take(6).ToList();

            foreach (Vettura vettura in listCisRE)
            {
                vettura.ListaAnomalia = listaTotaleAnomalie.Where(x => x.Cis == vettura.Cis).ToList();
            }



            if (listCisRE != null)
            {
                listCisDR.AddRange(listCisRE);
            }
            return listCisDR;
        }


        public int GetTotaleAnonalie(List<Vettura> vetture)
        {
            int totale = vetture
                .Where(item => (item.ListaAnomalia != null) && ( item.ListaAnomalia.Count>0))
                .Sum(item => item.ListaAnomalia.Count);
            return totale;
        }




        /// <summary>
        /// restituisce l'elenco dei CIS nella <paramref name="linea"> indicata
        /// </summary>
        /// <param name="linea"></param>
        /// <param name="Vetture"></param>
        /// <returns></returns>
        public List<Vettura> GetCisInTratto(A_TrattiFrontEnd tratto, List<Vettura> Vetture)
        {
            List<Vettura> result = null;
            try
            {
                BusinessService_DBEntities db = new BusinessService_DBEntities();

                List<SP_F_TOP_TRANSITO_Result> F_TOP_TRANSITO = db.SP_F_TOP_TRANSITO(tratto.FirstWorkplace).ToList();

                List<A_Stazioni> StazioniTratto = DbQueries.PlantModel.GetStazioni(tratto);


                int indice = -1;

                if (Vetture.Count > 0)
                {
                    result = new List<Vettura>();

                    for (int j = 0; j < Vetture.Count; j++)
                    {
                        if (Vetture[j].Sequenza == F_TOP_TRANSITO[0].Sequenza)
                        {
                            indice = j;
                            break;
                        }
                    }

                    if (indice >-1)
                    {
                        int Offset = indice;
                        //numero postazioni = linea.Postazioni (da config)
                        for (int i = indice; i <= indice + tratto.CountStazioni; i++)
                        {
                            Vetture.ElementAt(i).Stazione = StazioniTratto.ElementAt(i - Offset).Codice;
                            result.Add(Vetture.ElementAt(i));
                        }
                    }
                }
            }
            catch (SqlException e)
            {
                StringBuilder errorMessages = new StringBuilder();
                for (int i = 0; i < e.Errors.Count; i++)
                {
                    errorMessages.Append("Index #" + i + "\n" +
                        "Message: " + e.Errors[i].Message + "\n" +
                        "Error Number: " + e.Errors[i].Number + "\n" +
                        "LineNumber: " + e.Errors[i].LineNumber + "\n" +
                        "Source: " + e.Errors[i].Source + "\n" +
                        "Procedure: " + e.Errors[i].Procedure + "\n");
                }
                log.Error($"{tratto.Code} Exception - " + errorMessages);
            }
            catch (Exception ex)
            {
                log.Error($"ERROR Tratto: {tratto.Code} {tratto.FirstWorkplace}");
                log.Error(ex);
            }

            return result;
        }


        /// <summary>
        /// restituisce l'elenco dei CIS tra le due linee <paramref name="ListaVettureMonte"/> e <paramref name="ListaVettureValle"/>
        /// </summary>
        /// <param name="ListaVettureMonte">Sequenza di CIS della "Linea Precedente"</param>
        /// <param name="ListaVettureValle">Sequenza di CIS della "Linea Successiva"</param>
        /// <param name="Vetture">elenco completo di tutte le vetture al Montaggio</param>
        /// <returns></returns>
        public List<Vettura> GetCisInBuffer( List<Vettura> ListaVettureMonte,  List<Vettura> ListaVettureValle,  List<Vettura> Vetture)
        {
            int SeqMax = ListaVettureMonte.Max(a => a.Sequenza);
            int SeqMin = ListaVettureValle.Min(a => a.Sequenza);
            List<Vettura> ListCisBuffer = Vetture.Where(a => SeqMin < a.Sequenza && a.Sequenza < SeqMax).ToList();
            return ListCisBuffer;
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="ListaVettureMonte"></param>
        /// <param name="ListaVettureValle"></param>
        /// <param name="Vetture"></param>
        /// <returns></returns>
        public List<Vettura> GetCisInBuffer(string Buffer,  List<Vettura> ListaVettureMonte,  List<Vettura> ListaVettureValle,  List<Vettura> Vetture)
        {
            Vettura vMin = null;
            Vettura vMax = null;
            Buffer = Buffer.Trim().ToLower(culture: new CultureInfo("en-US", false));
            switch(Buffer)
            {
                case "doo-fn1":
                    vMin = ListaVettureValle[10];
                    vMax = ListaVettureMonte[ListaVettureMonte.Count - 1];
                    break;

                case "dsb-tr2":
                    vMin = ListaVettureValle[7];
                    vMax = ListaVettureMonte[ListaVettureMonte.Count - 1];
                    break;

                case "fre-ch2":
                    vMin = ListaVettureValle[14];
                    vMax = ListaVettureMonte[ListaVettureMonte.Count - 1];
                    break;

                case "prt-ch2":
                    vMin = ListaVettureValle[20];
                    vMax = ListaVettureMonte[ListaVettureMonte.Count - 1];
                    break;

                case "umc-dsp":
                    vMin = ListaVettureValle[0];
                    vMax = ListaVettureMonte[ListaVettureMonte.Count - 1];
                    break;

                case "dsp-goma":
                    vMin = ListaVettureValle[0];
                    vMax = ListaVettureMonte[ListaVettureMonte.Count - 1];
                    break;

                case "gomp-dckinf":
                    vMin = ListaVettureValle[0];
                    vMax = ListaVettureMonte[ListaVettureMonte.Count - 1];
                    break;
            }

            List<Vettura> ListCisBuffer = null;
            if ((vMin != null) && (vMax != null))
            {
                ListCisBuffer = Vetture.Where(a => vMin.Sequenza < a.Sequenza && a.Sequenza < vMax.Sequenza).ToList();
            }
            return ListCisBuffer;
        }

        public List<Vettura> GetCisInBuffer_DOO_FN1(ref List<Vettura> VettureDOO, ref List<Vettura> VettureFN1, ref List<Vettura> Vetture)
        {

            Vettura vFn1 = VettureFN1[10];
            Vettura vDoo = VettureDOO[VettureDOO.Count -1];

            List<Vettura> result = Vetture.Where(x => (vFn1.Sequenza < x.Sequenza) && (x.Sequenza < vDoo.Sequenza)).ToList();
            return result;
        }

        public List<Vettura> GetCisInBuffer_DSB_TR2(ref List<Vettura> VettureDSB, ref List<Vettura> VettureTR2, ref List<Vettura> Vetture)
        {
            Vettura vTr2 = VettureTR2[7];
            Vettura vDsb = VettureDSB[VettureDSB.Count - 1];

            List<Vettura> result = Vetture.Where(x => (vTr2.Sequenza < x.Sequenza) && (x.Sequenza < vDsb.Sequenza)).ToList();
            return result;
        }

        
        public List<Vettura> GetCisInBuffer_FRE_CH2(ref List<Vettura> VettureFRE, ref List<Vettura> VettureCH2, ref List<Vettura> Vetture)
        {
            Vettura vCh2 = VettureCH2[14];
            Vettura vFre = VettureFRE[VettureFRE.Count - 1];

            List<Vettura> result = Vetture.Where(x => (vCh2.Sequenza < x.Sequenza) && (x.Sequenza < vFre.Sequenza)).ToList();
            return result;
        }




        public List<Vettura> GetCisTestInLine()
        {
            List<Vettura> result = null;
            try
            {
                BusinessService_DBEntities db = new BusinessService_DBEntities();

                var query = from item in db.List_Cis_R_E()
                            select new Vettura
                            {
                                Cis = item.CIS,
                                Sequenza = int.Parse(item.NUMSEQ.ToString()),
                                Modello = item.Modello_Anag,
                                Motorizzazione = item.Descr_Motore,
                                Telaio = item.NUMTEL,
                                TipoMotore = item.MOTORIZ,
                                ModelloRaggruppato = item.MODELLO,
                                Mercato = item.MERCATO,
                                Allestimento = item.AllestSpeciale,
                                ListaAnomalia = listaTotaleAnomalie.Where(p => p.Cis == item.CIS).ToList()
                            };

                result = query.ToList();

                //List<Vettura> result = query.ToList();

                //foreach (Vettura vettura in listCisRE)
                //{
                //    vettura.ListaAnomalia = listaTotaleAnomalie.Where(x => x.Cis == vettura.Cis).ToList();
                //}

            }
            catch (SqlException e)
            {
                StringBuilder errorMessages = new StringBuilder();
                for (int i = 0; i < e.Errors.Count; i++)
                {
                    errorMessages.Append("Index #" + i + "\n" +
                        "Message: " + e.Errors[i].Message + "\n" +
                        "Error Number: " + e.Errors[i].Number + "\n" +
                        "LineNumber: " + e.Errors[i].LineNumber + "\n" +
                        "Source: " + e.Errors[i].Source + "\n" +
                        "Procedure: " + e.Errors[i].Procedure + "\n");
                }
                Console.WriteLine($"CIS Test in Line Exception - " + errorMessages);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
            }

            return result;
        }


    }


}
