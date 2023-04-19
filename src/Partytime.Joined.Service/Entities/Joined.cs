namespace Party.Joined.Service.Entities
{
    public class Joined
    {
        public Guid PartyId { get; set; }
        public Guid UserId { get; set; }
        public DateTimeOffset JoinedDate { get; set; }
        public DateTimeOffset AcceptedDate { get; set; }
        public bool Accepted { get; set; }
    }
}