using MstShop_ServerSide.DataLayer.Entities.Account;
using MstShop_ServerSide.DataLayer.Entities.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace MstShop_ServerSide.DataLayer.Entities.Orders
{
    public class Order : BaseEntity
    {
        #region properties

        public long UserId { get; set; }

        public bool IsPay { get; set; }

        public DateTime? PaymentDate { get; set; }

        #endregion

        #region relations

        public User User { get; set; }

        public ICollection<OrderDetail> OrderDetails { get; set; }

        #endregion
    }
}
