namespace ImageStorageMicroservice
{
    public class Information
    {
        /*
         * 1. Klient skickar en begäran om att ladda upp en bild:
             Klienten skickar en HTTP POST-begäran till ImageController's UploadImage-endpoint.
             Begäran innehåller den uppladdade bildfilen som en del av begäran.
        ------------------------------------------------------------------------------
        2.  ImageController tar emot begäran och vidarebefordrar den till ImageService för behandling.
            I ImageService utförs kvalitetskontroller på den uppladdade bilden med hjälp av ImageHelper för att säkerställa att den uppfyller de specificerade kvalitetskraven,
            inklusive dimensioner och filstorlek.
        ----------------------------------------------------------
        3.
            Om bilden klarar kvalitetskontrollen genereras ett unikt filnamn för bilden med hjälp av ImageHelper.
            Mappstruktur för att spara bilden skapas baserat på butikens identifierare och månad, och bilden sparas på hårddisken med det unika filnamnet i den relevanta mappen.
        ----------------------------------------------------------------------
        4. Ett svar returneras till klienten med antingen en framgångsindikator och URL:en till den sparade bilden eller en felstatus om bilden inte uppfyller kvalitetskraven.
         */
    }
}
