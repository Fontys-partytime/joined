using System.ComponentModel.DataAnnotations;

namespace Partytime.Joined.Service.Dtos
{
    public record JoinedDto(Guid PartyId, Guid UserId, DateTimeOffset JoinedDate, DateTimeOffset AcceptedDate, bool Accepted);
    public record CreateJoinDto([Required] Guid PartyId, [Required] Guid UserId, [Required] DateTimeOffset JoinedParty);
    public record UpdateJoin([Required] Guid PartyId, [Required] Guid UserId, [Required] DateTimeOffset AcceptedDate, [Required] bool Accepted);
}