using Eventshuffle.src.Eventshuffle.Domain.Entities;
using System;
using System.Collections.Generic;

namespace Eventshuffle.Application.DTOs
{
    public class DateOptionDto
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public List<string> People { get; set; } = new List<string>();
    }
}