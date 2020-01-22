using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using uploadFile.Core.models;
using uploadFile.Core.Services.Interfaces;

namespace uploadFile.Core.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StorageController : ControllerBase
    {
        private readonly IStorageService _service;

        public StorageController(IStorageService service)
        {
            _service = service;
        }

        [HttpPost("upload")]
        public async Task<ActionResult<string>> Post ([FromForm] GedisFile std)
        {
            var url = await _service.UploadFiles(std);

            if(url == null)
            {
                return BadRequest("Erro ao fazer upload de arquivo");
            }

            return Ok();
        }
    }
}