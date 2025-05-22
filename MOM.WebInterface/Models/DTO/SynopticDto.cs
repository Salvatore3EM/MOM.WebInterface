using System.Collections.Generic;

namespace MOM.WebInterface.Models.DTO
{
    public class SynopticDataDto
    {
        public List<SynopticCellaDto> Trim1 { get; set; }
        public List<SynopticCellaDto> Trim2 { get; set; }
    }

    public class SynopticCellaDto
    {
        public int Tableid { get; set; }
        public string Cis { get; set; }
        public string Modello { get; set; }
        public string Telaio { get; set; }
        public List<AnomaliaDto> Anomalie { get; set; }
    }

    public class AnomaliaDto
    {
        public string Descrizione { get; set; }
    }
}
