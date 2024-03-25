namespace ImageStorageMicroservice.Utils
{
    //En hjälparklass som innehåller metoder för bildrelaterade operationer, såsom kvalitetskontroll och sparande av bilder.
    public static class ImageHelper
    {
        public static string GenerateUniqueFileName(string extension)
        {
            // Genererar ett unikt filnamn genom att använda Guid.NewGuid() och lägger till den givna filändelsen.
            return $"{Guid.NewGuid().ToString()}{extension}";
        }

        public static string GenerateImageUrl(string filePath)
        {
            // Genererar en offentlig URL för en sparad bild genom att kombinera en bas-URL med filnamnet från filvägen.
            // Implementera logik för att generera en offentlig URL baserat på filvägen
            var baseUrl = "https://example.com/images/";
            return baseUrl + Path.GetFileName(filePath);
        }

        public static bool IsImageSizeValid(IFormFile image, int maxSizeInBytes)
        {
            // Kontrollerar om storleken på den uppladdade bilden är inom det tillåtna gränsvärdet (maxSizeInBytes).
            // Implementera logik för att kontrollera att bildens storlek inte överstiger maxgränsen
            return image.Length <= maxSizeInBytes;
        }

        public static bool IsImageFormatValid(IFormFile image, string[] allowedFormats)
        {
            // Kontrollerar om filformatet för den uppladdade bilden är en av de tillåtna formaten i allowedFormats.
            // Implementera logik för att kontrollera att bildens filformat är acceptabelt
            var fileExtension = Path.GetExtension(image.FileName).ToLowerInvariant();
            foreach (var format in allowedFormats)
            {
                if (fileExtension == format)
                    return true;
            }
            return false;
        }
    }
}
