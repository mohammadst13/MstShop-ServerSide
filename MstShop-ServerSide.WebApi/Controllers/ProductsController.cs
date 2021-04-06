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

            // await Task.Delay(4000);

            return JsonResponseStatus.Success(products);
        }

        #endregion

        #region get products categories

        [HttpGet("product-active-categories")]
        public async Task<IActionResult> GetProductsCategories()
        {
            return JsonResponseStatus.Success(await productService.GetAllActiveProductCategories());
        }

        #endregion

        #region get single product

        [HttpGet("single-product/{id}")]
        public async Task<IActionResult> GetProduct(long id)
        {
            var product = await productService.GetProductById(id);
            var productGalleries = await productService.GetProductActiveGalleries(id);

            if (product != null)
                return JsonResponseStatus.Success(new { product = product, galleries = productGalleries });

            return JsonResponseStatus.NotFound();
        }

        #endregion

        #region related products

        [HttpGet("related-products/{id}")]
        public async Task<IActionResult> GetRelatedProducts(long id)
        {
            var relatedProducts = await productService.GetRelatedProducts(id);

            return JsonResponseStatus.Success(relatedProducts);
        }

        #endregion

        #region product comments

        [HttpGet("product-comments/{id}")]
        public async Task<IActionResult> GetProductComments(long id)
        {
            var comments = await productService.GetActiveProductComments(id);

            return JsonResponseStatus.Success(comments);
        }

        #endregion
    }
}
