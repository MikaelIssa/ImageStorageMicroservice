namespace ImageStorageMicroservice.Models
{
    public class Image
    {
        public int Id { get; set; } // Unikt ID för bilden (om du behöver spara det i en databas)
        public string FileName { get; set; } // Filnamnet för bilden
        public string StoreName { get; set; } // Butikens namn
        public string FilePath { get; set; } // Sökvägen till bilden på hårddisken
        public int Width { get; set; } // Bredden på bilden i pixlar
        public int Height { get; set; } // Höjden på bilden i pixlar
        public DateTime UploadedAt { get; set; } // Datum och tid för uppladdning av bilden
                                                 // Det används för att spåra när bilden laddades upp
    }
}
