namespace Partytime.Joined.Contracts
{
    public record JoinedCreated(Guid partyId, Guid UserId, string Username); // This happens when a joined request gets sent
    public record JoinedUpdated(Guid partyId, Guid UserId, bool Accepted); // This happens after a joined request gets accepted
    public record JoinedDeleted(Guid partyId, Guid UserId); // This happens after a joined gets declined
}