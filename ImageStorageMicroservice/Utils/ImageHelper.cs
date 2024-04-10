using System.Drawing;
namespace ImageStorageMicroservice.Utils
{
    public static class ImageHelper
    {
        public static bool IsImageValid(Image image, int minWidth, int minHeight)
        {
            /* Detta är en metod som används för att kontrollera om en bild är giltig baserat på dess dimensioner. 
             * Den tar in en bild, samt minimibredd och minimihöjd som parametrar. 
             * Metoden jämför sedan bildens bredd och höjd med de angivna minimidimensionerna och returnerar true om bilden är tillräckligt stor, annars returnerar den false.
             */
            return image.Width >= minWidth && image.Height >= minHeight;
        }

        public static Size GetImageDimensions(Image image)
        {
            /* Denna metod används för att hämta dimensionerna (bredd och höjd) på en bild. 
             * Den tar in en bild som parameter och skapar sedan en ny instans av klassen Size med bildens bredd och höjd som parametrar. 
             * Denna storlek kan sedan användas för att få information om bildens dimensioner för vidare bearbetning eller visning.
             */
            return new Size(image.Width, image.Height);
        }
    }
}
