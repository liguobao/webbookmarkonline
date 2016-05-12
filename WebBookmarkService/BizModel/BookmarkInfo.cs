﻿//============================================================
//http://codelover.link author:李国宝
//============================================================

using System;
using System.Collections.Generic;
using System.Text;
using WebBookmarkService.Model;

namespace WebBookmarkService.BizModel
{	
	[Serializable()]
    
    /// <summary>
    /// 
    /// </summary>
	public class BizBookmarkInfo
	{
        /// <summary>
        /// 主键，自增
        /// </summary>
		public long BookmarkInfoID{get;set;}
            
        /// <summary>
        /// 网页收藏夹ID
        /// </summary>
		public long UserWebFolderID{get;set;}
            
        /// <summary>
        /// 用户ID
        /// </summary>
		public long UserInfoID{get;set;}
            
        /// <summary>
        /// 网址
        /// </summary>
		public string Href{get;set;}
            
        /// <summary>
        /// 网页HTML
        /// </summary>
		public string HTML{get;set;}
            
        /// <summary>
        /// 域名
        /// </summary>
		public string Host{get;set;}
            
        /// <summary>
        /// 创建时间
        /// </summary>
		public DateTime CreateTime{get;set;}
            
        /// <summary>
        /// 导入XML信息
        /// </summary>
		public string IElementJSON{get;set;}
            
        /// <summary>
        /// 
        /// </summary>
		public string BookmarkName{get;set;}
            
        /// <summary>
        /// 等级,0:对外公开，1：对关注者公开，2对群组公开，3：仅自己可见
        /// </summary>
		public int Grate{get;set;}
            
        /// <summary>
        /// 类似于主键
        /// </summary>
		public int HashCode{get;set;}
            
        /// <summary>
        /// 是否可在iframe中展示
        /// </summary>
		public int IsShowWithiframe{get;set;}
            
        
        /// <summary>
        /// Biz Convert To DB Model
        /// </summary>
        public BookmarkInfo ToModel()
        {
            return new BookmarkInfo()
            {
                BookmarkInfoID =  BookmarkInfoID,
                UserWebFolderID =  UserWebFolderID,
                UserInfoID =  UserInfoID,
                Href =  Href,
                HTML =  HTML,
                Host =  Host,
                CreateTime =  CreateTime,
                IElementJSON =  IElementJSON,
                BookmarkName =  BookmarkName,
                Grate =  Grate,
                HashCode =  HashCode,
                IsShowWithiframe =  IsShowWithiframe,
            };
        }
        
        
        public BizBookmarkInfo (BookmarkInfo dataInfo)
        {
             BookmarkInfoID =  dataInfo.BookmarkInfoID;
             UserWebFolderID =  dataInfo.UserWebFolderID;
             UserInfoID =  dataInfo.UserInfoID;
             Href =  dataInfo.Href;
             HTML =  dataInfo.HTML;
             Host =  dataInfo.Host;
             CreateTime =  dataInfo.CreateTime;
             IElementJSON =  dataInfo.IElementJSON;
             BookmarkName =  dataInfo.BookmarkName;
             Grate =  dataInfo.Grate;
             HashCode =  dataInfo.HashCode;
             IsShowWithiframe =  dataInfo.IsShowWithiframe;
        }
        
        public  BizBookmarkInfo ()
        {
        
        } 
        
	}
}