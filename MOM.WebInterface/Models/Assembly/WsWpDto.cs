using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MOM.WebInterface.Models.Assembly
{
    public class WsWpDto
    {
        /// <summary>
        /// Codice Tratto - da configurazione
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// Codice della Stazione (Workstation) 
        /// </summary>
        public string CodiceStazione { get; set; }
        /// <summary>
        /// Codice Stazione nella forma: {Codice_Tratto_da_Config}_{numero_progressivo}
        /// <para>è un codice ricavato in modo empirico - non è affidabile</para>>
        /// </summary>
        public string CodiceStazioneBreve { get; set; }
        /// <summary>
        /// p.k. della Workplace nella tabella A_StandardWorkplace
        /// </summary>
        public int WorkplacePK { get; set; }
        /// <summary>
        /// Codice Alfanumerico della Workplace
        /// </summary>
        public string Workplace { get; set; }
        /// <summary>
        /// Descrizione della Workplace
        /// </summary>
        public string WorkplaceDescription { get; set; }

    }
}