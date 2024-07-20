using BusinessObjects.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO
{
    public class WatercolorsPaintingDAO
    {
        private static WatercolorsPaintingDAO instance = null;
        private readonly WatercolorsPainting2024DBContext _context = null;

        public WatercolorsPaintingDAO()
        {
            if (_context == null)
            {
                _context = new WatercolorsPainting2024DBContext();
            }
        }

        public static WatercolorsPaintingDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new WatercolorsPaintingDAO();
                }
                return instance;
            }
        }

        public bool AddWatercolorsPainting(WatercolorsPainting waterColorPainting)
        {
            try
            {
                _context.WatercolorsPaintings.Add(waterColorPainting);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool UpdateWatercolorsPainting(WatercolorsPainting waterColorPainting)
        {
            try
            {
                var existingWaterColorPainting = _context.WatercolorsPaintings.Where(x => x.PaintingId == waterColorPainting.PaintingId).FirstOrDefault();
                if (existingWaterColorPainting != null)
                {
                    _context.Entry(existingWaterColorPainting).CurrentValues.SetValues(waterColorPainting);
                    _context.SaveChanges();
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool DeleteWatercolorsPainting(string paintingId)
        {
            try
            {
                var existingWaterColorPainting = _context.WatercolorsPaintings.Where(x => x.PaintingId == paintingId).FirstOrDefault();
                if (existingWaterColorPainting != null)
                {
                    _context.WatercolorsPaintings.Remove(existingWaterColorPainting);
                    _context.SaveChanges();
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public List<WatercolorsPainting> GetAllWatercolorsPaintings()
        {
            return _context.WatercolorsPaintings.Include(w => w.Style).ToList();
        }

        public WatercolorsPainting GetWatercolorsPaintingById(string paintingId)
        {
            return _context.WatercolorsPaintings.Where(x => x.PaintingId == paintingId).FirstOrDefault();
        }

        public List<WatercolorsPainting> SearchByAuthorAndPublishyear(string? authorName, int? year)
        {
            var query = _context.WatercolorsPaintings.AsQueryable();
            if (!string.IsNullOrEmpty(authorName))
            {
                query = query.Where(j => j.PaintingAuthor.Contains(authorName));
            }

            if (year.HasValue)
            {
                query = query.Where(j => j.PublishYear >= year.Value);
            }

            return query.Include(j => j.Style).ToList();
        }
    }
}
