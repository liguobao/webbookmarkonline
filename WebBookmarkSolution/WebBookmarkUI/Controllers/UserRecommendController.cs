﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBookmarkBo.Service;
using WebBookmarkUI.Models;

namespace WebBookmarkUI.Controllers
{
    /// <summary>
    /// 用户推荐页面
    /// </summary>
    public class UserRecommendController : Controller
    {
        

        public ActionResult Index()
        {
            var loginUid = UILoginHelper.GetUIDFromHttpContext(HttpContext);
            var dicBeFollowerID = UserRelationshipBo.GetByFollowUserID(loginUid);
            var lstUserInfoModel = new List<SearchUserInfo>();
            var lstModel = RecommendBo.GetRecommendUserList(loginUid);
            if (lstModel != null && lstModel.Count > 0)
            {
                lstUserInfoModel.AddRange(lstModel.Select(model => new SearchUserInfo()
                {
                    UserImagURL = model.UserImagURL,
                    UserEmail = model.UserEmail,
                    UserInfoComment = model.UserInfoComment,
                    UserName = model.UserName,
                    CreateTime = model.CreateTime,
                    UserInfoID = model.UserInfoID,
                    IsFollow = dicBeFollowerID.ContainsKey(model.UserInfoID)
                }));
            }
            return PartialView("Index", lstUserInfoModel);
        }

    }
}
