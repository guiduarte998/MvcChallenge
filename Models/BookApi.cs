namespace Models
{
    public class BookApi
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ISBN { get; set; }
        public string Publisher { get; set; }
        public string Language { get; set; }
        public decimal? Price { get; set; }
    }
}
