using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shopping.Application.Catalog.Products;
using Shopping.ViewModel.Catalog.Products;
using Shopping.ViewModel.Catalog.ProductImages;
using Microsoft.AspNetCore.Authorization;

namespace Shopping.WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class ProductController : ControllerBase
    {
        private readonly IPublicProductService _publicProductService;
        private readonly IManageProductService _manageProductService;
        public ProductController(IPublicProductService publicProductService
            ,IManageProductService manageProductService)
        {
            _publicProductService = publicProductService;
            _manageProductService = manageProductService;
        }
        //Product
        [HttpGet("{productId}/{languageId}")]
        public async Task<IActionResult> GetById(int productId,string languageId) 
        {
            var product = await _manageProductService.GetById(productId,languageId);
            if (product == null) return BadRequest();
            return Ok(product);
        }
        [HttpGet("{languageId}")]
        public async Task<IActionResult> GetPaggingProduct(string languageId, [FromQuery] GetPublicProductPagingRequest request)
        {
            var product = await _publicProductService.GetAllByCategoryId(languageId, request);
            if (product == null) return BadRequest();
            return Ok(product);
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromForm] ProductCreateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var productId = await _manageProductService.Create(request);
            if (productId == 0) return BadRequest();
            var product = await _manageProductService.GetById(productId, request.LanguageId);

            return Ok(product);
        }
        [HttpPut]
        public async Task<IActionResult> Update([FromForm] ProductEditRequest request)
        {
            var result = await _manageProductService.Update(request);
            if (result == 0) return BadRequest();
            return Ok();
        }
        [HttpDelete("{productId}")]
        public async Task<IActionResult> Delete(int productId)
        {
            var result = await _manageProductService.Delete(productId);
            if (result == 0) return BadRequest();
            return Ok();
        }
        [HttpPatch("{productId}/{newPrice}")]
        public async Task<IActionResult> UpdatePrice(int productId,decimal newPrice)
        {
            bool isSuccess = await _manageProductService.UpdatePrice(productId, newPrice);
            if (isSuccess) return Ok();
            return BadRequest();
        }
        [HttpPatch("{productId}/{quantity}")]
        public async Task<IActionResult> UpdateStock(int productId,int quantity)
        {
            bool isSuccess = await _manageProductService.UpdateStock(productId, quantity);
            if (isSuccess == true) return Ok();
            return BadRequest();
        }
        
        //Image
        [HttpGet("{productId}/{imageId}")]
        public async Task<IActionResult> GetImageById(int productId,int imageId)
        {
            var productImage = await _manageProductService.GetImageById(productId, imageId);
            if (productImage == null) return BadRequest();
            return Ok(productImage);
        }
        [HttpPost("{productId}")]
        public async Task<IActionResult> AddImage(int productId,[FromForm] ProductImageCreateRequest request)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var imageId = await _manageProductService.AddImage(productId, request);
            if (imageId == -1) return BadRequest();

            var productImage = await _manageProductService.GetImageById(productId, imageId);
            return Ok(productImage);
        }
        [HttpPut("{productId}")]
        public async Task<IActionResult> UpdateImage(int productId, [FromForm] ProductImageUpdateRequest request)
        {
            var result = await _manageProductService.UpdateImage(productId, request);
            if (result == 0) return BadRequest();
            return Ok();
        }
        [HttpDelete("{productId}/{imageId}")]
        public async Task<IActionResult> DeleteImage(int productId,int imageId)
        {
            var result = await _manageProductService.RemoveImage(productId, imageId);
            if (result == 0) return BadRequest();
            return Ok();
        }
    }
}
