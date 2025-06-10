using Microsoft.Extensions.Caching.Memory;
using Moq;
using Satellite.Interfaces;
using Satellite.Models;
using System.Net;
using System.Text.Json;
using FluentAssertions;

namespace Satellite.DataAccess.Services.Tests
{
    public class SatelliteServiceTests
    {

        [Fact]
        public async Task GetSatellitesAsync_ReturnsCachedDataIfAvailable()
        {
            // Arrange
            var httpMock = new Mock<IHttpClient>();
            var response = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(GetSampleSatelliteDataJson()),
            };

            httpMock.Setup(x => x.GetAsync(It.IsAny<string>())).ReturnsAsync(response);

            var nasaSatelliteClient = new N2YOSatelliteClient(httpMock.Object, "1234");
            var memoryCacheMock = new Mock<IMemoryCache>();
            var data = JsonSerializer.Deserialize<SatelliteInfo>(GetSampleSatelliteDataJson());
            object cachedData = data.Above.Select(i => new Models.Satellite
            {
                Name = i.Satname,
                Latitude = i.Satlat,
                Longitude = i.Satlng,
            });

            var service = new SatelliteService(nasaSatelliteClient, memoryCacheMock.Object, new CurrentCoords
            {
                Latitude = 0,
                Longitude = 0
            });

            // Mock the cache to return null (data not cached)
            memoryCacheMock.Setup(x => x.TryGetValue("iridium", out cachedData))
                           .Returns(true);

            // Act
            var satellites = await service.GetIridiumsAsync();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.NotNull(satellites);
                Assert.Equal(((IEnumerable<Models.Satellite>)cachedData).Count(), satellites.Count());
            });
        }


        [Fact]
        public async Task GetSatellitesAsync_FetchesDataFromClientIfNotCached()
        {
            // Arrange
            var httpMock = new Mock<IHttpClient>();
            var response = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(GetSampleSatelliteDataJson()),
            };

            httpMock.Setup(x => x.GetAsync(It.IsAny<string>())).ReturnsAsync(response);

            var nasaSatelliteClient = new N2YOSatelliteClient(httpMock.Object, "1234");
            var memoryCacheMock = new Mock<IMemoryCache>();
            var data = JsonSerializer.Deserialize<SatelliteInfo>(GetSampleSatelliteDataJson());
            var service = new SatelliteService(nasaSatelliteClient, memoryCacheMock.Object, new CurrentCoords
            {
                Latitude = 0,
                Longitude = 0
            });

            // Mock the cache to return null (data not cached)
            object cachedData;
            var cachedEntry = new Mock<ICacheEntry>();
            memoryCacheMock.Setup(x => x.TryGetValue("iridium", out cachedData))
                           .Returns(false);
            memoryCacheMock.Setup(x => x.CreateEntry(It.IsAny<object>())).Returns(cachedEntry.Object);


            // Act
            var satellites = await service.GetIridiumsAsync();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.NotNull(satellites);
                memoryCacheMock.Verify(x => x.CreateEntry(It.IsAny<object>()), Times.Once());
            });
        }

        [Fact]
        public async Task GetIridiumsAsync_FiltersByProximity_ReturnsOnlyProximateSatellites()
        {
            // Arrange
            var httpMock = new Mock<IHttpClient>();
            var apiResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(GetSampleSatelliteDataJson()),
            };

            httpMock.Setup(x => x.GetAsync(It.IsAny<string>())).ReturnsAsync(apiResponse);

            // Use N2YOSatelliteClient as the ISatelliteClient implementation
            var nasaSatelliteClient = new N2YOSatelliteClient(httpMock.Object, "test-api-key");

            var memoryCacheMock = new Mock<IMemoryCache>();
            object cachedData;
            memoryCacheMock.Setup(x => x.TryGetValue("iridium", out cachedData))
                           .Returns(false);

            var cacheEntryMock = new Mock<ICacheEntry>();
            memoryCacheMock.Setup(x => x.CreateEntry(It.IsAny<object>())).Returns(cacheEntryMock.Object);

            // Changed double literals to float literals by adding 'F' suffix
            var userCoords = new CurrentCoords { Latitude = (float)-35.0, Longitude = (float)158.0 };
            var service = new SatelliteService(nasaSatelliteClient, memoryCacheMock.Object, userCoords);

            // Act
            var satellites = await service.GetIridiumsAsync();

            // Assert
            Assert.NotNull(satellites);
            var proximateSatellite = Assert.Single(satellites);
            proximateSatellite.Name.Should().Be("IRIDIUM 36");
            proximateSatellite.Latitude.Should().BeApproximately(-35.6729f, 0.0001f);
            proximateSatellite.Longitude.Should().BeApproximately(158.2153f, 0.0001f);

            // Verify that the HTTP client was called (due to cache miss)
            httpMock.Verify(x => x.GetAsync(It.IsAny<string>()), Times.Once);
            // Verify that the cache was attempted to be updated with the key "iridium"
            memoryCacheMock.Verify(x => x.CreateEntry("iridium"), Times.Once);
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
