using Shopping.ViewModel.Common.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shopping.ViewModel.Catalog.Products.Dtos.Manage
{
    public class GetProductPagingRequest : PagingRequestBase
    {
        public string keyword { get; set; }
        public List<int> CategoryId { get; set; }
    }
}
