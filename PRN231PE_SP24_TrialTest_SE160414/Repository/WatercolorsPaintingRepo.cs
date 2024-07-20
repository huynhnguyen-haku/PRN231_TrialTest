using BusinessObjects.Models;
using DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class WatercolorsPaintingRepo : IWatercolorsPaintingRepo
    {
        public bool AddWatercolorsPainting(WatercolorsPainting waterColor)
            => WatercolorsPaintingDAO.Instance.AddWatercolorsPainting(waterColor);

        public bool DeleteWatercolorsPainting(string id)
            => WatercolorsPaintingDAO.Instance.DeleteWatercolorsPainting(id);

        public IList<WatercolorsPainting> GetAllWatercolorsPainting()
            => WatercolorsPaintingDAO.Instance.GetAllWatercolorsPaintings();

        public WatercolorsPainting GetWatercolorsPaintingById(string id)
            => WatercolorsPaintingDAO.Instance.GetWatercolorsPaintingById(id);

        public IList<WatercolorsPainting> SearchWatercolorsPainting(string? authorName, int? publishYear)
            => WatercolorsPaintingDAO.Instance.SearchByAuthorAndPublishyear(authorName, publishYear);

        public bool UpdateWatercolorsPainting(WatercolorsPainting waterColor)
            => WatercolorsPaintingDAO.Instance.UpdateWatercolorsPainting(waterColor);
    }
}
