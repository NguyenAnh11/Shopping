using Shopping.ViewModel.Catalog.Products.Dtos.Public;
using Shopping.ViewModel.Common.Dtos;
using Shopping.ViewModel.Products.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Shopping.Application.Catalog.Products
{
    public interface IPublicProductService
    {
        Task<PageResult<ProductViewModel>> GetAllByCategoryId(GetProductPagingRequest request);
    }
}
