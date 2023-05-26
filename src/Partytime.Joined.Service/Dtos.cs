using System.ComponentModel.DataAnnotations;

namespace Partytime.Joined.Service.Dtos
{
    public record JoinedDto(Guid Partyid, Guid Userid);
    public record CreateJoinDto([Required] Guid Partyid, [Required] Guid Userid, string Username);
    public record UpdateJoinDto([Required] Guid Partyid, [Required] Guid Userid, [Required] bool Accepted);
    public record DeleteJoinDto([Required] Guid Partyid, [Required] Guid Userid);
}