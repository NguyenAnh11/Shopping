using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Shopping.Application.Catalog.Products.Dtos;
using Shopping.Application.Catalog.Products.Dtos.Public;
using Shopping.Application.Dtos;

namespace Shopping.Application.Catalog.Products
{
    public interface IPublicProductService
    {
        Task<PageResult<ProductViewModel>> GetAllByCategoryId(GetProductPagingRequest request);
    }
}
