using Microsoft.EntityFrameworkCore;
using ImageStorageMicroservice.Models;

namespace ImageStorageMicroservice.Data
{
    public class ImageDbContext : DbContext
    {
        public ImageDbContext(DbContextOptions<ImageDbContext> options) : base(options)
        {
        }

        public DbSet<Image> Images { get; set; }
        /* Detta är en egenskap av typen DbSet<Image> som representerar en samling av bilder i databasen. 
         * DbSet används för att fråga och uppdatera data i databastabellen som är associerad med Image-entiteten.
         */
    }
}
