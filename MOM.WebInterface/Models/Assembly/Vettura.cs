using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MOM.WebInterface.Models.Assembly
{
    public class Vettura
    {
        /// <summary>
        /// codice della stazione - es.: WSA_TR1_006
        /// </summary>
        [JsonProperty("stazione")]
        public string Stazione { get; set; }


        [JsonProperty("sequenza")]
        public int Sequenza { get; set; }

        [JsonProperty("cis")]
        public string Cis { get; set; }

        [JsonProperty("modello")]
        public string Modello { get; set; }

        [JsonProperty("motorizzazione")]
        public string Motorizzazione { get; set; }

        [JsonProperty("telaio")]
        public string Telaio { get; set; }


        private string allestimento = "";

        [JsonProperty("allestimento")]
        public string Allestimento 
        {
            get { return allestimento; }
            set
            {
                if (value.StartsWith("9"))
                {
                    allestimento = "SI";
                }
                else if (value.Contains(value)) // ***max*** capire questa cagata
                {
                    allestimento = value;
                }
                else
                {
                    allestimento = "NO";
                }
            } 
        }


        [JsonProperty("tipoMotore")]
        public string TipoMotore { get; set; }

        [JsonProperty("modelloRaggruppato")]
        public string ModelloRaggruppato { get; set; }

        [JsonProperty("mercato")]
        public string Mercato { get; set; }

        [JsonProperty("anomalie")]
        public List<Anomalia> ListaAnomalia {  get; set; }






        //mainLine

        #region ' Main Line '

        public List<Anomalia> listaDifettiTrim()
        {
            List<Anomalia> listaDifetti = new List<Anomalia>();
            foreach (Anomalia a in ListaAnomalia)
            {
                if (a.Anomalia_trim == 1)
                {
                    listaDifetti.Add(a);
                }
            }
            return listaDifetti;
        }

        public List<Anomalia> listaDifettiCh1()
        {
            List<Anomalia> listaDifetti = new List<Anomalia>();
            foreach (Anomalia a in ListaAnomalia)
            {
                if (a.Anomalia_ch1 == 1)
                {

                    listaDifetti.Add(a);
                }
            }
            return listaDifetti;
        }

        public List<Anomalia> listaDifettiCh2()
        {
            List<Anomalia> listaDifetti = new List<Anomalia>();
            foreach (Anomalia a in ListaAnomalia)
            {
                if (a.Anomalia_ch2 == 1)
                {

                    listaDifetti.Add(a);
                }
            }
            return listaDifetti;
        }

        public List<Anomalia> listaDifettiCh3()
        {
            List<Anomalia> listaDifetti = new List<Anomalia>();
            foreach (Anomalia a in ListaAnomalia)
            {
                if (a.Anomalia_ch3 == 1)
                {

                    listaDifetti.Add(a);
                }
            }
            return listaDifetti;
        }

        public List<Anomalia> listaDifettiDCKSUP()
        {
            List<Anomalia> listaDifetti = new List<Anomalia>();
            foreach (Anomalia a in ListaAnomalia)
            {
                if (a.Anomalia_DCKSUP == 1)
                {

                    listaDifetti.Add(a);
                }
            }
            return listaDifetti;
        }

        #endregion



        //subline

        #region ' Sub-Line '

        public List<Anomalia> listaDifettiDoo()
        {
            List<Anomalia> listaDifetti = new List<Anomalia>();
            foreach (Anomalia a in ListaAnomalia)
            {
                if (a.Anomalia_doo == 1)
                {

                    listaDifetti.Add(a);
                }
            }
            return listaDifetti;
        }

        public List<Anomalia> listaDifettiDsb()
        {
            List<Anomalia> listaDifetti = new List<Anomalia>();
            foreach (Anomalia a in ListaAnomalia)
            {
                if (a.Anomalia_dsb == 1)
                {

                    listaDifetti.Add(a);
                }
            }
            return listaDifetti;
        }

        public List<Anomalia> listaDifettiFre()
        {
            List<Anomalia> listaDifetti = new List<Anomalia>();
            foreach (Anomalia a in ListaAnomalia)
            {
                if (a.Anomalia_fre == 1)
                {

                    listaDifetti.Add(a);
                }
            }
            return listaDifetti;
        }

        public List<Anomalia> listaDifettiPrt()
        {
            List<Anomalia> listaDifetti = new List<Anomalia>();
            foreach (Anomalia a in ListaAnomalia)
            {
                if (a.Anomalia_prt == 1)
                {

                    listaDifetti.Add(a);
                }
            }
            return listaDifetti;
        }

        public List<Anomalia> listaDifettiCh2Tot()
        {

            List<Anomalia> listaDifetti = new List<Anomalia>();
            foreach (Anomalia a in ListaAnomalia)
            {
                if (a.Anomalia_prt == 1 || a.Anomalia_fre == 1 || a.Anomalia_ch2 == 1)
                {
                    listaDifetti.Add(a);
                }
            }
            return listaDifetti;
        }

        #endregion



        //Meccaniche

        #region ' Meccaniche '

        public List<Anomalia> listaDifettiUmc()
        {
            List<Anomalia> listaDifetti = new List<Anomalia>();
            foreach (Anomalia a in ListaAnomalia)
            {
                if (a.Anomalia_umc == 1)
                {

                    listaDifetti.Add(a);
                }
            }
            return listaDifetti;
        }

        public List<Anomalia> listaDifettiDrp()
        {
            List<Anomalia> listaDifetti = new List<Anomalia>();
            foreach (Anomalia a in ListaAnomalia)
            {
                if (a.Anomalia_drs == 1)
                {

                    listaDifetti.Add(a);
                }
            }
            return listaDifetti;
        }

        public List<Anomalia> listaDifettiGoma()
        {
            List<Anomalia> listaDifetti = new List<Anomalia>();
            foreach (Anomalia a in ListaAnomalia)
            {
                if (a.Anomalia_goma == 1)
                {

                    listaDifetti.Add(a);
                }
            }
            return listaDifetti;
        }

        public List<Anomalia> listaDifettiGomp()
        {
            List<Anomalia> listaDifetti = new List<Anomalia>();
            foreach (Anomalia a in ListaAnomalia)
            {
                if (a.Anomalia_gomp == 1)
                {

                    listaDifetti.Add(a);
                }
            }
            return listaDifetti;
        }
        public List<Anomalia> listaDifettiDCkInf()
        {
            List<Anomalia> listaDifetti = new List<Anomalia>();
            foreach (Anomalia a in ListaAnomalia)
            {
                if (a.Anomalia_mecc == 1)
                {

                    listaDifetti.Add(a);
                }
            }
            return listaDifetti;
        }

        #endregion

    }


}