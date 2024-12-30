using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs
{
    public class RegisterDto
    {
        [Required(ErrorMessage = "نام کامل خود را وارد کنید")]
        [StringLength(maximumLength: 20, MinimumLength = 1, ErrorMessage = "طول فیلد {0} باید بین {2} و {1} باشد")] 
        public string FullName { get; set; }

        [Required(ErrorMessage = "ایمیل خود را وارد کنید")]
        [EmailAddress(ErrorMessage = "فرمت ایمیل اشتباه است")]
        public string Email { get; set; }

        [Required(ErrorMessage = "شماره تلفن را وارد کنید")]
        [Phone(ErrorMessage = "فرمت شماره تلفن اشتباه است")]
        public string Phone { get; set; }
        [StringLength(200)]
        public string? Address { get; set; }

        [Required(ErrorMessage = "پسورد را وارد کنید")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "تکرار پسورد را وارد کنید")]
        [DataType(DataType.Password)]
        [Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }

    }
}
