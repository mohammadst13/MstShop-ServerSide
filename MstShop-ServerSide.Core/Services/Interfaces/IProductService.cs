using MstShop_ServerSide.Core.DTOs.Producs;
using MstShop_ServerSide.DataLayer.Entities.Product;
using System;
using System.Threading.Tasks;

namespace MstShop_ServerSide.Core.Services.Interfaces
{
    public interface IProductService : IDisposable
    {
        #region product

        Task AddProduct(Product product);
        Task UpdateProduct(Product product);
        Task<FilterProdcutsDTO> FilterProducts(FilterProdcutsDTO filter);
        #endregion
    }
}
