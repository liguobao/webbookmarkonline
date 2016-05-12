using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBookmarkBo.Model;

namespace WebBookmarkUI.Controllers
{
    public class MessageListController : Controller
    {
        //
        // GET: /MessageList/

        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 所有的消息
        /// </summary>
        /// <returns></returns>
        public ActionResult ShowAllMessage()
        {
            var loginUID = UILoginHelper.GetUIDFromHttpContext(HttpContext);
            var lstModel = BizMessageInfo.LoadByUserID(loginUID);
            return View("ShowAllMessage", lstModel);
        }

        /// <summary>
        /// 未读消息
        /// </summary>
        /// <returns></returns>
        public ActionResult ShowNotReadMessage()
        {
            var loginUID = UILoginHelper.GetUIDFromHttpContext(HttpContext);
            var lstModel = BizMessageInfo.LoadNotReadListByUserID(loginUID);
            return View("ShowNotReadMessage", lstModel);
        }

        /// <summary>
        /// 已读消息
        /// </summary>
        /// <returns></returns>
        public ActionResult ShowHasReadMessage()
        {
            var loginUID = UILoginHelper.GetUIDFromHttpContext(HttpContext);
            var lstModel = BizMessageInfo.LoadHasReadListByUserID(loginUID);
            return View("ShowHasReadMessage", lstModel);
        }


        /// <summary>
        /// 展示消息内容
        /// </summary>
        /// <param name="messageID"></param>
        /// <returns></returns>
        public ActionResult ShowMessageContent(long messageID)
        {
            var model = BizMessageInfo.LoadByMessageID(messageID);
            if(model!=null && model.IsRead ==(int) MessageReadStatus.NotRead)
            {
                model.SetToHasRead();
            }
            return View(model);
        }

    }
}
