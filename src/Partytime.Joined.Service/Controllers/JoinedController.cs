using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Partytime.Joined.Service.Dtos;
using Partytime.Joined.Contracts;
using Partytime.Joined.Service.Repositories;

namespace Partytime.Joined.Service.Controllers
{
    [ApiController]
    [Route("joined")]
    public class JoinedController : ControllerBase
    {
        private readonly IJoinedRepository _joinedRepository;
        private readonly IPublishEndpoint _publishEndpoint;
        
        public JoinedController(IJoinedRepository joinedRepository, IPublishEndpoint publishEndpoint)
        {
            this._joinedRepository = joinedRepository;
            this._publishEndpoint = publishEndpoint;
        }

        [HttpGet("party/{id}")]
        public async Task<IActionResult> GetJoinedByPartyId(Guid id)
        {
            var joinedFound = await _joinedRepository.GetJoinedByPartyId(id);

            if (joinedFound == null)
                return NotFound();

            return Ok(joinedFound);
        }

        [HttpPost]
        public async Task<ActionResult<JoinedDto>> Post([FromBody] CreateJoinDto createJoinDto)
        {
            var joined = new Entities.Joined
            {
                Partyid = createJoinDto.partyid,
                Userid = createJoinDto.userid,
                Username = createJoinDto.username
            };

            // 1. Communicate with messaging container
            await _publishEndpoint.Publish(new JoinedCreated(createJoinDto.partyid, createJoinDto.userid, createJoinDto.username));

            Entities.Joined createdJoined = await _joinedRepository.CreateJoined(joined);

            // Returns the GetById link of the created party
            return Ok(createdJoined);
        }

        [HttpPut]
        public async Task<ActionResult<bool>> Put([FromBody] UpdateJoinDto updateJoinDto)
        {
            var joined = new Entities.Joined
            {
                Partyid = updateJoinDto.partyid,
                Userid = updateJoinDto.userid,
                Accepted = updateJoinDto.accepted
            };

            var updatedJoined = await _joinedRepository.UpdateJoined(joined);

            if (updatedJoined == null)
                return NotFound();

            return Ok(updatedJoined);
        }

        [HttpDelete("{partyId}/{id}")]
        public async Task<ActionResult<bool>> Delete(Guid partyId, Guid id)
        {
            bool joinedDeleted = await _joinedRepository.DeleteJoined(partyId, id);
            return Ok(joinedDeleted);
        }
    }
}