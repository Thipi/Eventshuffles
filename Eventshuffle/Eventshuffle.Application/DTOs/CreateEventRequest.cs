using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Eventshuffle.Application
{
    public class CreateEventRequest
    {
        // The name of the event
        [Required] // Ensures that the name is provided
        public string Name { get; set; }

        // A list of dates that are suitable for the event
        [Required] // Ensures that at least one date is provided
        public List<DateTime> Dates { get; set; }

    }
}