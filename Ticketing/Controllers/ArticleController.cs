using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Ticketing.Models.Articles;

namespace Ticketing.Controllers
{
    public class ArticleController : Controller
    {
        private readonly TicketContext ticketContext;
        private IWebHostEnvironment Environment;


        public ArticleController( TicketContext ticketContext , IWebHostEnvironment environment)
        {
            this.ticketContext = ticketContext;
            Environment = environment;
        }
        // GET: ArticleController
        public ActionResult Index()
        {
            var list = ticketContext.Articles.ToList(); 
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
            ViewBag.CategoryId = new SelectList(ticketContext.Categories.ToList(), "Id", "Title");
            return View();
        }

        // POST: ArticleController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateArticleVewModel article)
        {

            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "مقادیر را به درستی وارد کنید");
                ViewBag.CategoryId = new SelectList(ticketContext.Categories.ToList(), "Id", "Title", article.CategoryId);
                return View(article);
            }

            var res = new Article()
            {
                Title = article.Title,
                CategoryId = article.CategoryId,
                ArticleImage = Guid.NewGuid().ToString() + "." + Path.GetExtension(article.ArticleImageFile.FileName),
                Content = article.Content,
                ShortDescription = article.ShortDescription

            };

           ticketContext.Articles.Add(res);
            ticketContext.SaveChanges();    

            SaveImageFile(article.ArticleImageFile, res.ArticleImage);

           

            return RedirectToAction(nameof(Index));
        }
        private void SaveImageFile(IFormFile formFile, string Title)
        {
            string FilePath = Path.Combine(Environment.WebRootPath, "ArticleImages");

            if (!Directory.Exists(FilePath))

                Directory.CreateDirectory(FilePath);

            var filePath = Path.Combine(FilePath, Title);



            using (FileStream fs = System.IO.File.Create(filePath))

            {

                formFile.CopyTo(fs);

            }

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
