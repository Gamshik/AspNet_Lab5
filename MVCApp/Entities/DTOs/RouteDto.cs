namespace Entities.DTOs
{
    public class RouteDto
    {
        public SettlementDto? StartSettlement { get; set; }

        public SettlementDto? EndSettlement { get; set; }

        public int Distance { get; set; }
    }
}
