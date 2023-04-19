using MassTransit;
using MassTransit.Transports;
using Partytime.Joined.Contracts;
using Partytime.Joined.Service.Controllers;

namespace Partytime.Party.Consumers
{
    public class CommandMessageConsumer : IConsumer<CommandMessage>
    {
        private readonly IPublishEndpoint publishEndpoint;
        
        public CommandMessageConsumer(IPublishEndpoint publishEndpoint)
        {
            this.publishEndpoint = publishEndpoint;
        }

        public async Task Consume(ConsumeContext<CommandMessage> context)
        {
            // Recieved message when joined party is created
            var message = context.Message;
            JoinedController.hardcodedReply = new CommandMessage(context.Message.ToString());
        }
    }

}