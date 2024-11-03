﻿using System.ComponentModel.DataAnnotations;

namespace Entities.DTOs
{
    public class CargoCreateDto
    {
        [Required(ErrorMessage = "Cargo Title is required.")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Cargo Weight  is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Cargo Weight must be a positive number.")]
        public int Weight { get; set; }
        [Required(ErrorMessage = "Cargo Registration number is required.")]
        public string RegistrationNumber { get; set; }
    }
}