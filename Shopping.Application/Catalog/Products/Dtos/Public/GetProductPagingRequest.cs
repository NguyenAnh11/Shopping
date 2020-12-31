using System;
using System.Collections.Generic;
using System.Text;
using Shopping.Application.Dtos;

namespace Shopping.Application.Catalog.Products.Dtos.Public
{
    public class GetProductPagingRequest:PagingRequestBase
    {
        public int? CategoryId { get; set; }
    }
}
