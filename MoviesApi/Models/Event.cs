namespace MoviesApi.Models
{
    public class Event
    {
        public int Id { get; set; }

        [MaxLength(250)]

        public string Cinema { get; set; }

        [MaxLength(250)]
        public string City { get; set; }

        public DateTime Date { get; set; }

        public int MovieId { get; set; }

        [MaxLength(250)]
        public string MovieTitle { get; set; }

        public Movie Movie { get; set; }
    }
}

