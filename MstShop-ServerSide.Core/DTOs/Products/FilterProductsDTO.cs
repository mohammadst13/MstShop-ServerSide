using MstShop_ServerSide.Core.DTOs.Paging;
using MstShop_ServerSide.DataLayer.Entities.Product;
using System.Collections.Generic;

namespace MstShop_ServerSide.Core.DTOs.Products
{
    public class FilterProductsDTO : BasePaging
    {
        public string Title { get; set; }

        public int StartPrice { get; set; }

        public int EndPrice { get; set; }

        public List<Product> Products { get; set; }

        public List<long> Categories { get; set; }

        public ProductOrderBy? OrderBy { get; set; }

        public FilterProductsDTO SetPaging(BasePaging paging)
        {
            this.PageId = paging.PageId;
            this.PageCount = paging.PageCount;
            this.StartPage = paging.StartPage;
            this.EndPage = paging.EndPage;
            this.TakeEntity = paging.TakeEntity;
            this.SkipEntity = paging.SkipEntity;
            this.ActivePage = paging.ActivePage;
            return this;
        }

        public FilterProductsDTO SetProducts(List<Product> products)
        {
            this.Products = products;
            return this;
        }
    }

    public enum ProductOrderBy
    {
        PriceAsc,
        PriceDec
    }
}
