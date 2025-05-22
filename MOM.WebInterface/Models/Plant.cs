using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MOM.WebInterface.Models
{

    public class DictionaryPlant
    {
        public Dictionary<string, PlantLinea> Linea = null;
        public DictionaryPlant(Plant plant)
        {
            if (plant != null)
            {
                foreach (PlantLinea l in plant.Linea)
                {
                    Linea.Add(l.nomeLinea, l);
                }
            }
        }
    }





    // NOTE: Generated code may require at least .NET Framework 4.5 or .NET Core/Standard 2.0.
    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
    public partial class Plant
    {

        private PlantLinea[] lineaField;

        private string nomePlantField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Linea")]
        public PlantLinea[] Linea
        {
            get
            {
                return this.lineaField;
            }
            set
            {
                this.lineaField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string nomePlant
        {
            get
            {
                return this.nomePlantField;
            }
            set
            {
                this.nomePlantField = value;
            }
        }
    }





    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class PlantLinea
    {

        private string nomeLineaField;

        private byte indiceLineaField;

        private string workplaceField;

        private byte postazioniField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string nomeLinea
        {
            get
            {
                return this.nomeLineaField;
            }
            set
            {
                this.nomeLineaField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public byte IndiceLinea
        {
            get
            {
                return this.indiceLineaField;
            }
            set
            {
                this.indiceLineaField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string workplace
        {
            get
            {
                return this.workplaceField;
            }
            set
            {
                this.workplaceField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public byte postazioni
        {
            get
            {
                return this.postazioniField;
            }
            set
            {
                this.postazioniField = value;
            }
        }
    }



}


