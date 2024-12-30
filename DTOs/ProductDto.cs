using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs
{
    public class ProductDto
    {
        public int Id { get; set; }
        [Display(Name = "نام محصول")]
        [Required(ErrorMessage = "فیلد {0} حتما باید پر بشود")]
        [StringLength(maximumLength: 20, MinimumLength = 1, ErrorMessage = "طول فیلد {0} باید بین {2} و {1} باشد")]
        public string Name { get; set; }
        public string? Description { get; set; }
        [Display(Name = "قیمت محصول")]
        [Required(ErrorMessage = "فیلد  قیمت  حتما باید پر بشود")]
        [Range(1,1000000,ErrorMessage = "بازه فیلد {0} باید بین {1} و {2} باشد")]
        public decimal? Price { get; set; }
        public decimal? Discount { get; set; }
        public bool HasDiscount { get; set; }
        public DiscountTypeEnum? DiscountType { get; set; }
        [Display(Name = "شعبه")]
        [Required(ErrorMessage = "فیلد {0} حتما باید پر بشود")]
        public int? BranchId { get; set; }
        public string? BranchName { get; set; }

    }
}
