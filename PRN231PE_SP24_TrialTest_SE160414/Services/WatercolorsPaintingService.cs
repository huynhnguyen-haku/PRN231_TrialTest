using BusinessObjects.Models;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class WatercolorsPaintingService : IWatercolorsPaintingService
    {
        private IWatercolorsPaintingRepo _waterRepo;

        public WatercolorsPaintingService()
        {
            _waterRepo = new WatercolorsPaintingRepo();
        }

        public bool AddWatercolorsPainting(WatercolorsPainting waterColor)
            => _waterRepo.AddWatercolorsPainting(waterColor);

        public bool DeleteWatercolorsPainting(string id)
            => _waterRepo.DeleteWatercolorsPainting(id);

        public IList<WatercolorsPainting> GetAllWatercolorsPainting()
            => _waterRepo.GetAllWatercolorsPainting();

        public WatercolorsPainting GetWatercolorsPaintingById(string id)
            => _waterRepo.GetWatercolorsPaintingById(id);

        public IList<WatercolorsPainting> SearchWatercolorsPainting(string? authorName, int? publishYear)
            => _waterRepo.SearchWatercolorsPainting(authorName, publishYear);

        public bool UpdateWatercolorsPainting(WatercolorsPainting waterColor)
            => _waterRepo.UpdateWatercolorsPainting(waterColor);
    }
}
