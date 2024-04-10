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
               Detta är en HTTP POST-åtgärd som tar emot en bildfil (IFormFile) och en butiksparameter (storeName).
                Kontrollerar om en giltig bildfil har mottagits. Om inte returnerar den en BadRequest-respons.
                Skapar en mappstruktur baserat på butiksparametern och det aktuella datumet.
                Genererar ett unikt filnamn för den uppladdade bilden.
                Sparar den uppladdade bilden på servern.
                Returnerar en URL till den sparade bilden i en Ok-respons om uppladdningen lyckades, annars loggar den felet och returnerar en StatusCode 500 (Internal Server Error).
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
            /*  Detta är en HTTP GET-åtgärd som tar emot parametrar för butiksnamn, år, månad och filnamn.
                Den söker efter den begärda bilden på servern och returnerar den som en filrespons om den hittas.
                Om bilden inte hittas returnerar den en NotFound-respons.
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
            /*  Detta är en HTTP GET-åtgärd som tar emot parametrar för butiksnamn, år och månad.
                Den listar alla bilder som finns sparade för den angivna butiken och perioden.
                Returnerar en lista med URL:er till bilderna i en Ok-respons om det finns bilder.
                Om det inte finns några bilder returnerar den en NotFound-respons.
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



