
using Eventshuffle.Domain.Entities;

namespace Eventshuffle.Domain.Interfaces
{
    public interface IEventRepository
    {
        Task AddAsync(Event eventEntity);
        Task<List<Event>> GetAllAsync();
        Task<Event> GetByIdAsync(int eventId);
        Task UpdateAsync(Event eventEntity);
    }
}
