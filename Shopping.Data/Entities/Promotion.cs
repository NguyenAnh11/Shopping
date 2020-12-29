using Shopping.Data.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shopping.Data.Entities
{
    public class Promotion
    {
        public int Id { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public bool ApplyForAll { get; set; }
        public int ? DiscountPercent { get; set; }
        public decimal? DicountAmount { get; set; }
        public string ProductId { get; set; }
        public string ProductCategoryIds { get; set; }
        public Status Status { get; set; }
        public string Name { get; set; }
    }
}
