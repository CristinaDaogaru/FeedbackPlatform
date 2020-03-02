using FeedBackPlatformWeb.Controllers.Base;
using FeedBackPlatformWeb.Models;
using FeedBackPlatformWeb.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FeedBackPlatformWeb.Controllers
{
    [Utils.Attributes.AuthorizeUser]
    public class HomeController : BaseController
    {
        #region Actions
        public ActionResult Dashboard()
        {




            this.SetHeaderAndMenu(Constants.Home_DashboardPage);
            return View();
        }

        public ActionResult About()
        {




            this.SetHeaderAndMenu(Constants.Home_AboutPage);
            return View();
        }
        #endregion


        #region Private Methods
        private void SetHeaderAndMenu(string pageKey)
        {
            ViewBag.Sidebar = new SidebarModel
            {
                CurrentPage = pageKey
            };
        }
        #endregion
    }
}