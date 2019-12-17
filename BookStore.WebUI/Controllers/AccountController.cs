using BookStore.WebUI.Infrastrusture.Abstract;
using BookStore.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookStore.WebUI.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        private IAuthProvider authprovider;
        public AccountController(IAuthProvider aut)
        {
            authprovider = aut;
        } 
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel model,string returnUrl)
        {
            if (ModelState.IsValid)
            {
                if (authprovider.Authenticate(model.username, model.password))
                    return Redirect(returnUrl ?? Url.Action("Index", "Admin"));
                else
                {
                    ModelState.AddModelError("", "InCorrect Username/Password");
                    return View();
                }
            }
            else
            {
                return View();
            }
            
        }
    }
}