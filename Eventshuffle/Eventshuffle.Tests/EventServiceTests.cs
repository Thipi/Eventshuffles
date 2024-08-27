using Microsoft.AspNetCore.Mvc.Testing;
using NUnit.Framework;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Eventshuffle.Application.DTOs;
using System.Collections.Generic;
using Moq;
using Eventshuffle.Domain.Interfaces;
using Eventshuffle.Application.Services;
using Eventshuffle.Domain;
using Eventshuffle.Domain.Entities;

namespace Eventshuffle.Tests
{
    [TestFixture]
    public class EventServiceTests
    {
        private Mock<IEventRepository> _eventRepositoryMock;
        private EventService _eventService;

        [SetUp]
        public void Setup()
        {
            _eventRepositoryMock = new Mock<IEventRepository>();
            _eventService = new EventService(_eventRepositoryMock.Object);
        }

        [Test]
        public async Task CreateEventAsync_ShouldCreateEventAndReturnDto()
        {
            // Arrange
            var request = new CreateEventRequest
            {
                Name = "Test Event",
                Dates = new List<DateTime> { new DateTime(2024, 8, 24) }
            };

            var eventEntity = new Event
            {
                Id = 1,
                Name = "Test Event",
                DateOptions = request.Dates.Select(d => new DateOption { Date = d }).ToList()
            };

            _eventRepositoryMock.Setup(repo => repo.AddAsync(It.IsAny<Event>()))
                .Callback<Event>(e => eventEntity.Id = e.Id)
                .Returns(Task.CompletedTask);

            // Act
            var result = await _eventService.CreateEventAsync(request);

            // Assert
            Assert.That(result != null);
            Assert.AreEqual(request.Name, result.Name);
            Assert.AreEqual(request.Dates.Count, result.Dates.Count);
        }

        [Test]
        public async Task GetEventByIdAsync_ShouldReturnEventDto()
        {
            // Arrange
            var eventEntity = new Event
            {
                Id = 1,
                Name = "Test Event",
                DateOptions = new List<DateOption> { new DateOption { Date = new DateTime(2024, 8, 24) } },
                People = new List<string>()
            };

            _eventRepositoryMock.Setup(repo => repo.GetByIdAsync(1))
                .ReturnsAsync(eventEntity);

            // Act
            var result = await _eventService.GetEventByIdAsync(1);

            // Assert
            Assert.That(result != null);
            Assert.AreEqual(eventEntity.Name, result.Name);
            Assert.AreEqual(eventEntity.DateOptions.Count, result.Dates.Count);
        }

        [Test]
        public async Task GetEventResultsAsync_ShouldReturnEventResultsDto()
        {
            // Arrange
            var eventEntity = new Event
            {
                Id = 1,
                Name = "Test Event",
                DateOptions = new List<DateOption>
                {
                    new DateOption { Date = new DateTime(2024, 8, 24), People = new List<string> { "User1" } },
                    new DateOption { Date = new DateTime(2024, 8, 25), People = new List<string>() }
                }
            };

            _eventRepositoryMock.Setup(repo => repo.GetByIdAsync(1))
                .ReturnsAsync(eventEntity);

            // Act
            var result = await _eventService.GetEventResultsAsync(1);

            // Assert
            Assert.That(result != null);
            Assert.AreEqual(1, result.suitableDates.Count);
        }

        [Test]
        public async Task GetEventsAsync_ShouldReturnListOfEventDtos()
        {
            // Arrange
            var events = new List<Event>
            {
                new Event
                {
                    Id = 1,
                    Name = "Test Event 1",
                    DateOptions = new List<DateOption> { new DateOption { Date = new DateTime(2024, 8, 24) } },
                    People = new List<string>()
                },
                new Event
                {
                    Id = 2,
                    Name = "Test Event 2",
                    DateOptions = new List<DateOption> { new DateOption { Date = new DateTime(2024, 8, 25) } },
                    People = new List<string>()
                }
            };

            _eventRepositoryMock.Setup(repo => repo.GetAllAsync())
                .ReturnsAsync(events);

            // Act
            var result = await _eventService.GetEventsAsync();

            // Assert
            Assert.That(result != null);
            Assert.AreEqual(2, result.Count);
        }

        [Test]
        public async Task AddVoteAsync_ShouldAddVoteToDateOptions()
        {
            // Arrange
            var eventEntity = new Event
            {
                Id = 1,
                Name = "Test Event",
                DateOptions = new List<DateOption>
                {
                    new DateOption { Date = new DateTime(2024, 8, 24), People = new List<string>() }
                }
            };

            var request = new CreateVoteRequest
            {
                UserId = "User1",
                EventId = 1,
                Dates = new List<DateTime> { new DateTime(2024, 8, 24) }
            };

            _eventRepositoryMock.Setup(repo => repo.GetByIdAsync(1))
                .ReturnsAsync(eventEntity);
            _eventRepositoryMock.Setup(repo => repo.UpdateAsync(It.IsAny<Event>()))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _eventService.AddVoteAsync(request);

            // Assert/
            Assert.That(result == true);
            Assert.AreEqual(1, eventEntity.DateOptions.First().People.Count);
            Assert.AreEqual("User1", eventEntity.DateOptions.First().People.First());
        }
    }
}
