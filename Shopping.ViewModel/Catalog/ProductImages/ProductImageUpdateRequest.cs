using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shopping.ViewModel.Catalog.ProductImages
{
    public class ProductImageUpdateRequest
    {
        public int Id { get; set; }
        public string Caption { get; set; }
        public bool isDefault { get; set; }
        public int SortOrder { get; set; }
        public IFormFile FileImage { get; set; }
    }
}
