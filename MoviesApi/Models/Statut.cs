namespace MoviesApi.Models
{
    public class Statut
    {
        public int Id { get; set; }

        [MaxLength(100)]
        public string Label { get; set; }
    }
}
