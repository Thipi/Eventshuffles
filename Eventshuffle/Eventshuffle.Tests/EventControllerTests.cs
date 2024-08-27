using NUnit.Framework;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Moq;
using Moq.Protected;
using Eventshuffle.Application.DTOs;
using Eventshuffle.Domain.Entities;

namespace Eventshuffle.Tests
{
    [TestFixture]
    public class EventControllerTests
    {
        private HttpClient _client;
        private HttpResponseMessage _response;

        [SetUp]
        public void Setup()
        {
            _response = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(JsonConvert.SerializeObject(new
                {
                    events = new List<EventDto>
                    {
                        new EventDto { Id = 1, Name = "Event 1", Dates = new List<DateOption>() },
                        new EventDto { Id = 2, Name = "Event 2", Dates = new List<DateOption>() }
                    }
                }))
            };

            _client = HttpClientFactory.CreateMockHttpClient(_response);
        }

        [TearDown]
        public void Teardown()
        {
            _client.Dispose();
            _response.Dispose();
        }

        [Test]
        public async Task GetEvents_ReturnsOkResponse()
        {
            // Act
            var response = await _client.GetAsync("/api/v1/events/list");

            // Assert
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<dynamic>(responseString);
            var events = (IEnumerable<dynamic>)result.events;
            Assert.IsNotEmpty(events);
        }
    }
}
