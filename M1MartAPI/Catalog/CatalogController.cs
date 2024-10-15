using M1MartAPI.Catalog.CatalogDtos;
using M1MartAPI.Shared;
using M1MartDataAccess.Models;
using Microsoft.AspNetCore.Mvc;

namespace M1MartAPI.Catalog
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CatalogController : ControllerBase
    {
        private readonly CatalogService _catalogService;
        public CatalogController(CatalogService catalogService)
        {
            _catalogService = catalogService;
        }

        [HttpGet]
        public IActionResult GetCatalogs()
        {
            try {
                var catalogs = _catalogService.GetAllCatalogs();
                return Ok(new ResponseDto<List<CatalogDto>>(){
                    Status = "Success",
                    Message = $"You've received {catalogs.Count()} catalogs.",
                    Data = catalogs
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResponseDto<string>()
                {
                    Status = "SERVER ERROR",
                    Message = ex.Message
                });
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetCatalog(int id)
        {
            try {
                var catalog = _catalogService.GetCatalogById(id);
                return Ok(new ResponseDto<CatalogDetailDto>()
                {
                    Status = "SUCCESS",
                    Message = $"Here you've a single catalog requested for id {id}.",
                    Data = catalog
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResponseDto<string>()
                {
                    Status = "SERVER ERROR",
                    Message = ex.Message
                });
            }
        }
    }
}
