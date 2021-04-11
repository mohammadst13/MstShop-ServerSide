﻿using Microsoft.AspNetCore.Mvc;
using MstShop_ServerSide.Core.Services.Interfaces;
using MstShop_ServerSide.Core.Utilities.Common;
using MstShop_ServerSide.Core.Utilities.Extensions.Identity;
using System.Threading.Tasks;

namespace MstShop_ServerSide.WebApi.Controllers
{
    public class OrderController : SiteBaseController
    {
        #region constructor

        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        #endregion

        #region add product to order

        [HttpGet("add-order")]
        public async Task<IActionResult> AddProductToOrder(long productId, int count)
        {
            if (User.Identity.IsAuthenticated)
            {
                var userId = User.GetUserId();
                await _orderService.AddProductToOrder(userId, productId, count);
                return JsonResponseStatus.Success(new { message = "محصول با موفقیت به سبد خرید شما اضافه شد" });
            }

            return JsonResponseStatus.Error(new { message = "برای افزودن محصول به سبد خرید ، ابتدا لاگین کنید" });
        }

        #endregion
    }
}
