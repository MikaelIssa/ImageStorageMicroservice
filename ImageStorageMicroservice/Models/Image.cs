namespace ImageStorageMicroservice.Models
{
    //En modell som representerar en bild.
    public class Image
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public string StoreId { get; set; }
        public DateTime UploadDate { get; set; }
        public int CustomerId { get; set; }
    }
}
