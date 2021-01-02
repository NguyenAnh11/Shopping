using System;
using System.Collections.Generic;
using System.Text;
using Shopping.ViewModel.Common;

namespace Shopping.ViewModel.Catalog.Products
{
    public class GetProductImagePagingRequest:PagingRequestBase
    {
        public int productId { get; set; }
    }
}
