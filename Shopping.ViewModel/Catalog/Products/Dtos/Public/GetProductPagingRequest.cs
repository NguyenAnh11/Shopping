using Shopping.ViewModel.Common.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shopping.ViewModel.Catalog.Products.Dtos.Public
{
    public class GetProductPagingRequest : PagingRequestBase
    {
        public int? CategoryId { get; set; }
    }
}
