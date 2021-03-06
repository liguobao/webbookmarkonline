﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBookmarkBo.Model;
using WebBookmarkBo.Service;
using WebBookmarkUI.Models;

namespace WebBookmarkUI.Controllers
{
    /// <summary>
    /// 用户信息相关页面
    /// </summary>
    public class UserInfoController : Controller
    {
       
        /// <summary>
        /// 当前用户信息
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            UIUserInfo uiUserInfo = null;

            if (!UILoginHelper.CheckUserLogin(HttpContext))
                return View(uiUserInfo);

            var result = UserInfoBo.GetUserInfoByLoginNameOrEmail(UILoginHelper.GetUIUserLoginNameOrEmail(HttpContext));
            if (result.IsSuccess)
            {
                var bizUserInfo = result.Target as BizUserInfo;
                if (bizUserInfo == null || bizUserInfo.UserInfoID == 0)
                    return View(uiUserInfo);
                uiUserInfo = new UIUserInfo();
                uiUserInfo.UserEmail = bizUserInfo.UserEmail;
                uiUserInfo.UserName = bizUserInfo.UserName;
                uiUserInfo.LoginName = bizUserInfo.UserLoginName;
                uiUserInfo.Phone = bizUserInfo.UserPhone;
                uiUserInfo.LoginName = bizUserInfo.UserLoginName;
                uiUserInfo.QQ = bizUserInfo.UserQQ;
                uiUserInfo.UserInfoComment = bizUserInfo.UserInfoComment;
                uiUserInfo.UserImagURL = bizUserInfo.UserImagURL;
            }
            return View(uiUserInfo);
        }

        /// <summary>
        /// 保存用户信息
        /// </summary>
        /// <param name="uiUserInfo"></param>
        /// <returns></returns>
        public ActionResult SaveUserInfo(UIUserInfo uiUserInfo)
        {
            var loginEmail = UILoginHelper.GetUIUserLoginNameOrEmail(HttpContext);
            return Json(SaveUserToDB(loginEmail, uiUserInfo));
        }

        /// <summary>
        /// 检查邮箱是否有效
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public ActionResult CheckUserEmail(string email)
        {
            var cookieLoginName = UILoginHelper.GetUIUserLoginNameOrEmail(HttpContext);
            if(!cookieLoginName.Contains('@'))
            {
               var userInfo = UserInfoBo.GetUserInfoByLoginNameOrEmail(cookieLoginName).Target as BizUserInfo;
               cookieLoginName = userInfo.UserEmail;
            }

            BizResultInfo result = new BizResultInfo();
            if (cookieLoginName.Equals(email))
            {
                result.IsSuccess = true;
                result.SuccessMessage = "邮箱是有效的哦，可以使用。";
                
            }else
            {
                result = UserInfoBo.CheckUserEmailOrLoginName(email);
            }
            return Json(result);
        }

        /// <summary>
        /// 检查登录名是否可用
        /// </summary>
        /// <param name="loginName"></param>
        /// <returns></returns>
        public ActionResult CheckUserLoginName(string loginName)
        {
            var cookieLoginName = UILoginHelper.GetUIUserLoginNameOrEmail(HttpContext);
            BizResultInfo result = UserInfoBo.GetUserInfoByLoginNameOrEmail(cookieLoginName);
            if (result.IsSuccess)
            {
                var userInfo = result.Target as BizUserInfo;
                if(!userInfo.UserLoginName.Equals(loginName))
                {
                    result = UserInfoBo.CheckUserEmailOrLoginName(loginName);
                }else
                {
                    result.IsSuccess = true;
                    result.SuccessMessage = "邮箱是有效的哦，可以使用。";
                }
            }
            return Json(result);
        }


        /// <summary>
        /// 保存用户数据到数据库
        /// </summary>
        /// <param name="loginEmail"></param>
        /// <param name="uiUserInfo"></param>
        /// <returns></returns>
        private BizResultInfo SaveUserToDB(String loginEmail, UIUserInfo uiUserInfo)
        {
           var result = UserInfoBo.GetUserInfoByLoginNameOrEmail(loginEmail);
            if (result.IsSuccess)
            {
                var userInfo = result.Target as BizUserInfo;
                userInfo.UserEmail = uiUserInfo.UserEmail;
                userInfo.UserInfoComment = uiUserInfo.UserInfoComment;
                userInfo.UserName = uiUserInfo.UserName;
                userInfo.UserQQ = uiUserInfo.QQ;
                userInfo.UserPhone = uiUserInfo.Phone;
                userInfo.UserLoginName = uiUserInfo.LoginName;
                userInfo.Save();
                result.IsSuccess = true;
                result.SuccessMessage = "保存成功了耶，你可以去别的地方玩了。";
            }
            else
            {
                result.IsSuccess = false;
                result.ErrorMessage = "没找到登陆信息呀呀呀！";
            }
            return (result);
        }

        /// <summary>
        /// 保存用户头像
        /// </summary>
        /// <returns></returns>
        public ActionResult SaveUserImag()
        {
            BizResultInfo result = UploadFileHelper.UploadFileToUserImg(Request);
            if(result.IsSuccess)
            {
                var loginEmail = UILoginHelper.GetUIUserLoginNameOrEmail(HttpContext);
                var res=  UserInfoBo.GetUserInfoByLoginNameOrEmail(loginEmail);
                var bizUserInfo = res.Target as BizUserInfo;
                var path = Server.MapPath(bizUserInfo.UserImagURL);
                if(System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }
                bizUserInfo.UserImagURL = result.ResultID;
                bizUserInfo.Save();
                result.SuccessMessage = "上传成功！";
            }
            return Json(result);
        }


        /// <summary>
        /// 展示用户资料
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        public ActionResult ShowUserDetail(long uid)
        {
            return View(uid);
        }


        /// <summary>
        /// 修改密码页面
        /// </summary>
        /// <returns></returns>
        public ActionResult ModifyPasswordIndex()
        {
            UIUserInfo uiUserInfo = null;

            if (!UILoginHelper.CheckUserLogin(HttpContext))
                return View(uiUserInfo);

            var result = UserInfoBo.GetUserInfoByLoginNameOrEmail(UILoginHelper.GetUIUserLoginNameOrEmail(HttpContext));
            if (result.IsSuccess)
            {
                var bizUserInfo = result.Target as BizUserInfo;
                if (bizUserInfo == null || bizUserInfo.UserInfoID == 0)
                    return View(uiUserInfo);
                uiUserInfo = new UIUserInfo();
                uiUserInfo.UserEmail = bizUserInfo.UserEmail;
                uiUserInfo.UserName = bizUserInfo.UserName;
                uiUserInfo.LoginName = bizUserInfo.UserLoginName;
                uiUserInfo.Phone = bizUserInfo.UserPhone;
                uiUserInfo.LoginName = bizUserInfo.UserLoginName;
                uiUserInfo.QQ = bizUserInfo.UserQQ;
                uiUserInfo.UserInfoComment = bizUserInfo.UserInfoComment;
                uiUserInfo.UserImagURL = bizUserInfo.UserImagURL;
            }
            return View(uiUserInfo);

        }


        /// <summary>
        /// 保存密码
        /// </summary>
        /// <param name="oldpassword"></param>
        /// <param name="newpassword"></param>
        /// <returns></returns>
        public ActionResult SavePassword(string oldpassword,string newpassword)
        {
            BizResultInfo  result = new BizResultInfo();
            if(string.IsNullOrEmpty(oldpassword) || string.IsNullOrEmpty(newpassword))
            {
                result.IsSuccess = false;
                result.ErrorMessage = "原密码和新密码都不能为空呀。";
                return Json(result);

            }

            string password = UILoginHelper.GetUIUserPassword(HttpContext);

            if(password != oldpassword)
            {
                result.IsSuccess = false;
                result.ErrorMessage = "原密码错误，请重新输入.";
                return Json(result);
            }

            long uid = UILoginHelper.GetUIDFromHttpContext(HttpContext);

            var userInfo = BizUserInfo.LoadByUserInfoID(uid);
            if(userInfo!=null && userInfo.UserInfoID!=0)
            {
                userInfo.UserPassword = newpassword;
                userInfo.Save();

                result.IsSuccess = true;
                result.SuccessMessage = "修改成功，请重新登陆一下哦。";
            }
            else
            {
                result.IsSuccess = false;
                result.ErrorMessage = "数据库里面找不到你丫，重新登陆一下看看。";
            }
            return Json(result);

        }

    }
}