using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models.DTOs.User
{
    public class UserDeleteDto
    {
        [Required(ErrorMessage = "Settlement Id is required.")]
        public Guid Id { get; set; }
    }
}
