using Nest;

namespace ClassifiedsMongoService.Controllers
{
    internal class ElasticClient
    {
        private ConnectionSettings settings;

        public ElasticClient(ConnectionSettings settings)
        {
            this.settings = settings;
        }
    }
}