﻿//============================================================
//http://codelover.link author:李国宝
//============================================================

using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using MySql.Data.MySqlClient;
using WebBookmarkService.Model;

namespace WebBookmarkService.DAL
{
	public partial class GroupDAL
	{
        #region 根据传入Model，并返回Model
        /// <summary>
        /// 根据传入Model，并返回Model
        /// </summary>        
        public bool Add (Group group)
		{
				string sql ="INSERT INTO tblGroup (GroupName, GroupIntro, CreateUesrID, CreateTime)  VALUES (@GroupName, @GroupIntro, @CreateUesrID, @CreateTime)";
				MySqlParameter[] para = new MySqlParameter[]
					{
						new MySqlParameter("@GroupName", ToDBValue(group.GroupName)),
						new MySqlParameter("@GroupIntro", ToDBValue(group.GroupIntro)),
						new MySqlParameter("@CreateUesrID", ToDBValue(group.CreateUesrID)),
						new MySqlParameter("@CreateTime", ToDBValue(group.CreateTime)),
					};
					
				int AddId = (int)MyDBHelper.ExecuteScalar(sql, para);
				if(AddId==1)
				{
					return true;
				}else
				{
					return false;					
				}
		}
         #endregion
         

        #region  根据Id删除数据记录
        /// <summary>
        /// 根据Id删除数据记录
        /// </summary>
        public int DeleteByGroupID(long groupID)
		{
            string sql = "DELETE from tblGroup WHERE GroupID = @GroupID";

            MySqlParameter[] para = new MySqlParameter[]
			{
				new MySqlParameter("@GroupID", groupID)
			};
		
            return MyDBHelper.ExecuteNonQuery(sql, para);
		}
		 #endregion
		
				

		
        #region 根据传入Model更新数据并返回更新后的Model
        /// <summary>
        /// 根据传入Model更新数据并返回更新后的Model
        /// </summary>
        public int Update(Group group)
        {
            string sql =
                "UPDATE tblGroup " +
                "SET " +
			" GroupName = @GroupName" 
                +", GroupIntro = @GroupIntro" 
                +", CreateUesrID = @CreateUesrID" 
                +", CreateTime = @CreateTime" 
               
            +" WHERE GroupID = @GroupID";


			MySqlParameter[] para = new MySqlParameter[]
			{
				new MySqlParameter("@GroupID", group.GroupID)
					,new MySqlParameter("@GroupName", ToDBValue(group.GroupName))
					,new MySqlParameter("@GroupIntro", ToDBValue(group.GroupIntro))
					,new MySqlParameter("@CreateUesrID", ToDBValue(group.CreateUesrID))
					,new MySqlParameter("@CreateTime", ToDBValue(group.CreateTime))
			};

			return MyDBHelper.ExecuteNonQuery(sql, para);
        }
        #endregion
		
        #region 传入Id，获得Model实体
        /// <summary>
        /// 传入Id，获得Model实体
        /// </summary>
        public Group GetByGroupID(long groupID)
        {
            string sql = "SELECT * FROM tblGroup WHERE GroupID = @GroupID";
            using(MySqlDataReader reader = MyDBHelper.ExecuteDataReader(sql, new MySqlParameter("@GroupID", groupID)))
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
		public Group ToModel(MySqlDataReader dr)
		{
			Group group = new Group();

			group.GroupID = (long)ToModelValue(dr,"GroupID");
			group.GroupName = (string)ToModelValue(dr,"GroupName");
			group.GroupIntro = (string)ToModelValue(dr,"GroupIntro");
			group.CreateUesrID = (long)ToModelValue(dr,"CreateUesrID");
			group.CreateTime = (DateTime)ToModelValue(dr,"CreateTime");
			return group;
		}
		#endregion
        
        #region  获得总记录数
        ///<summary>
        /// 获得总记录数
        ///</summary>        
		public int GetTotalCount()
		{
			string sql = "SELECT count(*) FROM tblGroup";
			return (int)MyDBHelper.ExecuteScalar(sql);
		}
		#endregion
        
        #region 获得分页记录集IEnumerable<>
        ///<summary>
        /// 获得分页记录集IEnumerable<>
        ///</summary>              
		public IEnumerable<Group> GetPagedData(int minrownum,int maxrownum)
		{
			string sql = "SELECT * from(SELECT *,(row_number() over(order by GroupID))-1 rownum FROM tblGroup) t where rownum>=@minrownum and rownum<=@maxrownum";
			using(MySqlDataReader reader = MyDBHelper.ExecuteDataReader(sql,
				new MySqlParameter("@minrownum",minrownum),
				new MySqlParameter("@maxrownum",maxrownum)))
			{
				return ToModels(reader);					
			}
		}
		#endregion
        
        
        #region 根据字段名获取数据记录IEnumerable<>
        ///<summary>
        ///根据字段名获取数据记录IEnumerable<>
        ///</summary>              
		public IEnumerable<Group> GetBycolumnName(string columnName,string columnContent)
		{
			string sql = "SELECT * FROM tblGroup where "+columnName+"="+columnContent;
			using(MySqlDataReader reader = MyDBHelper.ExecuteDataReader(sql))
			{
				return ToModels(reader);			
			}
		}
		#endregion
        
        
        
        #region 获得总记录集IEnumerable<>
        ///<summary>
        /// 获得总记录集IEnumerable<>
        ///</summary> 
		public IEnumerable<Group> GetAll()
		{
			string sql = "SELECT * FROM tblGroup";
			using(MySqlDataReader reader = MyDBHelper.ExecuteDataReader(sql))
			{
				return ToModels(reader);			
			}
		}
        #endregion
		
        #region 把MySqlDataReader转换成IEnumerable<>
        ///<summary>
        /// 把MySqlDataReader转换成IEnumerable<>
        ///</summary> 
		protected IEnumerable<Group> ToModels(MySqlDataReader reader)
		{
			var list = new List<Group>();
			while(reader.Read())
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
			if(value==null)
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
		protected object ToModelValue(MySqlDataReader reader,string columnName)
		{
			if(reader.IsDBNull(reader.GetOrdinal(columnName)))
			{
				return null;
			}
			else
			{
				return reader[columnName];
			}
		}
        #endregion
	}
}