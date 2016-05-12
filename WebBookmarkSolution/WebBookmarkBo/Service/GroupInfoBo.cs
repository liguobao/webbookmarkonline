﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebBookmarkBo.Model;
using WebBookmarkService.DAL;

namespace WebBookmarkBo.Service
{
    public class GroupInfoBo
    {

        private static GroupInfoDAL DAL = new GroupInfoDAL();

        private static GroupUserDAL groupUserDAL = new GroupUserDAL();

        /// <summary>
        /// 删除分组
        /// </summary>
        /// <param name="groupID"></param>
        /// <returns></returns>
        public static bool DeleteGroupInfo(long groupID)
        {
            if (groupID == 0)
                return true;
           return DAL.DeleteByGroupInfoID(groupID) >= 0 && groupUserDAL.DeleteByGroupID(groupID);
           
        }

        /// <summary>
        /// 获取用户所在的群组信息
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="auditStatus">通过/未通过</param>
        /// <returns></returns>
        public static List<BizGroupInfo> GetUserGroupList(long userID, ApplyStatus status)
        {
            var lstGroupUser = BizGroupUser.LoadGroupUser(userID);
            if (lstGroupUser == null)
                return new List<BizGroupInfo>();

            var groupUsers = lstGroupUser.Where(user => user.IsPass == (int)status);

            var lstGroupInfo = BizGroupInfo.LoadByGroupIDList(groupUsers.Select(model => model.GroupInfoID).ToList());
            return lstGroupInfo;
        }


        /// <summary>
        /// 获取用户所在的群组信息（带有未通过的记录）
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public static List<BizGroupInfo> GetAllUserGroup(long userID)
        {
            var lstGroupUser = BizGroupUser.LoadGroupUser(userID);
            if (lstGroupUser == null)
                return new List<BizGroupInfo>();
            var lstGroupInfo = BizGroupInfo.LoadByGroupIDList(lstGroupUser.Select(model => model.GroupInfoID).ToList());
            return lstGroupInfo;
        }

    }
}
