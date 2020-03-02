using FeedBackPlatformWeb.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FeedBackPlatformWeb.Controllers.Base
{
    public abstract class BaseController: Controller
    {
        public UserContext UserContext
        {
            get
            {
                var userContext = this.Session[Constants.UserContext_key] as UserContext;
                if (userContext == null)
                    userContext = new UserContext();
                return userContext;
            }
            set
            {
                this.Session[Constants.UserContext_key] = value;
            }
        }
    }
}