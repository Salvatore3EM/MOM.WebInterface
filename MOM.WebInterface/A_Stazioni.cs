//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MOM.WebInterface
{
    using System;
    using System.Collections.Generic;
    
    public partial class A_Stazioni
    {
        public int IdStazione { get; set; }
        public string Codice { get; set; }
        public string Descrizione { get; set; }
        public Nullable<short> Abilitato { get; set; }
        public Nullable<short> Cancellato { get; set; }
        public Nullable<System.DateTime> DataOraCancellato { get; set; }
        public Nullable<int> IdTratto { get; set; }
        public string CodTratto { get; set; }
        public Nullable<int> Ordine { get; set; }
        public Nullable<int> NStazioni { get; set; }
        public Nullable<int> Linee { get; set; }
        public Nullable<bool> automaticAlertCreation { get; set; }
        public Nullable<bool> IsNPL { get; set; }
        public Nullable<short> IdSRC { get; set; }
    }
}
