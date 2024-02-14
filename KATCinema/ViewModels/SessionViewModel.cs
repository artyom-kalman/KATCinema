using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KATCinema.ViewModels
{
    public class SessionViewModel
    {
        public int Id { get; set; }
        [Display(Name = "Фильм")]
        public int MovieId{ get; set; }
        [Display(Name = "Время")]
        public DateTime StartTime { get; set; }
        [Display(Name = "Зал")]
        public int HallId { get; set; }
        [Display(Name = "Цена билета")]
        public decimal TicketPrice { get; set; }
    }
}
