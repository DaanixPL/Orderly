using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Orderly.Models;

[Table("Restaurants")]
public class Restaurant
{
    public int Id { get; set; }

    [Required]
    public string Name { get; set; }

    public string ImageUrl { get; set; }

    public string Address { get; set; }

    public List<MenuItem> MenuItems { get; set; }
}
