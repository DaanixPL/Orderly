using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

[Table("MenuItems")]
public class MenuItem
{
    public int Id { get; set; }

    [ForeignKey("Restaurant")]
    public int RestaurantId { get; set; }

    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;

    [Required]
    public decimal Price { get; set; }

    public string ImageUrl { get; set; }

    [MaxLength(255)]
    public string? Description { get; set; }

    public Restaurant? Restaurant { get; set; }
}

