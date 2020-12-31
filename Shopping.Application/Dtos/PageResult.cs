using System;
using System.Collections.Generic;
using System.Text;

namespace Shopping.Application.Dtos
{
    public class PageResult<T> where T: class
    {
        public int TotalRecord { get; set; }
        
        public IEnumerable<T> Items { get; set; }
    }
}
