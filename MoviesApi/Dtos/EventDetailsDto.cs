namespace MoviesApi.Dtos
{
    public class EventDetailsDto
    {
        public int Id { get; set; }
        public string Cinema { get; set; }

        public string City { get; set; }

        public DateTime Date { get; set; }

        public int Ticket { get; set; }

        public int MovieId { get; set; }

        public string MovieTitle { get; set; }

    }
}
