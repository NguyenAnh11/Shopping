using Microsoft.EntityFrameworkCore;
using Shopping.Data.EF;
using Shopping.Data.Entities;
using Shopping.Utilities.Exceptions;
using Shopping.ViewModel.Catalog.Products;
using Shopping.ViewModel.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shopping.Application.Catalog.Common;
using Microsoft.AspNetCore.Http;
using System.Net.Http.Headers;
using System.IO;

namespace Shopping.Application.Catalog.Products
{
    public class ManageProductService : IManageProductService
    {
        private ShoppingDBContext _context;
        private readonly IStorageService _storageService;
        public ManageProductService(ShoppingDBContext context,IStorageService storageService)
        {
            _context = context;
            _storageService = storageService;
        }

        public async Task AddViewCount(int productId)
        {
            var product = await _context.Products.FindAsync(productId);
            if (product == null) throw new ShoppingException($"Can not find product: ${productId}");
            product.ViewCount += 1;
            await _context.SaveChangesAsync();
        }

        public async Task<int> Create(ProductCreateRequest request)
        {
            var product = new Product()
            {
                Price = request.Price,
                OriginalPrice = request.OriginalPrice,
                Stock = request.Stock,
                ViewCount = 0,
                ProductTranslations = new List<ProductTranslation>()
                {
                    new ProductTranslation()
                    {
                        Name = request.Name,
                        Description = request.Description,
                        Details = request.Details,
                        SeoDescription = request.SeoDescription,
                        SeoTitle = request.SeoTitle,
                        SeoAlias = request.SeoAlias,
                        LanguageId = request.LanguageId,
                    }
                }
            };
            if(request.ThumbnialImage != null)
            {
                product.ProductImages = new List<ProductImage>()
                {
                    new ProductImage()
                    {
                        Caption = "Thumbnial Image",
                        DateCreated = DateTime.Now,
                        FileSize = request.ThumbnialImage.Length,
                        IsDefault = true,
                        SortOrder = 1,
                        ImagePath = await SaveFile(request.ThumbnialImage)
                    }
                };
            }

            _context.Products.Add(product);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> Delete(int productId)
        {
            var product = await _context.Products.FindAsync(productId);
            if (product == null) throw new ShoppingException($"Can not find product: {productId}");

            var productImages = _context.ProductImages.Where(x => x.ProductId == productId);
            foreach(var productImage in productImages)
            {
                string fileName = productImage.ImagePath;

                await _storageService.DeleteFileAsync(fileName);
            }
            _context.Products.Remove(product);
            return await _context.SaveChangesAsync();
        }

        public async Task<PageResult<ProductViewModel>> GetAllPaging(GetManageProductPagingRequest request)
        {
            string key = request.keyword.ToLower();
            //1. Select join
            var query = from p in _context.Products
                        join pt in _context.ProductTranslations on p.Id equals pt.ProductId
                        join pic in _context.ProductInCategories on p.Id equals pic.ProductId
                        join c in _context.Categories on pic.CategoryId equals c.Id
                        select new { p, pt, pic };
            //2. Filter
            if (!string.IsNullOrEmpty(request.keyword))
            {
                query = query.Where(x => x.pt.Name.ToLower().Contains(key));
            }
            if (request.CategoryId.Count > 0)
            {
                query = query.Where(x => request.CategoryId.Contains(x.pic.CategoryId));
            }
            //3.Pagging
            int totalRow = await query.CountAsync();
            var data = await query.Skip((request.pageIndex - 1) * request.pageSize).Take(request.pageSize)
                .Select(x => new ProductViewModel()
                {
                    Id = x.p.Id,
                    Price = x.p.Price,
                    OriginalPrice = x.p.Price,
                    Stock = x.p.Stock,
                    ViewCount = x.p.ViewCount,
                    DateCreated = x.p.DateCreated,
                    SeoAlias = x.pt.SeoAlias,
                    SeoDescription = x.pt.SeoDescription,
                    SeoTitle = x.pt.SeoTitle,
                    Description = x.pt.Description,
                    Details = x.pt.Details,
                    Name = x.pt.Name,
                    LanguageId = x.pt.LanguageId
                }).ToListAsync();
            //4. Select and projection
            var pageResult = new PageResult<ProductViewModel>()
            {
                TotalRecord = totalRow,
                Items = data
            };
            return pageResult;
        }

        public async Task<int> Update(ProductEditRequest request)
        {
            var product = await _context.Products.FindAsync(request.Id);
            var productTranslation = await _context.ProductTranslations.FirstOrDefaultAsync(x => x.ProductId == request.Id
                                        && x.LanguageId == request.LanguageId);
            if (product == null || productTranslation == null) throw new ShoppingException($"Can not find product: {request.Id}");
            productTranslation.Name = request.Name;
            productTranslation.SeoAlias = request.SeoAlias;
            productTranslation.SeoDescription = request.SeoDescription;
            productTranslation.SeoTitle = request.SeoTitle;
            productTranslation.Description = request.Description;
            productTranslation.Details = request.Details;

            //update image
            if(request.ThumbnailImage != null)
            {
                var productImage = await _context.ProductImages.FirstOrDefaultAsync(x => x.IsDefault == true && x.ProductId == request.Id);
                productImage.FileSize = request.ThumbnailImage.Length;
                productImage.ImagePath = await this.SaveFile(request.ThumbnailImage);
                _context.ProductImages.Update(productImage);
            }
           
            return await _context.SaveChangesAsync();

        }

        public async Task<bool> UpdatePrice(int productId, decimal newPrice)
        {
            var product = await _context.Products.FirstOrDefaultAsync(x => x.Id == productId);
            if (product == null) throw new ShoppingException($"Can not find product: {productId}");
            product.Price = newPrice;
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateStock(int productId, int newQuantity)
        {
            var product = await _context.Products.FirstOrDefaultAsync(x => x.Id == productId);
            if (product == null) throw new ShoppingException($"Can not find product: {productId}");
            product.Stock += newQuantity;
            return await _context.SaveChangesAsync() > 0;
        }
        private async Task<string> SaveFile(IFormFile file)
        {
            string originalFileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition)
                                            .FileName.Trim();
            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(originalFileName)}";
            await _storageService.SaveFileAsync(file.OpenReadStream(), fileName);
            return fileName;
        }

        public async Task<int> AddImages(int productId, List<IFormFile> files)
        {
            var product = await _context.Products.FirstAsync(x => x.Id == productId);
            if (product == null) throw new ShoppingException($"Can not found product {productId}");
            //get max order image with product
            int max_order_product_iamge = _context.ProductImages.Where(x => x.ProductId == productId).Max(x => x.SortOrder);
            int index = 1;
            foreach(var file in files)
            {
                var productImage = new ProductImage()
                {
                    Caption = "Thumbinal Image",
                    DateCreated = DateTime.Now,
                    IsDefault = false,
                    FileSize = file.Length,
                    SortOrder = max_order_product_iamge + index,
                    ImagePath = await this.SaveFile(file)
                };
                index++;
                product.ProductImages.Add(productImage);
            }
            return await _context.SaveChangesAsync();
        }

        public async Task<int> RemoveImage(int imageId)
        {
            var productImage = await _context.ProductImages.FirstOrDefaultAsync(x => x.Id == imageId);
            if (productImage == null) throw new ShoppingException($"Cannot find image: {imageId}");

            await _storageService.DeleteFileAsync(productImage.ImagePath);

            _context.ProductImages.Remove(productImage);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> UpdateImage(int imageId, string caption, bool isDefault,int? sortOrder)
        {
            var productImage = await _context.ProductImages.FirstOrDefaultAsync(x => x.Id == imageId);
            if (productImage == null) throw new ShoppingException($"Can not find product image: {imageId}");
            productImage.IsDefault = isDefault;
            productImage.Caption = caption;
            if(sortOrder.HasValue && sortOrder != productImage.SortOrder)
            {
                var productImageWithSortOrder = await _context.ProductImages.FirstOrDefaultAsync(x => x.SortOrder == sortOrder.Value);
                productImageWithSortOrder.SortOrder = productImage.SortOrder;
                productImage.SortOrder = sortOrder.Value;
                _context.ProductImages.Update(productImageWithSortOrder);
            }
            _context.ProductImages.Update(productImage);
            return await _context.SaveChangesAsync();
        }

        public async Task<PageResult<ProductImageViewModel>> GetListImage(GetProductImagePagingRequest request)
        {
            var query = from im in _context.ProductImages
                       where im.ProductId == request.productId
                       select im;
            if (query == null) throw new ShoppingException($"Can not find image with product id: {request.productId}");
            var data = await query.Skip((request.pageIndex - 1) * request.pageSize).Take(request.pageSize)
                                 .Select(x => new ProductImageViewModel()
                                 {
                                     Id = x.Id,
                                     FilePath = x.ImagePath,
                                     isDefault = x.IsDefault,
                                     FileSize = x.FileSize,
                                 }).ToListAsync();                  
            int totalRecord = query.Count();
            var pageResult = new PageResult<ProductImageViewModel>()
            {
                TotalRecord = totalRecord,
                Items = data
            };
            return pageResult;
        }
    }
}
