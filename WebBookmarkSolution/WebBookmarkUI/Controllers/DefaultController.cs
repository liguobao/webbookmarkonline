using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBookmarkBo.Service;
using WebBookmarkUI.Commom;

namespace WebBookmarkUI.Controllers
{
    /// <summary>
    /// 首页（主要用于显示用户动态）
    /// </summary>
    public class DefaultController : Controller
    {
        /// <summary>
        /// 获取当前用户关注者相关动态
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            var loginUID = UILoginHelper.GetUIDFromHttpContext(HttpContext);
            var lstModel = UserDynamicInfoBo.LoadDynamicLog(loginUID);
            return View(lstModel);
        }
    }
}