using System.ComponentModel.DataAnnotations;

namespace Partytime.Joined.Service.Dtos
{
    public record JoinedDto(Guid partyid, Guid Userid);
    public record CreateJoinDto([Required] Guid partyid, [Required] Guid userid, string username);
    public record UpdateJoinDto([Required] Guid partyid, [Required] Guid userid, [Required] bool accepted);
    public record DeleteJoinDto([Required] Guid partyid, [Required] Guid userid);
}