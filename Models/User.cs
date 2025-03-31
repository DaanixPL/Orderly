﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("User")]
public class User
{
    public int Id { get; set; }

    [Required]
    [MaxLength(50)]
    public string Username { get; set; }

    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
