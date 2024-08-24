using Eventshuffle.Application.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Eventshuffle.Application
{
    public interface IEventService
    {
        // Method to create a new event
        Task<EventDto> CreateEventAsync(CreateEventRequest request);

        // Method to get a list of all events
        Task<List<EventDto>> GetEventsAsync();

        // Method to get a specific event by its ID
        Task<EventDto> GetEventByIdAsync(int eventId);

        // Method to add a vote to an event
        Task<bool> AddVoteAsync(CreateVoteRequest request);

        // Method to get the results of an event
        Task<EventResultDto> GetEventResultsAsync(int eventId);

    }
}