using Entities.Base;

namespace Entities.DTOs
{
    public class CargoDto : EntityBaseDto
    {
        public string Title { get; set; }
        public int Weight { get; set; }
        public string RegistrationNumber { get; set; }
    }
}
