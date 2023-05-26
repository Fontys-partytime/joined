using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Partytime.Joined.Service.Entities
{
    
    [Table("joined")]
    public class Joined
    {
        public Guid Id { get; set; }
        public Guid Partyid { get; set; }
        public Guid Userid { get; set; }
        public string? Username { get; set;}
        public DateTimeOffset? Joineddate { get; set; }
        public DateTimeOffset? Accepteddate { get; set; }
        public bool? Accepted { get; set; }
    }
}