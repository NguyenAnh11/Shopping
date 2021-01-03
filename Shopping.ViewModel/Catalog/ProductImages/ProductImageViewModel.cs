using System;
using System.Collections.Generic;
using System.Text;

namespace Shopping.ViewModel.Catalog.ProductImages
{
    public class ProductImageViewModel
    {
        public int Id { get; set; }
        public string FilePath { get; set; }
        public bool isDefault { get; set; }
        public long FileSize { get; set; }
        public DateTime DateCreated { get; set; }
        public int ProductId { get; set; }
    }
}
