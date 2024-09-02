using MauiApiServer.Data.Core.Interfaces;
using MauiApiServer.Data.Core.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MauiApiServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DataController : ControllerBase
    {
        private readonly IDataService _dataService;
        private readonly ILogger<DataController> _logger;

        public DataController(
            IDataService dataService,
            ILogger<DataController> logger)
        {
            _dataService = dataService;
            _logger = logger;
        }

        [HttpPost("upload-file")]
        public async Task<IActionResult> UploadFile()
        {
            try
            {
                var httpContent = HttpContext.Request;

                if (httpContent?.Form.Files.Count == 0)
                {
                    return BadRequest("No file selected!");
                }

                var file = httpContent.Form.Files[0];
                var result = await _dataService.ProcessFileAsync(file);

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(500, "An internal server error occurred.");
            }
        }

        [HttpPost("update-data")]
        public async Task<IActionResult> UpdateData([FromBody] Person data)
        {
            return Ok(await _dataService.UpdatePerson(data)); 
        }

        [HttpPost("save-data")]
        public async Task<IActionResult> SaveData([FromBody] List<Person> data)
        {
            return Ok(await _dataService.SaveDataAsync(data));
        }
    }
}
