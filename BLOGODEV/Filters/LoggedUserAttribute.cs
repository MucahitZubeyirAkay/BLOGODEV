using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace BLOGODEV.Filters
{
    public class LoggedUserAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            string userId = context.HttpContext.Session.GetString("userId");

            if (string.IsNullOrEmpty(userId))
            {
                string routePath = context.HttpContext.Request.Path; //Kullanıcı eğer sayfaya, loogeduserattribut'ündan dolayı giriş yapamıyor ve login sayfasına yönlendiriliyorsa, bu satır login sayfasına gelirken url'i saklamak için kullanılır, çünkü giriş yapıldaktan tekrardan aynı sayfaya dönmek gerekir.

                context.Result = new RedirectToActionResult("Login", "Auth", new { yonlen = routePath });
            }
        }
    }
}
