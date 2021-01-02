using Shopping.ViewModel.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shopping.ViewModel.Catalog.Products
{
    public class GetManageProductPagingRequest : PagingRequestBase
    {
        public string keyword { get; set; }
        public List<int> CategoryId { get; set; }
    }
}
