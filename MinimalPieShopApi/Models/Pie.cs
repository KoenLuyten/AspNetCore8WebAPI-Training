﻿namespace MinimalPieShopApi.Models
{
    public class Pie
    {
        public int Id { get; set; }

        public required string Name { get; set; }

        public required string Description { get; set; }

        public required string Category { get; set; }
    }
}