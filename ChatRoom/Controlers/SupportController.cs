using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ChatRoom.Controlers
{
    [Authorize]
    public class SupportController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
