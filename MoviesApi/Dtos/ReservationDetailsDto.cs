namespace MoviesApi.Dtos
{
    public class ReservationDetailsDto
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }

        public int Nb_Ticket { get; set; }

        public int StatutId { get; set; }

        public string StatutLabel { get; set; }

        public int EventId { get; set; }

        public DateTime DateEvent { get; set; }
    }
}
