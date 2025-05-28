using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MOM.WebInterface.Models.Assembly
{
    public class LineaDto
    {
        public string Nome { get; set; }

        public byte Indice{ get; set; }

        public string Workplace{ get; set; }

        public byte Postazioni{ get; set; }
    }
}