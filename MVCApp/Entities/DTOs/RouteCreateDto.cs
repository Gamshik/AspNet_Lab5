﻿using System.ComponentModel.DataAnnotations;

namespace Entities.DTOs
{
    public class RouteCreateDto
    {
        [Required(ErrorMessage = "Route Start settlement is required.")]
        public Guid StartSettlementId { get; set; }
        [Required(ErrorMessage = "Route End settlement is required.")]
        public Guid EndSettlementId { get; set; }
        [Required(ErrorMessage = "Route Distance is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Route Distance must be a positive number.")]
        public int Distance { get; set; }
    }
}