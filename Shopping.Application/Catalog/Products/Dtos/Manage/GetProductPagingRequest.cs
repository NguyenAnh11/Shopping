using System;
using System.Collections.Generic;
using System.Text;
using Shopping.Application.Dtos;

namespace Shopping.Application.Catalog.Products.Dtos.Manage
{
    public class GetProductPagingRequest : PagingRequestBase
    {
        public string keyword { get; set; }
        public List<int> CategoryId { get; set; }
    }
}
