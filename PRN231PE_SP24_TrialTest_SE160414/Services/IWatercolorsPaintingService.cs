using BusinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface IWatercolorsPaintingService
    {
        bool AddWatercolorsPainting(WatercolorsPainting waterColor);
        bool UpdateWatercolorsPainting(WatercolorsPainting waterColor);
        bool DeleteWatercolorsPainting(string id);
        IList<WatercolorsPainting> GetAllWatercolorsPainting();
        WatercolorsPainting GetWatercolorsPaintingById(string id);
        IList<WatercolorsPainting> SearchWatercolorsPainting(string? authorName, int? publishYear);
    }
}
