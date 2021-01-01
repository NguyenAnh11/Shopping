using System;
using System.Collections.Generic;
using System.Text;
using Shopping.ViewModel.Common.Dtos;

namespace Shopping.ViewModel.Catalog.Products.Dtos.Manage
{
    public class GetProductImagePagingRequest:PagingRequestBase
    {
        public int productId { get; set; }
    }
}
