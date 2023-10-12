using Partytime.Joined.Service.Controllers;
using Xunit;
using FakeItEasy;
using Partytime.Joined.Service.Repositories;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Partytime.Joined.Service.Dtos;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace Partytime.Joined.Tests
{
    public class UnitTest1
    {
        [Fact]
        public async void GetJoinedByPartyId_ReturnsCorrectJoined_ByPartyId()
        {
            // Arrange
            List<Service.Entities.Joined> fakeJoined = A.CollectionOfDummy<Service.Entities.Joined>(1).ToList();
            var partyId = fakeJoined.First().Partyid;

            var dataStore = A.Fake<IJoinedRepository>();
            var publishEndPoint = A.Fake<IPublishEndpoint>();

            A.CallTo(() => dataStore.GetJoinedByPartyId(partyId)).Returns(fakeJoined);
            var controller = new JoinedController(dataStore, publishEndPoint);

            // Act
            var actionResult = await controller.GetJoinedByPartyId(partyId);

            // Assert
            var result = actionResult as OkObjectResult;
            var returnJoined = result.Value as List<Service.Entities.Joined>;
            Assert.Equal(returnJoined.First(), fakeJoined.First());
        }

        [Fact]
        public async void Updated_UpdatesJoinedCorrectly()
        {
            // Arrange
            Service.Entities.Joined fakeJoined = A.Dummy<Service.Entities.Joined>();
            var oldJoined = fakeJoined;

            fakeJoined.Accepted = true;
            UpdateJoinDto updatedJoined = new UpdateJoinDto(fakeJoined.Partyid, fakeJoined.Userid, true);

            var dataStore = A.Fake<IJoinedRepository>();
            var publishEndPoint = A.Fake<IPublishEndpoint>();

            A.CallTo(() => dataStore.UpdateJoined(oldJoined)).Returns(oldJoined);
            var controller = new JoinedController(dataStore, publishEndPoint);

            // Act
            var actionResult = await controller.Put(updatedJoined);

            // Assert
            var result = actionResult.Result as OkObjectResult;
         
            var returnJoined = result.Value as Service.Entities.Joined;
            Assert.NotEqual(oldJoined, returnJoined);
        }

        [Fact]
        public async void Delete_DeletesJoinedCorrectly()
        {
            // Arrange
            Service.Entities.Joined fakeJoined = A.Dummy<Service.Entities.Joined>();
            var oldJoined = fakeJoined;

            fakeJoined.Accepted = true;
            DeleteJoinDto deletedJoined = new DeleteJoinDto(fakeJoined.Partyid, fakeJoined.Userid);

            var dataStore = A.Fake<IJoinedRepository>();
            var publishEndPoint = A.Fake<IPublishEndpoint>();

            A.CallTo(() => dataStore.DeleteJoined(oldJoined.Partyid, oldJoined.Userid)).Returns(true);
            var controller = new JoinedController(dataStore, publishEndPoint);

            // Act
            var actionResult = await controller.Delete(deletedJoined.partyid, deletedJoined.userid);

            // Assert
            var result = actionResult.Result as OkObjectResult;

            Assert.True((bool)result.Value);
        }
    }
}