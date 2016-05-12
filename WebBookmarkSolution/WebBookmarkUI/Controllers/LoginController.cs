using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBookmarkBo.Model;
using WebBookmarkBo.Service;
using WebBookmarkUI.Models;
using WebBookmarkBo;

namespace WebBookmarkUI.Controllers
{
    /// <summary>
    /// 登录页面
    /// </summary>
    public class LoginController : Controller
    {
        /// <summary>
        /// 展示用户名
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        public ActionResult Index(string uid="")
        {
            UIUserInfo uiUserInfo = null;
            if(string.IsNullOrEmpty(uid))
                return View(uiUserInfo);

            var result= UserInfoBo.GetUserInfoByUID(uid.ConvertToPlainLong());
            if(result.IsSuccess)
            {
                BizUserInfo bizUserInfo = result.Target as BizUserInfo;
                if(bizUserInfo==null || bizUserInfo.UserInfoID ==0)
                 return   View(uiUserInfo);
                uiUserInfo = new UIUserInfo();
                uiUserInfo.UserEmail = bizUserInfo.UserEmail;
                uiUserInfo.UserName = bizUserInfo.UserName;
                uiUserInfo.LoginName = bizUserInfo.UserLoginName;
            }
            return View(uiUserInfo);
        }


        /// <summary>
        /// 登录操作
        /// </summary>
        /// <param name="uiUserInfo"></param>
        /// <returns></returns>
        public ActionResult UserLogin(UIUserInfo uiUserInfo)
        {
            string logionName = string.IsNullOrEmpty(uiUserInfo.LoginName) ? uiUserInfo.UserEmail : uiUserInfo.LoginName;
            var rsp = UserInfoBo.UserLogin(logionName, uiUserInfo.Password);
            if(rsp.IsSuccess)
            {

                UILoginHelper.WriteUserInfo(rsp.ResultID, logionName, uiUserInfo.Password, HttpContext);
                
            }
            return Json(rsp);
        }


      
       
    }
}