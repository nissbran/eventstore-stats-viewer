using System.Threading.Tasks;
using EventStore.Monitoring.Infrastructure.Communication.Http;
using Microsoft.AspNetCore.Mvc;

namespace EventStore.Monitoring.Api.Controllers
{
    public class StatsController : Controller
    {
        private readonly EventStoreHttpClient _eventStoreHttpClient;

        public StatsController(EventStoreHttpClient eventStoreHttpClient)
        {
            _eventStoreHttpClient = eventStoreHttpClient;
        }

        [HttpGet("")]
        public async Task<IActionResult> GetStats()
        {
            return Ok(await _eventStoreHttpClient.GetStats());
        }
    }
}