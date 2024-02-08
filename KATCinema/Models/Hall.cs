﻿using System.ComponentModel.DataAnnotations;

namespace KATCinema.Models
{
    public class Hall
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int TotalSeats { get; set; }
        public int TotalRows { get; set; }
        public List<Row> Rows { get; set; }
    }
}
