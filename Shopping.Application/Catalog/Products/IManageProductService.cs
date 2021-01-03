using Shopping.ViewModel.Common;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Shopping.ViewModel.Catalog.Products;
using Shopping.ViewModel.Catalog.ProductImages;

namespace Shopping.Application.Catalog.Products
{
    public interface IManageProductService
    {
        Task<ProductViewModel> GetById(int productId,string languageId);
        Task<int> Create(ProductCreateRequest request);
        Task<int> Update(ProductEditRequest request);
        Task<int> Delete(int productId);
        Task<bool> UpdatePrice(int productId, decimal newPrice);
        Task<bool> UpdateStock(int productId, int newQuantity);
        Task AddViewCount(int productId);
        Task<PageResult<ProductViewModel>> GetAllPaging(GetManageProductPagingRequest request);
        Task<int> AddImage(int productId, ProductImageCreateRequest request);
        Task<int> UpdateImage(int productId, ProductImageUpdateRequest request);
        Task<int> RemoveImage(int productId, int imageId);
        Task<ProductImageViewModel> GetImageById(int productId, int imageId);
        Task<PageResult<ProductImageViewModel>> GetProductImage(int productId,GetProductImagePaging request);
    }
}
