using System.ComponentModel.DataAnnotations;

namespace Eventshuffle.Domain.Entities {

    public class DateOption
    {
        [Key]
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public List<string> People { get; set; } = new List<string>();
    }
}