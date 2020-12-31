using System;
using System.Collections.Generic;
using System.Text;

namespace Shopping.Application.Dtos
{
    public class PagingRequestBase
    {
        public int pageIndex { get; set; }
        public int pageSize { get; set; }
    }
}
