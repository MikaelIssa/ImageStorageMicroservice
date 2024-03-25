using ImageStorageMicroservice.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using ImageStorageMicroservice;

namespace ImageStorageMicroservice.Controllers
{
    //Hanterar inkommande HTTP-begäranden för bilduppladdning och dirigera dem till lämplig tjänst.
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly ImageService _imageService;

        public ImageController(ImageService imageService)
        {
            _imageService = imageService;
        }

        [HttpPost("upload")]
        public async Task<IActionResult> UploadImage([FromForm] IFormFile image)
        {
            // Tar emot en bildfil från klienten och kontrollerar dess kvalitet.
            // Sparar bilden och returnerar en URL till den sparade bilden om kvalitetskontrollen passerar.
            try
            {
                if (image == null || image.Length == 0)
                    return BadRequest("No image uploaded.");

                // Kvalitetskontroll av bilden
                if (!_imageService.IsImageQualityAcceptable(image))
                    return BadRequest("Image quality is not acceptable.");

                // Spara bilden
                var imageUrl = await _imageService.SaveImageAsync(image);

                // Returnera URL för sparad bild
                return Ok(imageUrl);
            }
            catch (Exception ex)
            {
                // Logga fel
                Console.WriteLine($"Error uploading image: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error uploading image.");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetImage(int id)
        {
            // Hämtar en specifik bild baserat på dess ID från databasen.
            // Returnerar bilden om den finns, annars returneras en NotFound-status.
            try
            {
                var image = await _imageService.GetImageByIdAsync(id);
                if (image == null)
                    return NotFound();

                return Ok(image);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving image: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving image.");
            }
        }

        [HttpGet("list")]
        public async Task<IActionResult> GetImages()
        {
            // Hämtar en lista över alla sparade bilder från databasen.
            // Returnerar listan över bilder om det lyckas, annars returneras en felstatuskod.
            try
            {
                var images = await _imageService.GetAllImagesAsync();
                return Ok(images);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving images: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving images.");
            }
        }
    }
}





    //[ApiController]
    //[Route("[controller]")]
    //public class ImageController : ControllerBase
    //{
    //    private readonly IImageStorageService _imageStorageService;

    //    public ImageController(IImageStorageService imageStorageService) // Korrigera parameternamnet här
    //    {
    //        _imageStorageService = imageStorageService; // Använd this-nyckelordet för att skilja på det lokala attributet och den inkommande parametern
    //    }

    //    [HttpPost("upload")]
    //    public IActionResult UploadImage()
    //    {
    //        if (HttpContext.Request.Form.Files.Count > 0)
    //        {
    //            var file = HttpContext.Request.Form.Files[0];
    //            var imagePath = _imageStorageService.SaveImage(file);
    //            return Ok(imagePath);
    //        }
    //        else
    //        {
    //            return BadRequest("No image uploaded.");
    //        }
    //    }
    //}
}

