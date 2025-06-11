using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MOM.WebInterface.Models.DTO {
    public class PlantModelTreeDto {

         //Level ParentId ObjectId EquipmentPath EquipmentPathDescription EquipmentDescription

        public int EquipmentId { get; set; }
        public int ParentId { get; set; }
        public int Level { get; set; }
        public int IdTable { get; set; }
        public string EquipmentPath { get; set; } = "EquipmentPath";
        public string ObjectTypeId { get; set; }
        public string EquipmentDescription { get; set; }
        public string EquipmentPathDescription { get; set; } = "EquipmentPathDescription";
        public int ObjectId { get; set; } = 99;
        public List<PlantModelTreeDto> Children { get; set; } = new List<PlantModelTreeDto>();
    }

    public class PMTDto : PlantModelTreeDtoBase
    {
        public string EquipmentPath { get; set; } = "EquipmentPath";
        public string EquipmentPathDescription { get; set; } = "EquipmentPathDescription";
    }


    public class PlantModelTreeDtoBase
    {
        /*
        [IdEquipment]       INT IDENTITY(1,1), --ROW_NUMBER() OVER (ORDER BY newId())    [IdEquipment] INT,
        [IdParent]          INT,    -- IdEquipment "padre" nella gerarchia
        [Level]             INT,    -- livello nella gerarchia
        [CountChildren]     INT,    -- numero di nodi figlio (calcolato alla fine della S.P.)
        [Table]             NVARCHAR(50) default '',        -- Tabella di provenienza dell'oggetto (Tratto, Linea, ecc...)
        [IdTable]           INT,                            -- P.K. dell'oggetto (Tratto, Linea, ecc...) nella tabella dal quale è stato estratto
        [Codice]        NVARCHAR(50) null default '',   -- Codice dell'oggetto
        [DisplayName]   NVARCHAR(50) null default '',   -- Cosa mostrare a video
        [Descrizione]   NVARCHAR(250) null default ''   -- Descrizione dell'oggetto
         */

        /// <summary>
        /// Id della gerarchia
        /// </summary>
        [JsonProperty("EquipmentId")]
        public int IdEquipment { get; set; }

        /// <summary>
        /// Id del superiore gerarchico (solo se >0 - se <=0/null allora è radice di albero)
        /// <para>se >0 indica <see cref="EquipmentId"/> padre</para>
        /// <para>se <=0/null indica che l'Equipment è radice<see cref="EquipmentId"/> padre</para>
        /// </summary>
        [JsonProperty("ParentId")]
        public int? IdParent { get; set; }
        public int Level { get; set; }

        /// <summary>
        /// numero di "figli" nella gerarchia
        /// </summary>
        public int CountChildren { get; set; }

        /// <summary>
        /// tabella di provenienza dell'Equipment
        /// </summary>
        public string Table { get; set; }

        /// <summary>
        /// Id dell'Equipment nella propria tabella - <see cref="Table"/>
        /// </summary>
        [JsonProperty("ObjectId")]
        public int IdTable { get; set; }
        public string Codice { get; set; }
        public string DisplayName { get; set; }

        /// <summary>
        /// Descrizione dell'Equipment
        /// </summary>
        public string Descrizione { get; set; }
        public List<EquipmentDto> Children { get; set; } = new List<EquipmentDto>();
    }

}