using System;
using System.Collections.Generic;
using System.Text;

namespace Shopping.ViewModel.Common.Dtos
{
    public class PageResult<T> where T: class
    {
        public int TotalRecord { get; set; }
        
        public List<T> Items { get; set; }
    }
}
