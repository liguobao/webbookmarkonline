﻿//============================================================
//http://codelover.link author:李国宝
//============================================================

using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using MySql.Data.MySqlClient;
using WebBookmarkService.Model;
using System.Linq;

namespace WebBookmarkService.DAL
{
	public partial class UserInfoDAL
	{
        #region 自动生成方法

        #region 根据传入Model，并返回Model
        /// <summary>
        /// 根据传入Model，并返回Model
        /// </summary>        
        public bool Add(UserInfo userInfo)
        {
            string sql = "INSERT INTO tblUserInfo (UserLoginName, UserPassword, UserName, UserEmail, UserPhone, UserQQ, CreateTime, UserImagURL, UserInfoComment, ActivateAccountToken, AccountStatus)  VALUES (@UserLoginName, @UserPassword, @UserName, @UserEmail, @UserPhone, @UserQQ, @CreateTime, @UserImagURL, @UserInfoComment, @ActivateAccountToken, @AccountStatus)";
            MySqlParameter[] para = new MySqlParameter[]
					{
						new MySqlParameter("@UserLoginName", ToDBValue(userInfo.UserLoginName)),
						new MySqlParameter("@UserPassword", ToDBValue(userInfo.UserPassword)),
						new MySqlParameter("@UserName", ToDBValue(userInfo.UserName)),
						new MySqlParameter("@UserEmail", ToDBValue(userInfo.UserEmail)),
						new MySqlParameter("@UserPhone", ToDBValue(userInfo.UserPhone)),
						new MySqlParameter("@UserQQ", ToDBValue(userInfo.UserQQ)),
						new MySqlParameter("@CreateTime", ToDBValue(userInfo.CreateTime)),
						new MySqlParameter("@UserImagURL", ToDBValue(userInfo.UserImagURL)),
						new MySqlParameter("@UserInfoComment", ToDBValue(userInfo.UserInfoComment)),
						new MySqlParameter("@ActivateAccountToken", ToDBValue(userInfo.ActivateAccountToken)),
						new MySqlParameter("@AccountStatus", ToDBValue(userInfo.AccountStatus)),
					};

            int AddId = (int)MyDBHelper.ExecuteScalar(sql, para);
            if (AddId == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion


        #region  根据Id删除数据记录
        /// <summary>
        /// 根据Id删除数据记录
        /// </summary>
        public int DeleteByUserInfoID(long userInfoID)
        {
            string sql = "DELETE from tblUserInfo WHERE UserInfoID = @UserInfoID";

            MySqlParameter[] para = new MySqlParameter[]
			{
				new MySqlParameter("@UserInfoID", userInfoID)
			};

            return MyDBHelper.ExecuteNonQuery(sql, para);
        }
        #endregion




        #region 根据传入Model更新数据并返回更新后的Model
        /// <summary>
        /// 根据传入Model更新数据并返回更新后的Model
        /// </summary>
        public int Update(UserInfo userInfo)
        {
            string sql =
                "UPDATE tblUserInfo " +
                "SET " +
            " UserLoginName = @UserLoginName"
                + ", UserPassword = @UserPassword"
                + ", UserName = @UserName"
                + ", UserEmail = @UserEmail"
                + ", UserPhone = @UserPhone"
                + ", UserQQ = @UserQQ"
                + ", CreateTime = @CreateTime"
                + ", UserImagURL = @UserImagURL"
                + ", UserInfoComment = @UserInfoComment"
                + ", ActivateAccountToken = @ActivateAccountToken"
                + ", AccountStatus = @AccountStatus"

            + " WHERE UserInfoID = @UserInfoID";


            MySqlParameter[] para = new MySqlParameter[]
			{
				new MySqlParameter("@UserInfoID", userInfo.UserInfoID)
					,new MySqlParameter("@UserLoginName", ToDBValue(userInfo.UserLoginName))
					,new MySqlParameter("@UserPassword", ToDBValue(userInfo.UserPassword))
					,new MySqlParameter("@UserName", ToDBValue(userInfo.UserName))
					,new MySqlParameter("@UserEmail", ToDBValue(userInfo.UserEmail))
					,new MySqlParameter("@UserPhone", ToDBValue(userInfo.UserPhone))
					,new MySqlParameter("@UserQQ", ToDBValue(userInfo.UserQQ))
					,new MySqlParameter("@CreateTime", ToDBValue(userInfo.CreateTime))
					,new MySqlParameter("@UserImagURL", ToDBValue(userInfo.UserImagURL))
					,new MySqlParameter("@UserInfoComment", ToDBValue(userInfo.UserInfoComment))
					,new MySqlParameter("@ActivateAccountToken", ToDBValue(userInfo.ActivateAccountToken))
					,new MySqlParameter("@AccountStatus", ToDBValue(userInfo.AccountStatus))
			};

            return MyDBHelper.ExecuteNonQuery(sql, para);
        }
        #endregion

        #region 传入Id，获得Model实体
        /// <summary>
        /// 传入Id，获得Model实体
        /// </summary>
        public UserInfo GetByUserInfoID(long userInfoID)
        {
            string sql = "SELECT * FROM tblUserInfo WHERE UserInfoID = @UserInfoID";
            using (MySqlDataReader reader = MyDBHelper.ExecuteDataReader(sql, new MySqlParameter("@UserInfoID", userInfoID)))
            {
                if (reader.Read())
                {
                    return ToModel(reader);
                }
                else
                {
                    return null;
                }
            }
        }
        #endregion

        #region 把DataRow转换成Model
        /// <summary>
        /// 把DataRow转换成Model
        /// </summary>
        public UserInfo ToModel(MySqlDataReader dr)
        {
            UserInfo userInfo = new UserInfo();

            userInfo.UserInfoID = (long)ToModelValue(dr, "UserInfoID");
            userInfo.UserLoginName = (string)ToModelValue(dr, "UserLoginName");
            userInfo.UserPassword = (string)ToModelValue(dr, "UserPassword");
            userInfo.UserName = (string)ToModelValue(dr, "UserName");
            userInfo.UserEmail = (string)ToModelValue(dr, "UserEmail");
            userInfo.UserPhone = (string)ToModelValue(dr, "UserPhone");
            userInfo.UserQQ = (string)ToModelValue(dr, "UserQQ");
            userInfo.CreateTime = (DateTime)ToModelValue(dr, "CreateTime");
            userInfo.UserImagURL = (string)ToModelValue(dr, "UserImagURL");
            userInfo.UserInfoComment = (string)ToModelValue(dr, "UserInfoComment");
            userInfo.ActivateAccountToken = (string)ToModelValue(dr, "ActivateAccountToken");
            userInfo.AccountStatus = (int)ToModelValue(dr, "AccountStatus");
            return userInfo;
        }
        #endregion

        #region  获得总记录数
        ///<summary>
        /// 获得总记录数
        ///</summary>        
        public int GetTotalCount()
        {
            string sql = "SELECT count(*) FROM tblUserInfo";
            return (int)MyDBHelper.ExecuteScalar(sql);
        }
        #endregion

        #region 获得分页记录集IEnumerable<>
        ///<summary>
        /// 获得分页记录集IEnumerable<>
        ///</summary>              
        public IEnumerable<UserInfo> GetPagedData(int minrownum, int maxrownum)
        {
            string sql = "SELECT * from(SELECT *,(row_number() over(order by UserInfoID))-1 rownum FROM tblUserInfo) t where rownum>=@minrownum and rownum<=@maxrownum";
            using (MySqlDataReader reader = MyDBHelper.ExecuteDataReader(sql,
                new MySqlParameter("@minrownum", minrownum),
                new MySqlParameter("@maxrownum", maxrownum)))
            {
                return ToModels(reader);
            }
        }
        #endregion


        #region 根据字段名获取数据记录IEnumerable<>
        ///<summary>
        ///根据字段名获取数据记录IEnumerable<>
        ///</summary>              
        public IEnumerable<UserInfo> GetBycolumnName(string columnName, string columnContent)
        {
            string sql = "SELECT * FROM tblUserInfo where " + columnName + "=" + columnContent;
            using (MySqlDataReader reader = MyDBHelper.ExecuteDataReader(sql))
            {
                return ToModels(reader);
            }
        }
        #endregion



        #region 获得总记录集IEnumerable<>
        ///<summary>
        /// 获得总记录集IEnumerable<>
        ///</summary> 
        public IEnumerable<UserInfo> GetAll()
        {
            string sql = "SELECT * FROM tblUserInfo";
            using (MySqlDataReader reader = MyDBHelper.ExecuteDataReader(sql))
            {
                return ToModels(reader);
            }
        }
        #endregion

        #region 把MySqlDataReader转换成IEnumerable<>
        ///<summary>
        /// 把MySqlDataReader转换成IEnumerable<>
        ///</summary> 
        protected IEnumerable<UserInfo> ToModels(MySqlDataReader reader)
        {
            var list = new List<UserInfo>();
            while (reader.Read())
            {
                list.Add(ToModel(reader));
            }
            return list;
        }
        #endregion

        #region 判断数据是否为空
        ///<summary>
        /// 判断数据是否为空
        ///</summary>
        protected object ToDBValue(object value)
        {
            if (value == null)
            {
                return DBNull.Value;
            }
            else
            {
                return value;
            }
        }
        #endregion

        #region 判断数据表中是否包含该字段
        ///<summary>
        /// 判断数据表中是否包含该字段
        ///</summary>
        protected object ToModelValue(MySqlDataReader reader, string columnName)
        {
            if (reader.IsDBNull(reader.GetOrdinal(columnName)))
            {
                return null;
            }
            else
            {
                return reader[columnName];
            }
        }
        #endregion

        #endregion
	
	

        public UserInfo GetByUserEmailOrUserLoginName(string emailOrLoginName)
        {
            string sql = "SELECT * FROM tblUserInfo WHERE UserLoginName = @EmailOrLoginName or UserEmail = @EmailOrLoginName";
            using (MySqlDataReader reader = MyDBHelper.ExecuteDataReader(sql,
                new MySqlParameter("@EmailOrLoginName", ToDBValue(emailOrLoginName))))
            {
                if (reader.Read())
                {
                    return ToModel(reader);
                }
                else
                {
                    return null;
                }
            }
        }


        public UserInfo GetByActivateAccountToken(string token)
        {
            string sql = "SELECT * FROM tblUserInfo WHERE ActivateAccountToken = @ActivateAccountToken";
            using (MySqlDataReader reader = MyDBHelper.ExecuteDataReader(sql,
                new MySqlParameter("@ActivateAccountToken", ToDBValue(token))))
            {
                if (reader.Read())
                {
                    return ToModel(reader);
                }
                else
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// 通过用户名/用户邮箱搜索用户
        /// </summary>
        /// <param name="emailOrUserName"></param>
        /// <returns></returns>
        public IEnumerable<UserInfo> SearchByNameOrEmail(string emailOrUserName)
        {
            string sql = "SELECT * FROM tblUserInfo WHERE UserName like @EmailOrUserName or UserEmail like @EmailOrUserName";
            using (MySqlDataReader reader = MyDBHelper.ExecuteDataReader(sql,
                new MySqlParameter("@EmailOrUserName", ToDBValue(emailOrUserName+"%"))))
            {
                return ToModels(reader);
            }
        }

        /// <summary>
        /// 获取一批的用户信息
        /// </summary>
        /// <param name="lstUID"></param>
        /// <returns></returns>
        public IEnumerable<UserInfo> GetByUserIDList(List<long> lstUID)
        {
            string sql = string.Format("SELECT * FROM tblUserInfo WHERE UserInfoID IN ({0})",string.Join(",",lstUID));
            using (MySqlDataReader reader = MyDBHelper.ExecuteDataReader(sql))
            {
                return ToModels(reader);
            }
        }


        /// <summary>
        /// 获取和当前用户拥有相同链接的用户信息
        /// </summary>
        /// <param name="userID">当前用户</param>
        /// <returns></returns>
        public IEnumerable<UserInfo> GetSameHrefUserList(long userID)
        {
            string sql = @"SELECT 
                            *
                        FROM
                            webbookmark.tblUserInfo userInfo
                                JOIN
                            (SELECT DISTINCT
                                (info.UserInfoID)
                            FROM
                                webbookmark.tblBookmarkInfo info
                            JOIN (SELECT 
                                *
                            FROM
                                webbookmark.tblBookmarkInfo AS t1
                            JOIN (SELECT 
                                ROUND(RAND() * ((SELECT 
                                            MAX(BookmarkInfoID)
                                        FROM
                                            webbookmark.tblBookmarkInfo) - (SELECT 
                                            MIN(BookmarkInfoID)
                                        FROM
                                            webbookmark.tblBookmarkInfo)) + (SELECT 
                                            MIN(BookmarkInfoID)
                                        FROM
                                            webbookmark.tblBookmarkInfo)) AS id
                            ) AS t2
                            WHERE
                                t1.BookmarkInfoID >= t2.id
                                    AND t1.userinfoid = @UserInfoID
                            LIMIT 0 , @Length) AS userBM ON userBM.href = info.href
                                AND info.userinfoid != userBM.userinfoid) AS userids ON userids.userinfoid = userInfo.userinfoid;";
            Random rd = new Random(DateTime.Now.Millisecond);
            int length = rd.Next(0, 100);

            using (MySqlDataReader reader = MyDBHelper.ExecuteDataReader(sql, new MySqlParameter("@UserInfoID", userID), new MySqlParameter("@Length", length)))
            {
                return ToModels(reader);
            }
        }

    }
}