using System.Threading.Tasks;
using System.Web.Mvc;
using GraphAPIDemo.Helpers;

namespace GraphAPIDemo.Controllers
{
    public class CalendarController : BaseController
    {
        // GET: Calendar
        [Authorize]
        public async Task<ActionResult> Index()
        {
            var events = await GraphHelper.GetEventsAsync();
            return View(events);
        }
    }
}