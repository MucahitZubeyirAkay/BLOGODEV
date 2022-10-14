using BLOGODEV.Filters;
using BLL.DesignPatterns.GenericRepositoryPattern.IntRep;
using ENTITIES.Entity.Concrete;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ENTITIES.Entity.Abstract;
using BLOGODEV.Models;
using BLOGODEV.Managers;

namespace BLOGODEV.Controllers
{
    [LoggedUser]
    public class ArticleController : Controller
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IGenericRepository<Article> _genericRepository;

        public ArticleController(IWebHostEnvironment webHostEnvironment, IGenericRepository<Article> genericRepository)
        {
            _webHostEnvironment = webHostEnvironment;
            _genericRepository = genericRepository;
        }

        public IActionResult List()
        {
            var list = _genericRepository.Where(x => x.Status != Status.Passive);

            return View(list);
        }

        public IActionResult Create(string yonlen)
        {
            ViewBag.yonlen = yonlen;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ArticleCreateViewModel model, string yonlen)
        {
            if (ModelState.IsValid)
            {
                var article = new Article()
                {
                    Title = model.Title,
                    Content = model.Content,
                    AuthorId = int.Parse(HttpContext.Session.GetString("userId")),
                    ArticlePicture = model.ArticlePicture.GetUniqueNameAndSavePhotoToDisk(_webHostEnvironment)
                };
                _genericRepository.Add(article);
                TempData["message"] = "Makaleniz eklendi!";
                if (string.IsNullOrEmpty(yonlen))
                {
                    return RedirectToAction("List");
                }
                return Redirect(yonlen);
            }
            else
                return View();
        }

        //public IActionResult Profile()
        //{
        //    return View();
        //}

        //public IActionResult Delete()
        //{
        //    _genericRepository.Remove(article)
        //}
      

    }
}

