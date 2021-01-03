using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shopping.ViewModel.Catalog.ProductImages
{
    public class ProductImageCreateRequest
    {
        public string Caption { get; set; }
        public int SortOrder { get; set; }
        public bool isDefault { get; set; }
        public IFormFile FileImage { get; set; }
    }
}
