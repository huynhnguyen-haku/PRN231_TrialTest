using BusinessObjects.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.Server.HttpSys;
using Services;
using System.Net;
using WebApi.DTO;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WatercolorsPaintingController : ControllerBase
    {
        private readonly IWatercolorsPaintingService _watercolorsPaintingService;

        public WatercolorsPaintingController(IWatercolorsPaintingService watercolorsPaintingService)
        {
            _watercolorsPaintingService = watercolorsPaintingService;
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "3")]
        [HttpPost]
        public IActionResult CreateWaterColor([FromBody] WatercolorsPaintingDTO waterColor)
        {
            try
            {
                var existing = _watercolorsPaintingService.GetWatercolorsPaintingById(waterColor.Id);
                if (existing != null)
                {
                    return Conflict("Water color already exists");
                }

                var newWaterColor = new WatercolorsPainting
                {
                    PaintingId = waterColor.Id,
                    PaintingName = waterColor.Name,
                    PaintingDescription = waterColor.Description,
                    PaintingAuthor = waterColor.Author,
                    Price = waterColor.Price,
                    PublishYear = waterColor.PublishYear,
                    StyleId = waterColor.StyleId,
                    CreatedDate = DateTime.Now
                };

                _watercolorsPaintingService.AddWatercolorsPainting(newWaterColor);
                return Ok("Water color created successfully");
            }
            catch (System.Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "3")]
        [HttpPut]
        public IActionResult UpdateWaterColor([FromBody] WatercolorsPaintingDTO waterColor)
        {
            try
            {
                var existing = _watercolorsPaintingService.GetWatercolorsPaintingById(waterColor.Id);
                if (existing == null)
                {
                    return NotFound("Water color not found");
                }

                existing.PaintingName = waterColor.Name;
                existing.PaintingDescription = waterColor.Description;
                existing.PaintingAuthor = waterColor.Author;
                existing.Price = waterColor.Price;
                existing.PublishYear = waterColor.PublishYear;
                existing.StyleId = waterColor.StyleId;

                _watercolorsPaintingService.UpdateWatercolorsPainting(existing);
                return Ok("Water color updated successfully");
            }
            catch (System.Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "3")]
        [HttpDelete("{id}")]
        public IActionResult DeleteWaterColor(string id)
        {
            try
            {
                var existing = _watercolorsPaintingService.GetWatercolorsPaintingById(id);
                if (existing == null)
                {
                    return NotFound("Water color not found");
                }

                _watercolorsPaintingService.DeleteWatercolorsPainting(existing.PaintingId);
                return Ok("Water color deleted successfully");
            }
            catch (System.Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "3")]
        [HttpGet]
        [EnableQuery]
        public IActionResult GetAllWaterColors()
        {
            try
            {
                IList<WaterColorResponse> waterColorResponses = new List<WaterColorResponse>();
                var waterColors = _watercolorsPaintingService.GetAllWatercolorsPainting();
                foreach (var waterColor in waterColors)
                {
                    waterColorResponses.Add(new WaterColorResponse
                    {
                        PaintingId = waterColor.PaintingId,
                        PaintingName = waterColor.PaintingName,
                        Description = waterColor.PaintingDescription,
                        PaintingAuthor = waterColor.PaintingAuthor,
                        Price = waterColor.Price,
                        PublishYear = waterColor.PublishYear,
                        StyleName = waterColor.Style.StyleName
                    });
                }
                return Ok(waterColorResponses);
            }
            catch (System.Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "3,2")]
        [EnableQuery]
        [HttpGet("search")]
        public IActionResult SearchWaterColors(string? paintingAuthor, int? publishYear)
        {
            try
            {
                IList<WaterColorResponse> waterColorResponses = new List<WaterColorResponse>();
                var waterColors = _watercolorsPaintingService.SearchWatercolorsPainting(paintingAuthor, publishYear);
                foreach (var waterColor in waterColors)
                {
                    waterColorResponses.Add(new WaterColorResponse
                    {
                        PaintingId = waterColor.PaintingId,
                        PaintingName = waterColor.PaintingName,
                        Description = waterColor.PaintingDescription,
                        PaintingAuthor = waterColor.PaintingAuthor,
                        Price = waterColor.Price,
                        PublishYear = waterColor.PublishYear,
                        StyleName = waterColor.Style.StyleName
                    });
                }
                return Ok(waterColorResponses);
            }
            catch (System.Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
