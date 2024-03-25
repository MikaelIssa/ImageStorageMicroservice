using System;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace ImageStorageMicroservice.Services
{
    // En tjänstklass som innehåller affärslogiken för bildbehandling, inklusive kontroller för bildkvalitet och sparning av bilder.
    public class ImageService
    {
        private readonly string rootPath = @"C:\Your\Root\Path"; // Ange den korrekta rotsökvägen för att spara bilderna
        private readonly int minWidth = 100; // Ange minimidimensioner för bilderna
        private readonly int minHeight = 100;
        private readonly string[] allowedFileTypes = { ".jpg", ".jpeg", ".png" }; // Tillåtna filtyper för bilder
        private readonly long maxFileSize = 10 * 1024 * 1024; // Maximal filstorlek på 10 MB
        private readonly YourDbContext _dbContext; // Lägg till din DbContext

        public ImageService(YourDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public bool IsImageQualityAcceptable(IFormFile image)
        {
            using (var img = Image.FromStream(image.OpenReadStream()))
            {
                // Kontrollera minimidimensioner
                if (img.Width < minWidth || img.Height < minHeight)
                    return false;

                // Kontrollera filtyp
                if (!allowedFileTypes.Contains(Path.GetExtension(image.FileName).ToLowerInvariant()))
                    return false;

                // Kontrollera filstorlek
                if (image.Length > maxFileSize)
                    return false;

                return true;
            }
        }

        public async Task<string> SaveImageAsync(IFormFile image)
        {
            // Generera unikt filnamn för den sparade bilden
            var uniqueFileName = $"{Guid.NewGuid().ToString()}{Path.GetExtension(image.FileName)}";

            // Sökväg för att spara bilden baserat på butik och månad
            var storePath = Path.Combine(rootPath, GetStoreFolder(image), GetMonthFolder());

            // Säkerställ att mappen finns, annars skapa den
            if (!Directory.Exists(storePath))
                Directory.CreateDirectory(storePath);

            // Sökväg för den sparade bilden
            var imagePath = Path.Combine(storePath, uniqueFileName);

            // Spara bilden på hårddisken
            using (var fileStream = new FileStream(imagePath, FileMode.Create))
            {
                await image.CopyToAsync(fileStream);
            }

            // Returnera URL för sparad bild
            return imagePath;
        }

        private string GetStoreFolder(IFormFile image)
        {
            // Implementera logik för att få butiksidentifierare baserat på den uppladdade bilden
            // Till exempel, returnera "PosterAndPaw" för en bild som kommer från PosterAndPaw
            return "PosterAndPaw"; // Exempel
        }

        private string GetMonthFolder()
        {
            // Implementera logik för att få aktuell månad och år och returnera i formatet "MM-yyyy"
            return DateTime.Now.ToString("MM-yyyy"); // Exempel
        }

        public async Task<Image> GetImageByIdAsync(int id)
        {
            // Implementera logik för att hämta en bild från databasen baserat på ID
            var image = await _dbContext.Images.FindAsync(id);

            return image;
        }

        public async Task<List<Image>> GetAllImagesAsync()
        {
            // Implementera logik för att hämta alla sparade bilder från en databas eller annan lagringsplats
            var images = await _dbContext.Images.ToListAsync();

            return images;
        }
    }
}




    //public interface IImageStorageService
    //{
    //    string SaveImage(IFormFile file);
    //}

    //public class ImageStorageService : IImageStorageService
    //{
    //    public string SaveImage(IFormFile file)
    //    {
    //        if (file == null || file.Length == 0)
    //            throw new ArgumentException("Invalid image file.");

    //        var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "UploadedImages");
    //        if (!Directory.Exists(uploadsFolder))
    //            Directory.CreateDirectory(uploadsFolder);

    //        var uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;
    //        var filePath = Path.Combine(uploadsFolder, uniqueFileName);

    //        using (var stream = new FileStream(filePath, FileMode.Create))
    //        {
    //            file.CopyTo(stream);
    //        }

    //        return filePath;
    //    }
    //}
}
