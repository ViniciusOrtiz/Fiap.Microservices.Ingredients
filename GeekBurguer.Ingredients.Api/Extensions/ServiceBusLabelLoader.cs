using Azure.Messaging.ServiceBus;
using Common.ServiceBus;
using GeekBurguer.Ingredients.Api.Infra;
using GeekBurguer.Ingredients.Api.Model.LabelLoader;
using GeekBurguer.Ingredients.Api.Repository;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace GeekBurguer.Ingredients.Api.Extensions
{
    public class ServiceBusLabelLoader : IServiceBusLabelLoader
    {
        private readonly IngredientsContext _context;

        public ServiceBusLabelLoader(IngredientsContext context)
        {
            _context = context;
        }
        public async Task Run(IConfiguration configuration)
        {

            var connectionString = configuration["ServiceBusConnection"];
            var topicName = "labelimageadded";
            var subscriptionName = "Ingredients";

            var consumer = new Consumer(connectionString, topicName, subscriptionName);

            await consumer.Start(MessageHandler, ErrorHandler);

        }

        public async Task MessageHandler(ProcessMessageEventArgs args)
        {
            string body = args.Message.Body.ToString();
            var labelLoader = JsonSerializer.Deserialize<LabelLoader>(body);
            Console.WriteLine($"Received: {body}");

            var products = await _context.Products.Include(p => p.Items).ToListAsync();

            var itemsByIngredient = await _context.Items.Where(i => i.Name == labelLoader.ItemName).ToListAsync();

            foreach( var item in itemsByIngredient)
            {
                item.Ingredients = string.Join(",", labelLoader.Ingredients);
                _context.Items.Update(item);
            }
            await _context.SaveChangesAsync();

            // complete the message. message is deleted from the queue. 
            await args.CompleteMessageAsync(args.Message);
        }

        // handle any errors when receiving messages
        public Task ErrorHandler(ProcessErrorEventArgs args)
        {
            Console.WriteLine(args.Exception.ToString());
            return Task.CompletedTask;
        }

    }
}
