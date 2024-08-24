using Eventshuffle.src.Eventshuffle.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Eventshuffle.Application.DTOs
{
    public class VoteDto
    {
        public int Id { get; set; }
        public string UserId { get; set; }

        [JsonIgnore]
        public DateOption DateOption { get; set; }
    }
}