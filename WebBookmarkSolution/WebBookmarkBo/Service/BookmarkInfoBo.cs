﻿using System;
using System.Collections.Generic;
using System.Linq;
using WebBookmarkBo.Model;
using WebBookmarkService.DAL;

namespace WebBookmarkBo.Service
{
    public class BookmarkInfoBo
    {
        private static BookmarkInfoDAL DAL = new BookmarkInfoDAL();

        /// <summary>
        /// 批量插入书签数据
        /// </summary>
        /// <param name="lstBizBookmarkInfo"></param>
        /// <returns></returns>
        public static BizResultInfo BatchSaveToDB(List<BizBookmarkInfo> lstBizBookmarkInfo)
        {
            BizResultInfo result = new BizResultInfo();
            if(lstBizBookmarkInfo==null || lstBizBookmarkInfo.Count==0)
            {
                result.IsSuccess = false;
                result.ErrorMessage = "数据是空的呀，我就直接返回了....";
                return result;
            }
            var lstModel = lstBizBookmarkInfo.Select(info => info.ToModel()).ToList();
            var isSuccess = DAL.BatchInsert(lstModel);
            result.IsSuccess = isSuccess;
            if (!isSuccess)
                result.ErrorMessage = "可能是海底光纤挂了...重新试一下咯！(打死不认是程序的问题！！！)";
            return result;
        }


        /// <summary>
        /// 通过用户ID获取书签数据
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        public static List<BizBookmarkInfo> LoadByUID(long uid)
        {
            var lstBiz = new List<BizBookmarkInfo>();
            var lstModel = DAL.GetListByUID(uid);
            lstBiz = lstModel.Select(model => new BizBookmarkInfo(model)).ToList();
            return lstBiz;
        }

        /// <summary>
        /// 根据书签夹ID删除书签数据
        /// </summary>
        /// <param name="webfolderID"></param>
        /// <returns></returns>
        public static bool DeleteByWebFolderID(long webfolderID)
        {
            return  DAL.DeleteByWebFolderID(webfolderID) >=0;
        }

        /// <summary>
        /// 删除书签
        /// </summary>
        /// <param name="bookmarkInfoID"></param>
        /// <returns></returns>
        public static bool DeleteByBookmarkInfoID(long bookmarkInfoID)
        {
            return DAL.DeleteByBookmarkInfoID(bookmarkInfoID) >= 0;
        }


    }
}
