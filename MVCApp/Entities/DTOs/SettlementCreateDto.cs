using System.ComponentModel.DataAnnotations;

namespace Entities.DTOs
{
    public class SettlementCreateDto
    {
        [Required(ErrorMessage = "Settlement Title is required.")]
        public string Title { get; set; }
    }
}
