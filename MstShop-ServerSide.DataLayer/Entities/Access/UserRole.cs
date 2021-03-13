using MstShop_ServerSide.DataLayer.Entities.Account;
using MstShop_ServerSide.DataLayer.Entities.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace MstShop_ServerSide.DataLayer.Entities.Access
{
    public class UserRole : BaseEntity
    {
        #region properties

        public long UserId { get; set; }

        public long RoleId { get; set; }

        #endregion

        #region Relations

        public User User { get; set; }

        public Role Role { get; set; }

        #endregion
    }
}
