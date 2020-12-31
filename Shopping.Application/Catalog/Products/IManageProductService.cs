using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Shopping.Application.Catalog.Products.Dtos;
using Shopping.Application.Catalog.Products.Dtos.Manage;
using Shopping.Application.Dtos;

namespace Shopping.Application.Catalog.Products
{
    public interface IManageProductService
    {
        Task<int> Create(ProductCreateRequest request);
        Task<int> Update(ProductEditRequest request);
        Task<int> Delete(int productId);
        Task<bool> UpdatePrice(int productId, decimal newPrice);
        Task<bool> UpdateStock(int productId, int newQuantity);
        Task AddViewCount(int productId);
        Task<PageResult<ProductViewModel>> GetAllPaging(GetProductPagingRequest request);
    }
}
