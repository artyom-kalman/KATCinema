﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KATCinema.Models
{
    public class Session
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("Movie")]
        [Display(Name = "Фильм")]
        public int MovieId { get; set; }
        public Movie Movie { get; set; }
        [Display(Name = "Время")]
        public DateTime StartTime { get; set; }
        [ForeignKey("Hall")]
        public int HallId { get; set; }
        [Display(Name = "Зал")]
        public Hall Hall { get; set; }
        [Display(Name = "Цена билета")]
        public decimal TicketPrice { get; set; }
        public List<Reservation> Reservations { get; set; }
    }
}
