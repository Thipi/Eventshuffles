using Eventshuffle.Application.DTOs;
using Eventshuffle.Domain.Interfaces;
using Eventshuffle.src.Eventshuffle.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Eventshuffle.Application.Services
{
    public class EventService : IEventService
    {
        private readonly IEventRepository _eventRepository;

        public EventService(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }

        public async Task<EventDto> CreateEventAsync(CreateEventRequest request)
        {
            var newEvent = new Event
            {

                Name = request.Name,
            };

            List<DateOption> dateOptions = new List<DateOption>();
            request.Dates.ForEach(d => dateOptions.Add(new DateOption
            {
                Date = d.Date,
            }));

            newEvent.DateOptions = dateOptions;

            await _eventRepository.AddAsync(newEvent);

            return new EventDto
            {
                Id = newEvent.Id,
                Name = newEvent.Name,
                Dates = newEvent.DateOptions
            };
        }

        public async Task<EventDto> GetEventByIdAsync(int eventId)
        {
            var eventEntity = await _eventRepository.GetByIdAsync(eventId);

            if (eventEntity == null) return null;

            return new EventDto
            {
                Id = eventEntity.Id,
                Name = eventEntity.Name,
                Dates = eventEntity.DateOptions,
                People = eventEntity.People
            };
        }

        public async Task<EventResultDto> GetEventResultsAsync(int eventId)
        {
            var eventEntity = await _eventRepository.GetByIdAsync(eventId);

            if (eventEntity == null) return null;

            return new EventResultDto
            {
                Name = eventEntity.Name,
                suitableDates = eventEntity.DateOptions.Where(d => d.People.Count > 0).ToList(),
            };
        }

        public async Task<List<EventDto>> GetEventsAsync()
        {
            List<EventDto> eventDtos = new List<EventDto>();

            List<Event> events = await _eventRepository.GetAllAsync();

            foreach (Event e in events)
            {
                eventDtos.Add(new EventDto
                {
                    Id = e.Id,
                    Dates = e.DateOptions,
                    Name = e.Name,
                    People = e.People
                }
                );
            }

            return eventDtos;
        }


        public async Task<bool> AddVoteAsync(CreateVoteRequest request)
        {
            var eventEntity = await _eventRepository.GetByIdAsync(request.EventId);

            if (eventEntity == null) return false;

            foreach (DateTime date in request.Dates)
            {
                eventEntity.DateOptions.First(d => d.Date == date).People.Add(request.UserId);
            }
            await _eventRepository.UpdateAsync(eventEntity);

            return true;
        }

    }
}