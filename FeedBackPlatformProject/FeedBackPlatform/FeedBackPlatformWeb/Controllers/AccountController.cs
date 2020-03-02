using FeedBackPlatformDatabase.DAL;
using FeedBackPlatformDatabase.Models;
using FeedBackPlatformWeb.Controllers.Base;
using FeedBackPlatformWeb.Models.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FeedBackPlatformWeb.Controllers
{
    public class AccountController : BaseController
    {
        #region Actions
        [HttpGet]
        public ActionResult Login()
        {
            var model = new LoginModel();
            return View(model);
        }
        [HttpPost]
        public ActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                using (var uow = new UnitOfWork())
                {
                    var crypto = new PasswordEncode();

                    var user = uow.UserRepository.GetUserByEmailAndPassword(model.Username, crypto.Hash(model.Password));
                    if (user != null)
                    {
                        this.SetUserContext(user);
                        return Redirect("/");
                    }

                    ModelState.AddModelError("", "Invalid login attempt.");
                }
            }

            return View(model);
        }

        [HttpGet]
        public ActionResult LogOut()
        {
            this.UserContext = null;
            Session.Abandon();
            return RedirectToAction("Login", "Account");
        }


        [HttpGet]
        public ActionResult Register()
        {
            var model = new RegisterModel();
            return View(model);
        }
        [HttpPost]
        public ActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                using (var uow = new UnitOfWork())
                {
                    var isEmailUsed = uow.UserRepository.IsEmailUsed(model.Email);
                    if (isEmailUsed)
                    {
                        ModelState.AddModelError("Email", string.Format("Email '{0}' is already taken", model.Email));
                    }
                    else
                    {
                        var crypto = new PasswordEncode();
                        var encrypPass = crypto.Hash(model.Password);

                        var user = new User
                        {
                            Email = model.Email,
                            Password = encrypPass,
                            FirstName = model.FirstName,
                            LastName = model.LastName
                        };
                        uow.UserRepository.Insert(user);
                        uow.Save();


                        this.SetUserContext(user);

                        return Redirect("/");
                    }
                }
            }

            return View(model);
        }


        [HttpGet]
        public ActionResult Reset()
        {
            var model = new ResetModel();
            return View(model);
        }
        [HttpPost]
        public ActionResult Reset(ResetModel model)
        {
            return View(model);
        }
        #endregion


        #region Private Methods
        private void SetUserContext(User user)
        {
            this.UserContext = new Utils.UserContext
            {
                IsAuthenticated = true,
                UserId = user.Id,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName
            };
        }
        #endregion
    }
}