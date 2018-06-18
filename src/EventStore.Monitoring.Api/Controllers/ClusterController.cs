using System.Threading.Tasks;
using EventStore.Monitoring.Infrastructure.Communication.Http;
using Microsoft.AspNetCore.Mvc;

namespace EventStore.Monitoring.Api.Controllers
{
    public class ClusterController : Controller
    {
        private readonly EventStoreHttpClient _eventStoreHttpClient;

        public ClusterController(EventStoreHttpClient eventStoreHttpClient)
        {
            _eventStoreHttpClient = eventStoreHttpClient;
        }

        [HttpGet("")]
        public async Task<IActionResult> GetCluster()
        {
            return Ok(await _eventStoreHttpClient.GetClusterGossip());
        }
    }
}