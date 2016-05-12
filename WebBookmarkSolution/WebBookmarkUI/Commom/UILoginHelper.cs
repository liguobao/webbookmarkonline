using System.Web;
using System.Web.Mvc;
using WebBookmarkBo.Service;
using WebBookmarkBo;

namespace WebBookmarkUI
{
    public class UILoginHelper
    {

        /// <summary>
        /// 获取登录的邮箱/用户名
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        public static string GetUIUserLoginNameOrEmail(HttpContextBase httpContext)
        {
            string loginNameOrEmail = string.Empty;
            var userInfo = httpContext.Request.Cookies["UserInfo"];
            if (userInfo != null)
                loginNameOrEmail = userInfo.Values["LoginNameOrEmail"].ConvertToPlaintext();
            else
            {
               var sessioninfo= httpContext.Session["LoginNameOrEmail"];
               if (sessioninfo!=null)
                {
                    loginNameOrEmail = sessioninfo.ToString().ConvertToPlaintext();
                }
            }
            return loginNameOrEmail;
        }

        /// <summary>
        /// 获取用户密码
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        public static string GetUIUserPassword(HttpContextBase httpContext)
        {
            string password = string.Empty;
            var userInfo = httpContext.Request.Cookies["UserInfo"];
            if (userInfo != null)
                password = userInfo.Values["Token"].ConvertToPlaintext();
            else
            {
                var sessioninfo = httpContext.Session["Token"];
                if (sessioninfo != null)
                {
                    password = sessioninfo.ToString().ConvertToPlaintext();
                }
            }
            return password;
        }


        /// <summary>
        /// 获取当前用户UID
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        public static long GetUIDFromHttpContext(HttpContextBase httpContext)
        {
            long uid = 0;
            var userInfo = httpContext.Request.Cookies["UserInfo"];
            if (userInfo != null)
                uid = userInfo.Values["UID"].ConvertToPlainLong();
            else
            {
                var sessioninfo = httpContext.Session["UID"];
                if (sessioninfo != null)
                {
                    uid = sessioninfo.ToString().ConvertToPlainLong();
                }
            }
            return uid;
        }

        /// <summary>
        /// 写入用户登录信息
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="loginNameOrEmail"></param>
        /// <param name="password"></param>
        /// <param name="httpContext"></param>
        public static void WriteUserInfo(string uid, string loginNameOrEmail, string password, HttpContextBase httpContext)
        {
            HttpCookie cookie = new HttpCookie("UserInfo");
            cookie.Values.Add("UID", uid);
            cookie.Values.Add("LoginNameOrEmail", loginNameOrEmail.ConvertToCiphertext());
            cookie.Values.Add("Token", password.ConvertToCiphertext());
            httpContext.Response.Cookies.Add(cookie);

            httpContext.Session.Add("UID", uid);
            httpContext.Session.Add("LoginNameOrEmail", loginNameOrEmail.ConvertToCiphertext());
            httpContext.Session.Add("Token", password.ConvertToCiphertext());
        }


        /// <summary>
        /// 校验是否已登录
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        public static bool CheckUserLogin(HttpContextBase httpContext)
        {
            var rsp = UserInfoBo.UserLogin(GetUIUserLoginNameOrEmail(httpContext), GetUIUserPassword(httpContext));
            return rsp.IsSuccess;  
        }
    }
}