using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebBookmarkService.DAL;
using WebBookmarkBo.Model;

namespace WebBookmarkBo.Service
{
    
    public class GroupUserBo
    {
        private static GroupUserDAL DAL = new GroupUserDAL();

        /// <summary>
        /// 移除用户群组中的某个用户
        /// </summary>
        /// <param name="groupUserID"></param>
        /// <returns></returns>
        public static bool RemoverGroupUser(long groupUserID)
        {
            return DAL.DeleteByGroupUserID(groupUserID) >= 0;
        }

    }
}
