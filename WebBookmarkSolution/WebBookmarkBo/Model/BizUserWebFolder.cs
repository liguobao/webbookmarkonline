﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebBookmarkBo.Service;
using WebBookmarkService;
using WebBookmarkService.DAL;
using WebBookmarkService.Model;

namespace WebBookmarkBo.Model
{
    public class BizUserWebFolder
    {
        #region 属性

        private static readonly UserWebFolderDAL DAL = new UserWebFolderDAL();

        /// <summary>
        /// 主键，自增
        /// </summary>
        public long UserWebFolderID { get; set; }

        /// <summary>
        /// 收藏夹名称
        /// </summary>
        public string WebFolderName { get; set; }

        /// <summary>
        /// 用户ID
        /// </summary>
        public long UserInfoID { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 等级,0:对外公开，1：对关注者公开，2对群组公开，3：仅自己可见
        /// </summary>
        public int Grade { get; set; }

        /// <summary>
        /// 父收藏夹ID
        /// </summary>
        public long ParentWebfolderID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string IntroContent { get; set; }


        public string IElementJSON { get; set; }

        public int IElementHashcode { get; set; }


        public List<BizBookmarkInfo> BizBookmarkInfoList { get; set; }

        public List<BizUserWebFolder> ChildrenFolderList { get; set;}



        #endregion


        public UserWebFolder ToModel()
        {

            return new UserWebFolder()
            {
                UserInfoID= UserInfoID,
                UserWebFolderID=UserWebFolderID,
                IntroContent = IntroContent,
                CreateTime=CreateTime,
                ParentWebfolderID = ParentWebfolderID,
                Grade = Grade,
                WebFolderName = WebFolderName,
                IElementJSON = IElementJSON,
                IElementHashcode = IElementHashcode,
            };

        } 

        public BizUserWebFolder (UserWebFolder dataInfo)
        {
            UserInfoID = dataInfo.UserInfoID;
            UserWebFolderID = dataInfo.UserWebFolderID;
            IntroContent = dataInfo.IntroContent;
            CreateTime = dataInfo.CreateTime;
            ParentWebfolderID = dataInfo.ParentWebfolderID;
            Grade = dataInfo.Grade;
            WebFolderName = dataInfo.WebFolderName;
            IElementJSON = dataInfo.IElementJSON;
            IElementHashcode = dataInfo.IElementHashcode;
        }

        public BizUserWebFolder()
        {

        }

        public void Save ()
        {
            if(UserWebFolderID !=0)
            {
                DAL.Update(ToModel());

            }else
            {
                DAL.Add(ToModel());
                var model = DAL.GetByUserInfoIDAndHashcode(UserInfoID, IElementHashcode);
                if (model != null)
                {
                    UserWebFolderID = model.UserWebFolderID;

                }
                CreateDynamicInfo();
            }
        }

        private void CreateDynamicInfo()
        {
            Task.Factory.StartNew(() =>
            {
                try
                {
                   
                    UserDynamicInfoBo.CreateDynamicInfoMessage(UserInfoID, DynamicInfoType.NewWebFolder, this);
                }
                catch (Exception ex)
                {
                    LogHelper.WriteException("CreateDynamicInfoMessage", ex, new { UserInfoID = UserInfoID, Folder = this });
                }

            });
        }


        public static BizUserWebFolder LoadByID(long infoID)
        {
            var dataInfo = DAL.GetByUserWebFolderID(infoID);
           if (dataInfo != null)
               return new BizUserWebFolder(dataInfo);
           

           return new BizUserWebFolder();
        }

        /// <summary>
        /// 通过书签夹ID获取书签数据（包括子书签夹和书签数据）
        /// </summary>
        /// <param name="folderID"></param>
        /// <returns></returns>
        public static BizUserWebFolder LoadContainsChirdrenAndBookmark(long folderID)
        {
            var dataInfo = DAL.GetByUserWebFolderID(folderID);
            if (dataInfo != null)
            {
                return new BizUserWebFolder(dataInfo) 
                {
                    ChildrenFolderList = LoadByParentWebfolderID(folderID),
                    BizBookmarkInfoList = BizBookmarkInfo.LoadByFolderID(folderID)
                };
            }
               
            return new BizUserWebFolder();
        }

        public static List<BizUserWebFolder> LoadByParentWebfolderID(long parentWebfolderID)
        {
            var list = DAL.GetByParentWebfolderID(parentWebfolderID);
            if (list == null)
                return new List<BizUserWebFolder>();
            return list.Select(info=>new BizUserWebFolder(info)).ToList();
           
        }


        public static List<BizUserWebFolder> LoadAllByUID(long uid)
        {
            List<BizUserWebFolder> list = new List<BizUserWebFolder>();
            var lstModel = DAL.GetByUID(uid);
            if(lstModel!=null)
            {
                list.AddRange(lstModel.Select(model=>new BizUserWebFolder(model)));
            }
            return list;
        }

        

    }
}
