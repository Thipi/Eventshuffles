using Eventshuffle.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Eventshuffle.Application.DTOs
{
    public class EventResultDto
    {
        public string Name { get; set; }
        public List<DateOption> suitableDates { get; set; }
    }
}