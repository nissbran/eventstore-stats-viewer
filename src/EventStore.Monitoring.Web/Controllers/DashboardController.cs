using System.Threading.Tasks;
using EventStore.Monitoring.Infrastructure.Communication;
using EventStore.Monitoring.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace EventStore.Monitoring.Web.Controllers
{
    public class DashboardController : Controller
    {
        private readonly EventStoreHttpClient _eventStoreHttpClient;

        public DashboardController(EventStoreHttpClient eventStoreHttpClient)
        {
            _eventStoreHttpClient = eventStoreHttpClient;
        }
        
        [Route("")]
        public async Task<IActionResult> Index()
        {
           // var gossip = await _eventStoreHttpClient.GetClusterGossip();
            var stats = await _eventStoreHttpClient.GetStats();
            
            return View(new DashboardViewModel(stats));
        }
    }
}