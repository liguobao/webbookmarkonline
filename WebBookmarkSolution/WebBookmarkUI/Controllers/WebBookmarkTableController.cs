using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Xml.Linq;
using WebBookmarkBo.Model;
using WebBookmarkService;
using WebBookmarkUI.Models;
using WebBookmarkBo;
using WebBookmarkBo.Service;

namespace WebBookmarkUI.Controllers
{
    public class WebBookmarkTableController : Controller
    {
        /// <summary>
        /// 显示书签夹
        /// </summary>
        /// <param name="folderID"></param>
        /// <returns></returns>
        public ActionResult Index(long folderID=0)
        {
            long uid = UILoginHelper.GetUIDFromHttpContext(HttpContext);
            if (folderID == 0)
            {
                var lst = BizUserWebFolder.LoadAllByUID(uid);
                if (lst != null && lst.Count>0)
                {
                    var firstFolder = lst.Where(folder => folder.ParentWebfolderID == 0);
                    if (firstFolder == null || firstFolder.Count()==0)
                        return View();
                    if(firstFolder.Count() == 1)
                    {
                        folderID = firstFolder.FirstOrDefault().UserWebFolderID;
                    }else
                    {
                        BizUserWebFolder newFolderInfo = new BizUserWebFolder();
                        newFolderInfo.UserInfoID = uid;
                        newFolderInfo.UserWebFolderID = 0;
                        newFolderInfo.ParentWebfolderID = 0;
                        newFolderInfo.ChildrenFolderList = firstFolder.ToList();
                        newFolderInfo.BizBookmarkInfoList = new List<BizBookmarkInfo>();
                        return View(new UIWebFolderInfo(newFolderInfo));
                    }
                  
                }
            }
            var folderInfo = BizUserWebFolder.LoadContainsChirdrenAndBookmark(folderID);
            var model = new UIWebFolderInfo(folderInfo);
            return View(model);
        }

        /// <summary>
        /// 显示某个书签夹数据
        /// </summary>
        /// <param name="folderID"></param>
        /// <returns></returns>
        public ActionResult ShowFolderTable(long folderID)
        {
            long uid = UILoginHelper.GetUIDFromHttpContext(HttpContext);
            if (folderID == 0)
            {
                var lst = BizUserWebFolder.LoadAllByUID(uid);
                if (lst != null && lst.Count >0)
                {
                    var firstFolder = lst.Where(folder => folder.ParentWebfolderID == 0);
                    if (firstFolder == null || firstFolder.Count()==0)
                        return View();
                    if (firstFolder.Count() == 1)
                    {
                        folderID = firstFolder.FirstOrDefault().UserWebFolderID;
                    }
                    else
                    {
                        BizUserWebFolder newFolderInfo = new BizUserWebFolder();
                        newFolderInfo.UserInfoID = uid;
                        newFolderInfo.UserWebFolderID = 0;
                        newFolderInfo.ParentWebfolderID = 0;
                        newFolderInfo.ChildrenFolderList = firstFolder.ToList();
                        newFolderInfo.BizBookmarkInfoList = new List<BizBookmarkInfo>();
                        return View("ShowFolderTable", new UIWebFolderInfo(newFolderInfo));
                    }
                }
            }
            var folderInfo = BizUserWebFolder.LoadContainsChirdrenAndBookmark(folderID);
            var model = new UIWebFolderInfo(folderInfo);
            return View("ShowFolderTable", model);
        }

        /// <summary>
        /// 显示当前书签夹次级书签夹数据（用于下拉框）
        /// </summary>
        /// <param name="folderID"></param>
        /// <returns></returns>
        public ActionResult ShowAddFolderOrBookmarkView(long folderID)
        {
            long uid = UILoginHelper.GetUIDFromHttpContext(HttpContext);
            if (folderID == 0)
            {
                var lst = BizUserWebFolder.LoadAllByUID(uid);
                if (lst != null && lst.Count >0 )
                {
                    var firstFolder = lst.Where(folder => folder.ParentWebfolderID == 0);
                    if (firstFolder == null || firstFolder.Count()==0)
                        return View();
                    if (firstFolder.Count() == 1)
                    {
                        folderID = firstFolder.FirstOrDefault().UserWebFolderID;
                    }
                    else
                    {
                        BizUserWebFolder newFolderInfo = new BizUserWebFolder();
                        newFolderInfo.UserInfoID = uid;
                        newFolderInfo.UserWebFolderID = 0;
                        newFolderInfo.ParentWebfolderID = 0;
                        newFolderInfo.ChildrenFolderList = firstFolder.ToList();
                        newFolderInfo.BizBookmarkInfoList = new List<BizBookmarkInfo>();
                        return View("ShowAddFolderOrBookmarkView", new UIWebFolderInfo(newFolderInfo));
                    }
                }
            }
            var folderInfo = BizUserWebFolder.LoadContainsChirdrenAndBookmark(folderID);
            var model = new UIWebFolderInfo(folderInfo);
            return View("ShowAddFolderOrBookmarkView", model);
        }




        /// <summary>
        /// FolderModel json转换成Model
        /// </summary>
        /// <param name="strModel"></param>
        /// <returns></returns>
        public ActionResult ConvertToUIWebFolderInfo(string strModel)
        {
            UIWebFolderInfo folderInfo = null;
            try
            {
                folderInfo = JsonConvert.DeserializeObject<UIWebFolderInfo>(strModel);
            }catch(Exception ex)
            {
                LogHelper.WriteException("UIWebFolderInfo反序列化失败", ex, new { Modle = strModel });
            }
            return Json(folderInfo);
        }


        /// <summary>
        /// Bookmark json 转换成Model
        /// </summary>
        /// <param name="strModel"></param>
        /// <returns></returns>
        public ActionResult ConvertToUIBookmarkInfo(string strModel)
        {
            UIBookmarkInfo bookmark = null;
            try
            {
                bookmark = JsonConvert.DeserializeObject<UIBookmarkInfo>(strModel);
            }
            catch (Exception ex)
            {
                LogHelper.WriteException("UIBookmarkInfo反序列化失败", ex, new { Modle = strModel });
            }
            return Json(bookmark);
        }

        /// <summary>
        /// 添加书签/书签夹
        /// </summary>
        /// <param name="name"></param>
        /// <param name="href"></param>
        /// <param name="folderID"></param>
        /// <param name="type"></param>
        /// <param name="infoID"></param>
        /// <returns></returns>
        public ActionResult AddBookmark(string name,string href,long folderID,string type,long infoID)
        {
            BizResultInfo result = new BizResultInfo();
            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(type))
            {
                result.ErrorMessage = "名称和类型必须填写呀。";
                result.IsSuccess = false;
                return Json(result);
            }
            var loginUid = UILoginHelper.GetUIDFromHttpContext(HttpContext);

            try
            {
                if(type=="bookmark")
                {
                    BizBookmarkInfo bookmark = new BizBookmarkInfo();

                    if(infoID!=0)
                    {
                        bookmark = BizBookmarkInfo.LoadByID(infoID);
                    }

                   
                    bookmark.Href = href;
                    bookmark.BookmarkName = name;
                    bookmark.UserWebFolderID = folderID;
                    bookmark.CreateTime = DateTime.Now;
                    bookmark.Host = href.GetHost();
                    bookmark.UserInfoID = loginUid;
                    bookmark.HashCode = bookmark.GetHashCode();
                    bookmark.Save();
                   
                    

                }else
                {
                    BizUserWebFolder folder = new BizUserWebFolder();
                    if(infoID!=0)
                    {
                        folder = BizUserWebFolder.LoadByID(infoID);
                    }

                    folder.UserInfoID =loginUid;
                    folder.WebFolderName = name;
                    folder.ParentWebfolderID = folderID;
                    folder.IntroContent = "";
                    folder.IElementJSON = "";
                    folder.CreateTime = DateTime.Now;
                    folder.IElementHashcode = folder.GetHashCode();
                    folder.Save();
                }

               

                result.IsSuccess = true;
                result.SuccessMessage = "保存成功了耶，刷新一下页面就能看到了哦。";

            }catch(Exception ex)
            {
                result.IsSuccess = false;
                result.SuccessMessage = "保存失败，可能是数据库不开心了吧，重新保存试试。";
                LogHelper.WriteException("AddBookmark Exception", ex, new { Name = name,Href = href,FolderID =folderID });
            }
            return Json(result);

        }

        /// <summary>
        /// 删除书签/书签夹
        /// </summary>
        /// <param name="infoID"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public ActionResult DeleteBookmarkOrWebFolder(long infoID,string type)
        {
            BizResultInfo result = new BizResultInfo();
            try
            {
                if(type=="folder")
                {
                    result.IsSuccess = UserWebFolderBo.DeleteWebfolder(infoID);
                    result.SuccessMessage = "删除成功!";
                }
                else
                {
                    result.IsSuccess = BookmarkInfoBo.DeleteByBookmarkInfoID(infoID);
                    result.SuccessMessage = "删除成功!";
                }
            }
            catch(Exception ex)
            {
                LogHelper.WriteException("", ex, new { InfoID = infoID});
                result.IsSuccess =false;
                result.ErrorMessage = "操作失败，可能这就是命吧!要不你重试一下？";
            }

            return Json(result);
        }
        
        /// <summary>
        /// 导入书签文件主页
        /// </summary>
        /// <returns></returns>
        public ActionResult ImportWebBookmark()
        {
            return View();
        }

        /// <summary>
        /// 保存书签数据到数据库
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public ActionResult ImportWebBookmarkToDB(string filePath)
        {
            BizResultInfo result = new BizResultInfo();

            if(string.IsNullOrEmpty(filePath))
            {
                result.IsSuccess = false;
                result.ErrorMessage = "先上传文件呀。";
            }
            long uid = UILoginHelper.GetUIDFromHttpContext(HttpContext);

            var path = Server.MapPath(filePath);

            result = ImportBookmarkHelper.ImportBookmarkDataToDB(path,uid);


            return Json(new BizResultInfo() { IsSuccess = true,SuccessMessage="保存成功耶，你可以到别的地方玩了。" });
        }

      

        /// <summary>
        /// 上传书签文件到系统
        /// </summary>
        /// <returns></returns>
        public ActionResult UploadWebBookmarkFile()
        {
            var result = UploadFileHelper.UploadFileToUserImportFile(Request);
            if(result.IsSuccess)
            {
                BizUserWebBookmarkImportLog importLog = new BizUserWebBookmarkImportLog();
                importLog.CreateTime = DateTime.Now;
                importLog.UserInfoID = UILoginHelper.GetUIDFromHttpContext(HttpContext);
                importLog.Path = result.ResultID;
                importLog.FileName = result.ResultID;
                importLog.Save();
            }

            return Json(result);
        }

        /// <summary>
        /// 预览书签文件
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public FileResult PreView(string path)
        {
            return File(path, "text/html");
        }

        /// <summary>
        /// 展示书签数据
        /// </summary>
        /// <param name="folderID"></param>
        /// <returns></returns>
        public ActionResult ShowUserAllFolder(long folderID)
        {
            long uid = UILoginHelper.GetUIDFromHttpContext(HttpContext);
            if (folderID == 0)
            {
                var lst = BizUserWebFolder.LoadAllByUID(uid);
                if (lst != null && lst.Count > 0)
                {
                    var firstFolder = lst.Where(folder => folder.ParentWebfolderID == 0);
                    if (firstFolder == null || firstFolder.Count() == 0)
                        return View();
                    folderID = firstFolder.FirstOrDefault().UserWebFolderID;
                }
            }
            var folderInfo = BizUserWebFolder.LoadContainsChirdrenAndBookmark(folderID);
            var model = new UIWebFolderInfo(folderInfo);
            return View("ShowUserAllFolder", model);
        }

    }
}