using Eventshuffle.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Eventshuffle.Application.DTOs
{
    public class EventDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<DateOption> Dates { get; set; }
        public List<string> People { get; set; }
    }
}