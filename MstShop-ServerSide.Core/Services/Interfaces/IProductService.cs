﻿using MstShop_ServerSide.Core.DTOs.Products;
using MstShop_ServerSide.DataLayer.Entities.Product;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MstShop_ServerSide.Core.Services.Interfaces
{
    public interface IProductService : IDisposable
    {
        #region product

        Task AddProduct(Product product);
        Task UpdateProduct(Product product);
        Task<FilterProductsDTO> FilterProducts(FilterProductsDTO filter);

        #endregion

        #region product categories

        Task<List<ProductCategory>> GetAllActiveProductCategories();

        #endregion
    }
}
