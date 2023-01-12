using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Ticketing.Models.Tickets;

namespace Ticketing.Controllers
{
    public class TicketController : Controller
    {
        private readonly TicketContext ticketContext;
        private IWebHostEnvironment Environment;


        public TicketController( TicketContext ticketContext , IWebHostEnvironment environment)
        {
            this.ticketContext = ticketContext;
            Environment = environment;
        }
        // GET: ArticleController
        public ActionResult Index()
        {
            var list = ticketContext.Tickets.ToList(); 
            return View(list);
        }

        // GET: ArticleController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ArticleController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ArticleController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateTicketVewModel ticket)
        {

            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "مقادیر را به درستی وارد کنید");
               
                return View(ticket);
            }
            
            var res = new Ticket()
            {
                Title = ticket.Title,
                //ArticleImage = Guid.NewGuid().ToString() + "." + Path.GetExtension(ticket.TicketImageFile.FileName),
                Content = ticket.Content,
                ShortDescription = ticket.ShortDescription

            };
            res.ArticleImage = new List<TicketImage>();

            foreach (var item in ticket.TicketImageFile)
            {
                var img = new TicketImage()
                {
                    TicketId = res.Id,
                    Image = Guid.NewGuid().ToString() + "." + Path.GetExtension(item.FileName)
                };
                res.ArticleImage.Add(img);
              ticketContext.TicketImages.Add(img);  

            }
            ticketContext.Tickets.Add(res);
            ticketContext.SaveChanges();
            var ticketFiles = new List<IFormFile>();
            foreach (var item in ticket.TicketFile)
            {
                ticketFiles.Add(item);
            }
            foreach (var item in ticket.TicketImageFile)
            {
                ticketFiles.Add(item);
            }
            SaveImageFile(ticketFiles);
            return RedirectToAction(nameof(Index));
        }
        private void SaveImageFile(List<IFormFile> files)
        {
           
            foreach (var item in files)
            {
                string innerPath;
                if ((item?.Length > 0) && ((item?.ContentType == "image/jpeg") || (item?.ContentType == "image/jpg")))
               
                   innerPath= "\\ImageFiles\\";
                else
                innerPath= "\\TicketFiles\\";

                string path_root = Environment.WebRootPath;
                string newFileName = Guid.NewGuid().ToString() + Path.GetExtension(item.FileName);
                string path_to_file = path_root +"\\"+ newFileName;

                if (!Directory.Exists(path_to_file))

                    Directory.CreateDirectory(path_to_file);
                using (FileStream fs = System.IO.File.Create(path_to_file))
                {
                    item.CopyTo(fs);

                }
            }
           

        }


        [HttpPost]

        public ActionResult Uploadimage(List<IFormFile> upload)
        {
            var filepath = "";
            foreach (var photo in Request.Form.Files)
            {
                string serverpath =
  Path.Combine(Environment.WebRootPath, "\\Images\\", photo.FileName);
                using (var stream = new FileStream(serverpath, FileMode.Create))
                {
                    photo.CopyTo(stream);
                }
                var d = $"{Request.Scheme}://{Request.Host}";
                filepath = d +
                   "\\Images\\" + photo.FileName;

            }
            return Json(new { url = filepath });
        }
        // GET: ArticleController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ArticleController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ArticleController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ArticleController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
