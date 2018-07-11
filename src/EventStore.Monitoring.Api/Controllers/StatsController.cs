using System.Threading.Tasks;
using EventStore.Monitoring.Infrastructure.Communication;
using Microsoft.AspNetCore.Mvc;

namespace EventStore.Monitoring.Api.Controllers
{
    [Route("stats")]
    public class StatsController : Controller
    {
        private readonly EventStoreHttpClient _eventStoreHttpClient;
        private readonly EventStoreTcpClient _eventStoreTcplient;

        public StatsController(EventStoreHttpClient eventStoreHttpClient, EventStoreTcpClient eventStoreTcplient)
        {
            _eventStoreHttpClient = eventStoreHttpClient;
            _eventStoreTcplient = eventStoreTcplient;
        }

        [HttpGet("")]
        public async Task<IActionResult> GetStats()
        {
            return Ok(await _eventStoreHttpClient.GetStats());
        }
        
        [HttpGet("history")]
        public async Task<IActionResult> GetTcpStats()
        {
            return Ok(await _eventStoreTcplient.GetLatestStats());
        }

        [HttpGet("configure")]
        public async Task<IActionResult> ConfigureStats()
        {
            await _eventStoreTcplient.ConfigureAllClusterStatsStreams();

            return Ok();
        }
    }
}