using Npgsql;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using Partytime.Joined.Service.Dtos;
using Entities = Partytime.Joined.Service.Entities;
using MassTransit.JobService;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Partytime.Joined.Service.Entities;
using Partytime.Joined.Service.Repositories;

namespace Partytime.Joined.Service.Repositories
{
    public class JoinedRepository : IJoinedRepository
    {
        private readonly JoinedContext _context;

        public JoinedRepository(JoinedContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<List<Entities.Joined>> GetJoinedByPartyId(Guid partyId)
        {
            List<Entities.Joined> joined = await _context.Joined.Where(joined => joined.Partyid == partyId).ToListAsync(); 

            return joined;
        }

        public async Task<Entities.Joined> CreateJoined(Entities.Joined joined)
        {

            Entities.Joined joinedCreation = joined;
            joinedCreation.Joineddate = DateTimeOffset.UtcNow;

            await _context.Joined.AddAsync(joinedCreation);
            await _context.SaveChangesAsync();

            return joinedCreation;
        }

        public async Task<Entities.Joined?> UpdateJoined([FromBody] Entities.Joined joined)
        {
            var joinedFound = _context.Joined.SingleOrDefault(joined => joined.Partyid == joined.Partyid && joined.Userid == joined.Userid);

            if (joinedFound != null)
            {
                if(joined.Accepted != null)
                {
                    joinedFound.Accepteddate = DateTimeOffset.UtcNow;
                }

                await _context.SaveChangesAsync();
            }
            
            return joinedFound;
        }

        public async Task<bool> DeleteJoined(Guid partyId, Guid userId)
        {
            var joinedToDelete = _context.Joined.SingleOrDefault(joined => joined.Partyid == partyId && joined.Userid == userId);
            
            if (joinedToDelete != null) {
                _context.Joined.Remove(joinedToDelete);
                await _context.SaveChangesAsync();
                return true;
            }

            return false;
        }
    }
}