using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;

namespace PieShopApi.Controllers
{
    [ApiController]
    [Route("files")]
    public class FilesController: ControllerBase
    {
        private readonly FileExtensionContentTypeProvider _contentTypeProvider;

        public FilesController(FileExtensionContentTypeProvider contentTypeProvider)
        {
            _contentTypeProvider = contentTypeProvider;
        }

        [HttpGet]
        public async Task<IActionResult> GetFile(string fileName)
        {
            //var filePath = "Core principles.pdf";
            var filePath = "Core principles.pptx";

            if (string.IsNullOrWhiteSpace(fileName))
            {
                return BadRequest();
            }

            if (!System.IO.File.Exists(filePath))
            {
                return NotFound();
            }

            if (!_contentTypeProvider.TryGetContentType(filePath, out var contentType))
            {
                contentType = "application/octet-stream";
            }

            var fileBytes = await System.IO.File.ReadAllBytesAsync(filePath);

            return File(fileBytes, contentType, fileName);
        }

        [HttpPost]
        public async Task<IActionResult> CreateFile(string fileName, IFormFile file)
        {
            if (string.IsNullOrWhiteSpace(fileName) || file.Length == 0 || file.Length > 52428800)
                return BadRequest();

            // don't do this in real projects!!!
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), $"{Guid.NewGuid()}-{fileName}");

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return Created();
        }
    }
}
