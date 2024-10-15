using M1MartAPI.Dashboard.DashboardDtos;
using M1MartAPI.Shared;
using Microsoft.AspNetCore.Mvc;

namespace M1MartAPI.Dashboard
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        private readonly DashboardService _dashboardService;
        public DashboardController(DashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var dataDashboard = _dashboardService.GetData();
                return Ok(new ResponseDto<DashboardDto>()
                {
                    Status = "SUCCESS",
                    Message = "Here is the dashboard data",
                    Data = dataDashboard
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
