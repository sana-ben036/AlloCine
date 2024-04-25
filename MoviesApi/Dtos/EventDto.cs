namespace MoviesApi.Dtos
{
    public class EventDto
    {
        [MaxLength(250)]

        public string Cinema { get; set; }

        [MaxLength(250)]
        public string City { get; set; }

        public DateTime Date { get; set; }
        public int Ticket { get; set; }

        public int MovieId { get; set; }
    }
}
