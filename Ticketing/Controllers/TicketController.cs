using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
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
            using var connection = new SqlConnection("Server=.\\SQL2019;Database=Ticket;Trusted_Connection=True;Trust Server Certificate=true");
            var options = new DbContextOptionsBuilder<TicketContext>()
                .UseSqlServer(connection)
                .Options;

            using var context1 = new TicketContext(options);

            using var transaction = context1.Database.BeginTransaction();
            try
            {

                var res = new Ticket()
                {
                    Title = ticket.Title,
                    //ArticleImage = Guid.NewGuid().ToString() + "." + Path.GetExtension(ticket.TicketImageFile.FileName),
                    Content = ticket.Content,
                    ShortDescription = ticket.ShortDescription

                };
                res.ArticleImage = new List<TicketImage>();

                foreach (var item in ticket.TicketFile)
                {
                    if ((item?.Length > 0) && ((item?.ContentType == "image/jpeg") || (item?.ContentType == "image/jpg")))
                    {
                        var img = new TicketImage()
                        {
                            TicketId = res.Id,
                            Image = Guid.NewGuid().ToString() + "." + Path.GetExtension(item.FileName)
                        };
                        res.ArticleImage.Add(img);
                        //ticketContext.TicketImages.Add(img);  
                    }
                   
                }
                ticketContext.Tickets.Add(res);
                ticketContext.SaveChanges();
               
                if(ticket.TicketFile!=null)
                SaveImageFile(ticket.TicketFile);

                transaction.Commit();
            }
            catch (Exception)
            {
               transaction.Rollback();  
            }

            return RedirectToAction(nameof(Index));
        }
        private void SaveImageFile(List<IFormFile> files)
        {
           
            foreach (var item in files)
            {
                string filePath;
                if ((item?.Length > 0) && ((item?.ContentType == "image/jpeg") || (item?.ContentType == "image/jpg")))
                    filePath =  Path.Combine(Environment.WebRootPath, "ImageFiles");
               
                else
                   filePath= Path.Combine(Environment.WebRootPath, "TicketFiles");

                // string path_root = Environment.WebRootPath;
                string newFileName = Guid.NewGuid().ToString() + Path.GetExtension(item.FileName);
               
                if (!Directory.Exists(filePath))

                    Directory.CreateDirectory(filePath);
                using (FileStream fs = System.IO.File.Create(Path.Combine(filePath, newFileName)))
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
