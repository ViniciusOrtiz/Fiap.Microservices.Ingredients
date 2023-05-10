namespace GeekBurguer.Ingredients.Api.Extensions
{
    public interface IServiceBusLabelLoader
    {
        public Task Run(IConfiguration configuration);
    }
}
