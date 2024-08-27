using Moq;
using Moq.Protected;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Eventshuffle.Tests
{
	public static class HttpClientFactory
	{
		public static HttpClient CreateMockHttpClient(HttpResponseMessage response)
		{
			var handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);

			handlerMock.Protected()
				.Setup<Task<HttpResponseMessage>>(
					"SendAsync",
					ItExpr.IsAny<HttpRequestMessage>(),
					ItExpr.IsAny<CancellationToken>())
				.ReturnsAsync(response)
				.Verifiable();

			handlerMock.Protected()
				.Setup("Dispose", ItExpr.IsAny<bool>())
				.Verifiable();

			var client = new HttpClient(handlerMock.Object)
			{
				BaseAddress = new Uri("http://localhost")
			};

			return client;
		}
	}
}
