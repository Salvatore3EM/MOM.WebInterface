using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MOM.WebInterface.Models.Assembly
{
    public class Anomalia
    {

        [JsonProperty("tesis")]
        public string Tesis { get; set; }

        [JsonProperty("descrizione")]
        public string Descrizione { get; set; }

        [JsonProperty("cis")]
        public string Cis { get; set; }

        public int Anomalia_trim { get; set; }
        
        public int Anomalia_mecc { get; set; }

        public int Anomalia_ch1 { get; set; }

        public int Anomalia_ch2 { get; set; }

        public int Anomalia_ch3 { get; set; }

        public int Anomalia_doo { get; set; }

        public int Anomalia_dsb { get; set; }

        public int Anomalia_fre { get; set; }

        public int Anomalia_umc { get; set; }

        public int Anomalia_drs { get; set; }

        public int Anomalia_goma { get; set; }

        public int Anomalia_gomp { get; set; }

        public int Anomalia_gra { get; set; }

        public int Anomalia_tra { get; set; }

        public int Anomalia_grp { get; set; }

        public int Anomalia_trp { get; set; }

        public int Anomalia_DCKSUP { get; set; }

        public int Anomalia_prt { get; set; }


        public string WPAopen { get; set; }


    }


}