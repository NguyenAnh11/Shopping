using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Shopping.Data.EF;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Shopping.ViewModel.Catalog.Products;
using Shopping.ViewModel.Common;

namespace Shopping.Application.Catalog.Products
{
    public class PublicProductService : IPublicProductService
    {
        private readonly ShoppingDBContext _context;
        public PublicProductService(ShoppingDBContext context)
        {
            _context = context;
        }
        public async Task<PageResult<ProductViewModel>> GetAllByCategoryId(string languageId,GetPublicProductPagingRequest request)
        {
            var query = from p in _context.Products
                        join pt in _context.ProductTranslations on p.Id equals pt.ProductId
                        join pic in _context.ProductInCategories on p.Id equals pic.ProductId
                        select new { p, pt, pic };

            if(request.CategoryId.HasValue && request.CategoryId.Value > 0)
            {
                query = query.Where(x => x.pic.CategoryId == request.CategoryId);
            }
            if (!string.IsNullOrEmpty(languageId))
            {
                query = query.Where(x => x.pt.LanguageId == languageId);
            }

            int totalRows = await query.CountAsync();
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
            var pageResult = new PageResult<ProductViewModel>()
            {
                TotalRecord = totalRows,
                Items = data
            };
            return pageResult;
        }
    }
}
