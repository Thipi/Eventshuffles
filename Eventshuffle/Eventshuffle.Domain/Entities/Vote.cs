using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Eventshuffle.Domain.Entities
{
    public class Vote
    {
        public DateOption DateOption { get; set; }

    }
}
