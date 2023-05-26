using Entities = Partytime.Joined.Service.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Partytime.Joined.Service.Repositories
{
    public interface IJoinedRepository
    {
        Task<List<Entities.Joined>> GetJoinedByPartyId(Guid partyId);
        Task<Entities.Joined> CreateJoined(Entities.Joined joined);
        Task<Entities.Joined?> UpdateJoined(Entities.Joined joined);
        Task<bool> DeleteJoined(Guid partyId, Guid userId);
    }
}
