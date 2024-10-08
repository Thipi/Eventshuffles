using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System;

namespace Eventshuffle.Application.DTOs
{
    public class CreateVoteRequest
    {
        [Required]
        public string UserId { get; set; }

        [Required]
        public int EventId { get; set; }

        [Required]
        public List<DateTime> Dates { get; set; }

    }
}