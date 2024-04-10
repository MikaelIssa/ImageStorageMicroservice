using System.Drawing;
namespace ImageStorageMicroservice.Utils
{
    public static class ImageHelper
    {
        public static bool IsImageValid(Image image, int minWidth, int minHeight)
        {
            /*  Detta är en metod som används för att kontrollera om en bild är giltig baserat på dess dimensioner.
                Den tar in en bild (Image) och minimibredd samt minimihöjd som parametrar.
                Metoden jämför sedan bildens bredd och höjd med de angivna minimidimensionerna och returnerar true om bildens bredd är större än eller lika med minWidth och höjden är större än eller lika med minHeight. Annars returnerar den false.

             */
            return image.Width >= minWidth && image.Height >= minHeight;
        }

        public static Size GetImageDimensions(Image image)
        {
            /*  Denna metod används för att hämta dimensionerna (bredd och höjd) på en bild.
                Den tar in en bild (Image) som parameter och skapar sedan en ny instans av klassen Size med bildens bredd och höjd som parametrar.
                Size-objektet som skapas innehåller information om bildens dimensioner, vilket kan användas för vidare bearbetning eller visning av bilden.
             */
            return new Size(image.Width, image.Height);
        }
    }
}
