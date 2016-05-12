﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebBookmarkBo.Model
{
  
    public partial class BizBookmarkInfo
    {
        public static BizBookmarkInfo LoadByID(long infoID)
        {
            var dataInfo = DAL.GetByBookmarkInfoID(infoID);
            if (dataInfo != null)
                return new BizBookmarkInfo(dataInfo);
            return new BizBookmarkInfo();
        }


        /// <summary>
        /// 通过FolderID 加载书签数据
        /// </summary>
        /// <param name="folderID"></param>
        /// <returns></returns>
        public static List<BizBookmarkInfo> LoadByFolderID(long folderID)
        {
            var lstModel = DAL.GetListByFolderID(folderID);
            if (lstModel == null)
                return new List<BizBookmarkInfo>();
            return lstModel.Select(model => new BizBookmarkInfo(model)).ToList();

        }


        public static List<BizBookmarkInfo> LoadByUID(long uid)
        {
            var lstBiz = new List<BizBookmarkInfo>();
            var lstModel = DAL.GetListByUID(uid);
            lstBiz = lstModel.Select(model => new BizBookmarkInfo(model)).ToList();
            return lstBiz;
        }


        public static BizBookmarkInfo LoadByUserIDAndHashcode(long userInfoID, int hashcode)
        {
            var model = DAL.GetByUserInfoIAndHashcode(userInfoID, hashcode);
            return model != null ? new BizBookmarkInfo(model) : null;
        }



    }
}
