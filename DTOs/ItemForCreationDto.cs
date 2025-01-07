using System.ComponentModel.DataAnnotations;

namespace MyWebApi.DTOs;

public class ItemForCreationDto
{
    // Navnet på varen, kræves og må ikke være tomt
    [Required]
    [StringLength(100, ErrorMessage = "Name length can't exceed 100 characters.")]
    public string Name { get; set; }

    // Prisen på varen, kræves og skal være en positiv værdi
    [Required]
    [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0.")]
    public decimal Price { get; set; }
}