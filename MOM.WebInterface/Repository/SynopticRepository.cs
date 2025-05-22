using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using MOM.WebInterface.App_DB;
using MOM.WebInterface.Models;
using MOM.WebInterface.Models.DTO;

namespace MOM.WebInterface.Repository {
    public class SynopticRepository  {
        private readonly BusinessService_DBEntities _context;

        public SynopticRepository(BusinessService_DBEntities context)
        {
            _context = context;
        }

        public async Task<List<SynopticLayouts>> GetSynopticListAsync()
        {
            return await _context.SynopticLayouts.ToListAsync();
        }
        public async Task<SynopticLayouts> GetSynopticAsync(string layout)
        {
            return await _context.SynopticLayouts.FindAsync(layout);
        }

        public async Task<bool> InsertSynopticAsync(SynopticLayouts synopticLayout)
        {
            // Check if the synoptic already exists
            var existing = await _context.SynopticLayouts.FindAsync(synopticLayout.Layout);
            if (existing != null)
            {
                return false; // Synoptic already exists
            }

            _context.SynopticLayouts.Add(synopticLayout);
            int result = await _context.SaveChangesAsync();
            return result > 0;
        }
        public async Task<bool> UpdateSynopticAsync(SynopticLayouts synopticLayout)
        {
            // Find the existing synoptic
            var existing = await _context.SynopticLayouts.FindAsync(synopticLayout.Layout);
            if (existing == null)
            {
                return false; // Synoptic doesn't exist
            }

            // Update the properties
            existing.AreaId = synopticLayout.AreaId;
            existing.Svg = synopticLayout.Svg;
            existing.SvgBackup = synopticLayout.SvgBackup;

            int result = await _context.SaveChangesAsync();
            return result > 0;
        }

        public List<PlantModelTreeDto> GetPlantModelTreeTreeAsync()
        {
            // Fetch all plant model data
            List<PlantModelTreeDto> PlantModelTreeData = GetTree();
            List<PlantModelTreeDto> result = Utility.GetEquipmentsTree(ref PlantModelTreeData);
            // Convert to DTO and build hierarchy
            return result;
        }

        internal List<PlantModelTreeDto> GetTree()
        {
            
            using (var context = new BusinessService_DBEntities())
            {
                //var clientIdParameter = new SqlParameter("@ClientId", 4);
                //var result = context.Database
                //    .SqlQuery<ResultForCampaign>("GetResultsForCampaign @ClientId", clientIdParameter)
                //    .ToList();
                var result = context.Database
                    .SqlQuery<PlantModelTreeDto>("PlantModelTree")
                    .ToList();

                return result;
            }
        }
    }
}