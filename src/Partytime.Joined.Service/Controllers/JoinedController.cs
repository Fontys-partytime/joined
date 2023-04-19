using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Partytime.Joined.Service.Dtos;
using Partytime.Joined.Contracts;

namespace Partytime.Joined.Service.Controllers
{
    [ApiController]
    [Route("joinedParties")]
    public class JoinedController : ControllerBase
    {
        private readonly IPublishEndpoint publishEndpoint;
        
        public JoinedController(IPublishEndpoint publishEndpoint)
        {
            this.publishEndpoint = publishEndpoint;
        }

        private static readonly List<JoinedDto> joined = new()
        {
            new JoinedDto(Guid.NewGuid(), Guid.NewGuid(), DateTimeOffset.UtcNow, DateTimeOffset.UtcNow, false),
            new JoinedDto(Guid.NewGuid(), Guid.NewGuid(), DateTimeOffset.UtcNow, DateTimeOffset.UtcNow, true),
            new JoinedDto(Guid.NewGuid(), Guid.NewGuid(), DateTimeOffset.UtcNow, DateTimeOffset.UtcNow, true),
            new JoinedDto(Guid.NewGuid(), Guid.NewGuid(), DateTimeOffset.UtcNow, DateTimeOffset.UtcNow, true)
        };

        // Default value
        public static CommandMessage hardcodedReply = new CommandMessage("nothing send yet.");

        [HttpGet("hardcoded-reply")]
        public async Task<CommandMessage> GetHardcodedReply()
        {
            return hardcodedReply;
        }

        [HttpGet]
        public async Task<IEnumerable<JoinedDto>> GetAsync()
        {
            return joined;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<JoinedDto>> GetByIdAsync(Guid id)
        {
            var joinedParty = joined.Where(joined => joined.PartyId == id).SingleOrDefault();
            
            if(joinedParty == null)
            {
                return NotFound();
            }

            // 1. Communicate with messaging container
            await publishEndpoint.Publish(new JoinedGetByPartyId(id));
            
            return joinedParty;
        }

        [HttpPost]
        public async Task<ActionResult<JoinedDto>> Post(CreateJoinDto createJoinDto)
        {

            // Need to add a function in Program.cs that automatically converts to ISODateTime for starts and ends
            // https://stackoverflow.com/questions/68539924/c-wrong-datetime-format-passed-to-front-end
            var joinedParty = new JoinedDto(createJoinDto.PartyId, createJoinDto.UserId, createJoinDto.JoinedParty, DateTimeOffset.UtcNow, false); 
            joined.Add(joinedParty);

            // Publish for walking skeleton
            await publishEndpoint.Publish(new JoinedCreated(createJoinDto.PartyId, createJoinDto.UserId, createJoinDto.JoinedParty));

            // Returns the GetById link of the created party
            return CreatedAtAction(nameof(GetByIdAsync), new {id = joinedParty.PartyId}, joinedParty);
        }

    }
}