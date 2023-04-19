namespace Partytime.Joined.Contracts
{
    public record JoinedGetByPartyId(Guid partyId);
    public record JoinedCreated(Guid partyId, Guid UserId, DateTimeOffset JoinedDate);
    public record JoinedUpdated(Guid partyId, Guid UserId, DateTimeOffset AcceptedDate, bool Accepted);
    public record JoinedDeleted(Guid partyId, Guid UserId);

    // Hardcoded reply for demo purposes
    public record CommandMessage(string MessageString);
}