using ImageStorageMicroservice.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using ImageStorageMicroservice;

namespace ImageStorageMicroservice.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ImageController : ControllerBase
    {
        private readonly ILogger<ImageController> _logger;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public ImageController(ILogger<ImageController> logger, IWebHostEnvironment hostingEnvironment)
        {
            _logger = logger;
            _hostingEnvironment = hostingEnvironment;
        }

        [HttpPost]
        public IActionResult UploadImage(IFormFile image, string storeName)
        {

            /*
             * Denna åtgärd tar emot en HTTP POST-förfrågan med en bildfil och en butiksparameter. 
             * Den sparar sedan bilden på servern och returnerar en URL till den sparade bilden i en HTTP-respons.
             * Om det uppstår något fel under uppladdningsprocessen, loggar den felet och returnerar en lämplig felrespons.
             */

            try
            {
                if (image == null || image.Length == 0)
                    return BadRequest("Invalid image file");

                var uploadsFolder = Path.Combine(_hostingEnvironment.ContentRootPath, "Images", storeName, DateTime.Now.ToString("yyyy-MM"));
                Directory.CreateDirectory(uploadsFolder);

                var uniqueFileName = $"{Guid.NewGuid().ToString()}{Path.GetExtension(image.FileName)}";
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    image.CopyTo(fileStream);
                }

                var imageUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}/images/{storeName}/{DateTime.Now.ToString("yyyy-MM")}/{uniqueFileName}";

                return Ok(new { imageUrl });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while uploading image");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{storeName}/{year}/{month}/{fileName}")]
        public IActionResult GetImage(string storeName, int year, int month, string fileName)
        {
            /* Denna åtgärd tar emot en HTTP GET-förfrågan med parametrar för butiksnamn, år, månad och filnamn. 
             * Den söker efter den begärda bilden på servern och returnerar den som en filrespons. 
             * Om bilden inte hittas returnerar den en HTTP 404 NotFound-respons.
             */
            var imagePath = Path.Combine(_hostingEnvironment.ContentRootPath, "Images", storeName, year.ToString(), month.ToString(), fileName);

            if (!System.IO.File.Exists(imagePath))
                return NotFound();

            var imageBytes = System.IO.File.ReadAllBytes(imagePath);
            return File(imageBytes, "image/jpeg"); // Adjust content type based on your image type
        }

        [HttpGet("{storeName}/{year}/{month}")]
        public IActionResult GetImages(string storeName, int year, int month)
        {
            /* Denna åtgärd tar emot en HTTP GET-förfrågan med parametrar för butiksnamn, år och månad.
             * Den listar sedan alla bilder som finns sparade för den angivna butiken och perioden och returnerar en lista med deras URL:er. 
             * Om det inte finns några bilder returnerar den en HTTP 404 NotFound-respons.
             */
            var imagesFolder = Path.Combine(_hostingEnvironment.ContentRootPath, "Images", storeName, year.ToString(), month.ToString());

            if (!Directory.Exists(imagesFolder))
                return NotFound();

            var imageFiles = Directory.GetFiles(imagesFolder);
            var imageUrls = imageFiles.Select(imageFile =>
            {
                var fileName = Path.GetFileName(imageFile);
                return $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}/images/{storeName}/{year}/{month}/{fileName}";
            });

            return Ok(imageUrls);
        }
    }
}



