using Microsoft.AspNetCore.Mvc;
using MstShop_ServerSide.Core.DTOs.Products;
using MstShop_ServerSide.Core.Services.Interfaces;
using MstShop_ServerSide.Core.Utilities.Common;
using System.Threading.Tasks;

namespace MstShop_ServerSide.WebApi.Controllers
{
    public class ProductsController : SiteBaseController
    {
        #region constructor

        private IProductService productService;

        public ProductsController(IProductService productService)
        {
            this.productService = productService;
        }

        #endregion

        #region products

        [HttpGet("filter-products")]
        public async Task<IActionResult> GetProducts([FromQuery] FilterProductsDTO filter)
        {
            var products = await productService.FilterProducts(filter);

            return JsonResponseStatus.Success(products);
        }

        #endregion
    }
}
