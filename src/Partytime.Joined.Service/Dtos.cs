using System.ComponentModel.DataAnnotations;

namespace Partytime.Joined.Service.Dtos
{
    public record JoinedPartyDto(Guid PartyId, Guid UserId, DateTimeOffset joinedDate, DateTimeOffset acceptedDate, bool accepted);
    public record CreateJoinParty([Required] Guid PartyId, [Required] Guid UserId, [Required] DateTimeOffset joinedParty);
    public record UpdateJoinParty([Required] Guid PartyId, [Required] Guid UserId, [Required] DateTimeOffset acceptedDate, [Required] bool accepted);
}