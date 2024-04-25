namespace MoviesApi.Models
{
    public class Reservation
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }

        public int Nb_Ticket { get; set; }

        public int StatutId { get; set; }

        public Statut Statut { get; set; }

        public int EventId { get; set; }

        public Event Event { get; set; }

    }
}
