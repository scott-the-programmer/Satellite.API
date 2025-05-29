using Moq;
using Satellite.Interfaces;
using System.Net;

namespace Satellite.Tests
{
    public class N2YOSatelliteClientTests
    {
        [Fact]
        public async Task GetSatellitesAsync_ReturnsSatelliteData()
        {
            // Arrange
            var httpMock = new Mock<IHttpClient>();
            var response = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(GetSampleSatelliteDataJson()),
            };

            httpMock.Setup(x => x.GetAsync(It.IsAny<string>())).ReturnsAsync(response);

            var client = new N2YOSatelliteClient(httpMock.Object, "test-api-key");

            // Act
            var satellites = await client.GetSatellitesAsync(-35.0, 158.0, 3, (int)SatelliteType.Iridium);

            // Assert
            Assert.NotNull(satellites);
            Assert.Equal(8, satellites.Count());
        }

        [Fact]
        public async Task GetSatellitesAsync_HandlesEmptyResponse()
        {
            // Arrange
            var httpClientMock = new Mock<IHttpClient>();
            var response = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = null
            };

#pragma warning disable CS8620
            _ = httpClientMock.Setup(x => x.GetAsync(It.IsAny<string>())).ReturnsAsync(response);
#pragma warning restore CS8620

            var nasaSatellite = new N2YOSatelliteClient(httpClientMock.Object, "1234");

            // Act
            var satellites = await nasaSatellite.GetSatellitesAsync(0, 0, 0, 0);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.Empty(satellites);
                Assert.NotNull(satellites);
            });
        }

        [Fact]
        public async Task GetSatellitesAsync_HandlesDeserializationError()
        {
            // Arrange
            var httpClientMock = new Mock<IHttpClient>();
            var response = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent("invalid_json_data"),
            };

            httpClientMock.Setup(x => x.GetAsync(It.IsAny<string>())).ReturnsAsync(response);

            var nasaSatellite = new N2YOSatelliteClient(httpClientMock.Object, "1234");

            // Act
            var satellites = await nasaSatellite.GetSatellitesAsync(0, 0, 0, 0);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.Empty(satellites);
                Assert.NotNull(satellites);
            });
        }

        private string GetSampleSatelliteDataJson()
        {
            var sampleData = """
                                {
                   "info":{
                      "category":"Iridium",
                      "transactionscount":5,
                      "satcount":8
                   },
                   "above":[
                      {
                         "satid":24842,
                         "satname":"IRIDIUM 911",
                         "intDesignator":"1997-030G",
                         "launchDate":"1997-06-18",
                         "satlat":-59.9823,
                         "satlng":-173.7582,
                         "satalt":770.4524
                      },
                      {
                         "satid":24871,
                         "satname":"IRIDIUM 920",
                         "intDesignator":"1997-034C",
                         "launchDate":"1997-07-09",
                         "satlat":-32.7174,
                         "satlng":-159.0676,
                         "satalt":768.4795
                      },
                      {
                         "satid":24967,
                         "satname":"IRIDIUM 36",
                         "intDesignator":"1997-056C",
                         "launchDate":"1997-09-27",
                         "satlat":-35.6729,
                         "satlng":158.2153,
                         "satalt":784.267
                      },
                      {
                         "satid":25527,
                         "satname":"IRIDIUM 2",
                         "intDesignator":"1998-066A",
                         "launchDate":"1998-11-06",
                         "satlat":-36.5521,
                         "satlng":171.2387,
                         "satalt":432.6421
                      },
                      {
                         "satid":42959,
                         "satname":"IRIDIUM 119",
                         "intDesignator":"2017-061E",
                         "launchDate":"2017-10-09",
                         "satlat":-41.4083,
                         "satlng":161.356,
                         "satalt":794.8115
                      },
                      {
                         "satid":43569,
                         "satname":"IRIDIUM 160",
                         "intDesignator":"2018-061A",
                         "launchDate":"2018-07-25",
                         "satlat":-58.9183,
                         "satlng":-169.8877,
                         "satalt":801.9225
                      },
                      {
                         "satid":43571,
                         "satname":"IRIDIUM 158",
                         "intDesignator":"2018-061C",
                         "launchDate":"2018-07-25",
                         "satlat":-26.3792,
                         "satlng":-165.7311,
                         "satalt":788.1109
                      },
                      {
                         "satid":56730,
                         "satname":"IRIDIUM 179",
                         "intDesignator":"2023-068W",
                         "launchDate":"2023-05-20",
                         "satlat":-51.8311,
                         "satlng":166.5028,
                         "satalt":650.1408
                      }
                   ]
                }
                """;

            return sampleData;
        }
    }
}
