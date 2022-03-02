namespace Minimal_API_Swagger_JWT.Models
{
    public class Movie
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public double Rating { get; set; }
    }
}
