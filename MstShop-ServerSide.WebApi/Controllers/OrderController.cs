using Microsoft.AspNetCore.Mvc;
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
                return JsonResponseStatus.Success(new
                {
                    message = "محصول با موفقیت به سبد خرید شما اضافه شد",
                    details = await _orderService.GetUserBasketDetails(userId)
                });
            }

            return JsonResponseStatus.Error(new { message = "برای افزودن محصول به سبد خرید ، ابتدا لاگین کنید" });
        }

        #endregion

        #region user basket details

        [HttpGet("get-order-details")]
        public async Task<IActionResult> GetUserBasketDetails()
        {
            if (User.Identity.IsAuthenticated)
            {
                var details = await _orderService.GetUserBasketDetails(User.GetUserId());
                return JsonResponseStatus.Success(details);
            }

            return JsonResponseStatus.Error();
        }

        #endregion
    }
}
