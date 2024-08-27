
using System.ComponentModel.DataAnnotations;

namespace Eventshuffle.Domain.Entities
{
    public class Event
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public List<DateOption> DateOptions { get; set; } = new List<DateOption>();
        public List<string> People { get; set; } = new List<string>();
    }
}