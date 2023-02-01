﻿using System.ComponentModel.DataAnnotations;

namespace RedisAPI.Models;

public class Platform
{
    [Required]
    public string Id { get; set; } = $"Platform:{Guid.NewGuid()}";

    [Required]
    public string Name { get; set; } = string.Empty;
}