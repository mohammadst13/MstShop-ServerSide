using System;
using System.Linq;
using System.Threading.Tasks;
using MstShop_ServerSide.Core.DTOs.Paging;
using MstShop_ServerSide.Core.DTOs.Products;
using MstShop_ServerSide.Core.Services.Interfaces;
using MstShop_ServerSide.Core.Utilities.Extensions.Paging;
using MstShop_ServerSide.DataLayer.Entities.Product;
using MstShop_ServerSide.DataLayer.Repository;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace AngularEshop.Core.Services.Implementations
{
    public class ProductService : IProductService
    {
        #region constructor

        private IGenericRepository<Product> productRepository;
        private IGenericRepository<ProductCategory> productCategoryRepository;
        private IGenericRepository<ProductGallery> productGalleryRepository;
        private IGenericRepository<ProductSelectedCategory> productSelectedCategoryRepository;
        private IGenericRepository<ProductVisit> productVisitRepository;
        private IGenericRepository<ProductComment> productCommentRepository;

        public ProductService(IGenericRepository<Product> productRepository,
            IGenericRepository<ProductCategory> productCategoryRepository,
            IGenericRepository<ProductGallery> productGalleryRepository,
            IGenericRepository<ProductSelectedCategory> productSelectedCategoryRepository,
            IGenericRepository<ProductVisit> productVisitRepository,
            IGenericRepository<ProductComment> productCommentRepository)
        {
            this.productRepository = productRepository;
            this.productCategoryRepository = productCategoryRepository;
            this.productGalleryRepository = productGalleryRepository;
            this.productSelectedCategoryRepository = productSelectedCategoryRepository;
            this.productVisitRepository = productVisitRepository;
            this.productCommentRepository = productCommentRepository;
        }

        #endregion

        #region product

        public async Task AddProduct(Product product)
        {
            await productRepository.AddEntity(product);
            await productRepository.SaveChanges();
        }

        public async Task UpdateProduct(Product product)
        {
            productRepository.UpdateEntity(product);
            await productRepository.SaveChanges();
        }

        public async Task<FilterProductsDTO> FilterProducts(FilterProductsDTO filter)
        {
            var productsQuery = productRepository.GetEntitiesQuery().AsQueryable();

            switch (filter.OrderBy)
            {
                case ProductOrderBy.PriceAsc:
                    productsQuery = productsQuery.OrderBy(s => s.Price);
                    break;
                case ProductOrderBy.PriceDec:
                    productsQuery = productsQuery.OrderByDescending(s => s.Price);
                    break;
            }

            if (!string.IsNullOrEmpty(filter.Title))
                productsQuery = productsQuery.Where(s => s.ProductName.Contains(filter.Title));

            if (filter.StartPrice != 0)
                productsQuery = productsQuery.Where(s => s.Price >= filter.StartPrice);

            if (filter.EndPrice != 0)
                productsQuery = productsQuery.Where(s => s.Price <= filter.EndPrice);

            productsQuery = productsQuery.Where(s => s.Price >= filter.StartPrice);

            if (filter.Categories != null && filter.Categories.Any())
                productsQuery = productsQuery.SelectMany(s =>
                        s.ProductSelectedCategories.Where(f => filter.Categories.Contains(f.ProductCategoryId)).Select(t => t.Product));

            if (filter.EndPrice != 0)
                productsQuery = productsQuery.Where(s => s.Price <= filter.EndPrice);

            var count = (int)Math.Ceiling(productsQuery.Count() / (double)filter.TakeEntity);

            var pager = Pager.Build(count, filter.PageId, filter.TakeEntity);

            var products = await productsQuery.Paging(pager).ToListAsync();

            return filter.SetProducts(products).SetPaging(pager);
        }

        public async Task<Product> GetProductById(long productId)
        {
            return await productRepository.GetEntityById(productId);
        }

        public async Task<List<Product>> GetRelatedProducts(long productId)
        {
            var product = await productRepository.GetEntityById(productId);

            if (product == null) return null;

            var productCategoriesList = await productSelectedCategoryRepository.GetEntitiesQuery()
                .Where(s => s.ProductId == productId).Select(f => f.ProductCategoryId).ToListAsync();

            var relatedProducts = await productRepository
                .GetEntitiesQuery()
                .SelectMany(s => s.ProductSelectedCategories.Where(f => productCategoriesList.Contains(f.ProductCategoryId)).Select(t => t.Product))
                .Where(s => s.Id != productId)
                .OrderByDescending(s => s.CreateDate)
                .Take(4).ToListAsync();

            return relatedProducts;
        }

        #endregion

        #region product categories

        public async Task<List<ProductCategory>> GetAllActiveProductCategories()
        {
            return await productCategoryRepository.GetEntitiesQuery().Where(s => !s.IsDelete).ToListAsync();
        }

        #endregion

        #region products gallery

        public async Task<List<ProductGallery>> GetProductActiveGalleries(long productId)
        {
            return await productGalleryRepository
                .GetEntitiesQuery()
                .Where(s => s.ProductId == productId && !s.IsDelete)
                .Select(s => new ProductGallery
                {
                    ProductId = s.ProductId,
                    Id = s.Id,
                    ImageName = s.ImageName,
                    CreateDate = s.CreateDate
                }).ToListAsync();
        }

        #endregion

        #region product comments

        public async Task AddCommentToProduct(ProductComment comment)
        {
            await productCommentRepository.AddEntity(comment);
            await productCommentRepository.SaveChanges();
        }

        public async Task<List<ProductCommentDTO>> GetActiveProductComments(long productId)
        {
            return await productCommentRepository
                .GetEntitiesQuery()
                .Include(s => s.User)
                .Where(c => c.ProductId == productId && !c.IsDelete)
                .OrderByDescending(s => s.CreateDate)
                .Select(s => new ProductCommentDTO
                {
                    Id = s.Id,
                    Text = s.Text,
                    UserId = s.UserId,
                    UserFullName = s.User.FirstName + " " + s.User.LastName,
                    CreateDate = s.CreateDate.ToString("yyyy/MM/dd HH:mm")
                }).ToListAsync();
        }

        #endregion

        #region dispose

        public void Dispose()
        {
            productRepository?.Dispose();
            productCategoryRepository?.Dispose();
            productGalleryRepository?.Dispose();
            productSelectedCategoryRepository?.Dispose();
            productVisitRepository?.Dispose();
            productCommentRepository?.Dispose();
        }

        #endregion
    }
}