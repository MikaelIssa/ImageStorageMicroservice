namespace ImageStorageMicroservice
{
    public class Information
    {
        /*
         * Här är några saker att överväga för att ytterligare förbättra och utöka funktionaliteten:
         * Felhantering
         * Validering
         * Säkerhet
         * Skalbarhet och prestanda
         * Testning
         * Dokumentation
        ------------------------------------------------------------------------------


        Testa REST API för att ta emot bildfiler:

        Använd en API-testverktyg som Postman eller curl för att skicka en POST-förfrågan till din microservice med en bildfil som kropp (body) och butikens namn som parameter.
        Kontrollera att din microservice fångar upp förfrågan korrekt och returnerar en lämplig respons. Du bör få tillbaka en URL för den sparade bilden om uppladdningen lyckades.

        Verifiera sparade bilder på hårddisken:

        Efter att ha laddat upp en bild, gå till den mappstruktur där bilderna sparas på hårddisken.
        Kontrollera att bilderna är organiserade enligt den specificerade mappstrukturen, där de är separerade efter butik och månad.

        Kontrollera kvaliteten på sparade bilder:

        Testa din ImageService genom att skicka olika bildfiler av olika dimensioner och filtyper till metoden IsImageQualityAcceptable.
        Se till att metoden returnerar true för bilder av tillräcklig kvalitet och false för bilder som inte uppfyller kvalitetskraven.

        Verifiera statiska besöksadresser:

        När du har laddat upp bilder och fått tillbaka deras URL:er, kontrollera att dessa URL:er är stabila och leder till rätt bildfiler även efter flera förfrågningar.

        Skapa Dockerfile och docker-compose-fil:

        Skapa en Dockerfile som beskriver hur din applikation ska paketeras och köras som en Docker-container.
        Skapa en docker-compose-fil för att definiera och konfigurera de olika tjänsterna i din applikation samt deras nätverk och eventuella volymer.
        */

    }
}
