using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Entities.IdentityEntities
{
    public class ApplicationUser:IdentityUser<Guid>
    {
        [StringLength(200)]
        public string? Address { get; set; }
        [StringLength(50)]
        public string  FullName { get; set; }
    }
}
