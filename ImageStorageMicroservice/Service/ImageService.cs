﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore; // Lägg till Entity Framework Core namespace

namespace ImageStorageMicroservice.Services
{
    public class ImageService
    {
        private readonly string _rootPath = @"C:\Your\Root\Path"; // Ange den korrekta rotsökvägen för att spara bilderna
        private readonly int _minWidth = 100; // Ange minimidimensioner för bilderna
        private readonly int _minHeight = 100;
        private readonly string[] _allowedFileTypes = { ".jpg", ".jpeg", ".png" }; // Tillåtna filtyper för bilder
        private readonly long _maxFileSize = 10 * 1024 * 1024; // Maximal filstorlek på 10 MB

        public bool IsImageQualityAcceptable(IFormFile image)
        {
            /* Denna metod tar emot en bildfil och kontrollerar om den uppfyller kvalitetskraven.
             * Den öppnar bilden och kontrollerar dess dimensioner, filtyp och filstorlek. 
             * Om bilden uppfyller alla krav returnerar den true, annars false.
             */
            using (var img = Image.FromStream(image.OpenReadStream()))
            {
                // Kontrollera minimidimensioner
                if (img.Width < _minWidth || img.Height < _minHeight)
                    return false;

                // Kontrollera filtyp
                if (!_allowedFileTypes.Contains(Path.GetExtension(image.FileName).ToLowerInvariant()))
                    return false;

                // Kontrollera filstorlek
                if (image.Length > _maxFileSize)
                    return false;

                return true;
            }
        }

        public string SaveImage(IFormFile image, string storeName)
        {
            /*Denna metod sparar en bildfil på servern. 
             * Den tar emot bilden och en butiksparameter, genererar ett unikt filnamn för bilden och skapar en sökväg baserat på butikens namn och aktuell månad. 
             * Sedan sparas bilden på den genererade sökvägen på hårddisken. Metoden returnerar sökvägen till den sparade bilden.
             */

            // Generera unikt filnamn för den sparade bilden
            var uniqueFileName = $"{Guid.NewGuid().ToString()}{Path.GetExtension(image.FileName)}";

            // Sökväg för att spara bilden baserat på butik och månad
            var storePath = Path.Combine(_rootPath, storeName, DateTime.Now.ToString("yyyy-MM"));
            Directory.CreateDirectory(storePath);

            // Sökväg för den sparade bilden
            var imagePath = Path.Combine(storePath, uniqueFileName);

            // Spara bilden på hårddisken
            using (var fileStream = new FileStream(imagePath, FileMode.Create))
            {
                image.CopyTo(fileStream);
            }

            return imagePath;
        }
    }
}




