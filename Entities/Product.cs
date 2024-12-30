using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTOs;

namespace Entities
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [StringLength(200)]
        public string? Name { get; set; }
        [StringLength(300)]
        public string? Description { get; set; }
        public decimal? Price { get; set; }
        public decimal? Discount { get; set; }
        public bool HasDiscount { get; set; }
        public DiscountTypeEnum? DiscountType { get; set; }
        public int? BranchId { get; set; }
        public virtual Branch? Branch { get; set; }

    }
}
