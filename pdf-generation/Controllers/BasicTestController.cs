using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;

namespace pdf_generation.Controllers
{
    public class BasicTestController : Controller
    {
        [Route("~/html")]
        public ActionResult Html()
        {
            if (Request.Headers.ContainsKey(HeaderNames.Authorization))
            {
                var authHeader = Request.Headers[HeaderNames.Authorization].First();
                if (authHeader == "Bearer A_FAKE_TOKEN")
                {
                    return View();
                }
            }

            return new StatusCodeResult(StatusCodes.Status401Unauthorized);
        }
    }
}
