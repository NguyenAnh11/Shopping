using System;
using System.Collections.Generic;
using System.Text;
using Shopping.ViewModel.Common;

namespace Shopping.ViewModel.Catalog.Products
{
    public class GetPublicProductPagingRequest:PagingRequestBase
    {
        public int ? CategoryId { get; set; }
    }
}
