﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WebBookmarkBo.Service;
using WebBookmarkBo;
using WebBookmarkUI.Models;

namespace WebBookmarkUI.Controllers
{
    /// <summary>
    /// 书签推荐异步control
    /// </summary>
    public class AsyncShowBookmarkRecommendController : AsyncController
    {
        /// <summary>
        /// 使用IOCP异步方式返回书签推荐数据
        /// </summary>
        /// <returns></returns>
        public Task<ViewResult> Show()
        {
            return Task.Factory.StartNew(() => 
            {
                var loginUID = UILoginHelper.GetUIDFromHttpContext(HttpContext);
                var lstBookmarkInfo = RecommendBo.GetRecommendBookmarkList(loginUID);

                var dicUserInfo = UserInfoBo.GetListByUIDList(lstBookmarkInfo.Select(model => model.UserInfoID).ToList())
                    .ToDictionary(model => model.UserInfoID, model => model);

                var lstModel = new List<UIBookmarkInfo>();
                lstModel.AddRange(lstBookmarkInfo.Select(model => new UIBookmarkInfo()
                {
                    Href = model.Href,
                    BookmarkName = model.BookmarkName,
                    BookmarkInfoID = model.BookmarkInfoID,
                    CreateTime = model.CreateTime,
                    UserWebFolderID = model.UserWebFolderID,
                    UserInfoID = model.UserInfoID,
                    Host = model.Host,
                    UserInfo = dicUserInfo.ContainsKey(model.UserInfoID) ? new UIUserInfo()
                    {
                        UserEmail = dicUserInfo[model.UserInfoID].UserEmail,
                        UserInfoID = dicUserInfo[model.UserInfoID].UserInfoID,
                        UserName = dicUserInfo[model.UserInfoID].UserName,
                        UserImagURL = dicUserInfo[model.UserInfoID].UserImagURL,
                    } : new UIUserInfo() { UserName = "这个人消失了", UserInfoID = 0 },
                }));

                lstModel = Extend.GetRandomList(lstModel);
                return (lstModel.Count > 15 ? lstModel.Take(15).ToList() : lstModel);
            })
            .ContinueWith(t => View(t.Result));
        }

        /// <summary>
        /// 功能同上，此处为同步返回数据
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            var loginUID = UILoginHelper.GetUIDFromHttpContext(HttpContext);
            var lstBookmarkInfo = RecommendBo.GetRecommendBookmarkList(loginUID);

            var dicUserInfo = UserInfoBo.GetListByUIDList(lstBookmarkInfo.Select(model => model.UserInfoID).ToList())
                .ToDictionary(model => model.UserInfoID, model => model);

            var lstModel = new List<UIBookmarkInfo>();
            lstModel.AddRange(lstBookmarkInfo.Select(model => new UIBookmarkInfo()
            {
                Href = model.Href,
                BookmarkName = model.BookmarkName,
                BookmarkInfoID = model.BookmarkInfoID,
                CreateTime = model.CreateTime,
                UserWebFolderID = model.UserWebFolderID,
                UserInfoID = model.UserInfoID,
                Host = model.Host,
                UserInfo = dicUserInfo.ContainsKey(model.UserInfoID) ? new UIUserInfo()
                {
                    UserEmail = dicUserInfo[model.UserInfoID].UserEmail,
                    UserInfoID = dicUserInfo[model.UserInfoID].UserInfoID,
                    UserName = dicUserInfo[model.UserInfoID].UserName,
                    UserImagURL = dicUserInfo[model.UserInfoID].UserImagURL,
                } : new UIUserInfo() { UserName = "这个人消失了", UserInfoID = 0 },
            }));

            lstModel = Extend.GetRandomList(lstModel);
            return View(lstModel.Count > 15 ? lstModel.Take(15).ToList() : lstModel);
        }
    }
}
