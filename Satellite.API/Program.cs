using Microsoft.Extensions.Caching.Memory;
using Satellite.DataAccess.Services;
using Satellite.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

builder.Services.AddSingleton<IMemoryCache, MemoryCache>();
builder.Services.AddSingleton<IHttpClient, Http>(o =>
{
    var httpClient = new HttpClient();
    return new Http(httpClient);
});

#pragma warning disable CS8604 // Possible null reference argument.
builder.Services.AddSingleton<ISatelliteClient, N2YOSatelliteClient>(o =>
{
    var httpClient = o.GetService<IHttpClient>();
    var key = Environment.GetEnvironmentVariable("N2YO_KEY");
    return new N2YOSatelliteClient(httpClient, key);
});

builder.Services.AddSingleton<ISatelliteService, SatelliteService>(o =>
{
    var client = o.GetService<ISatelliteClient>();
    var cache = o.GetService<IMemoryCache>();
    var lon = Environment.GetEnvironmentVariable("CURRENT_LONGITUDE");
    var lat = Environment.GetEnvironmentVariable("CURRENT_LATITUDE");
    return new SatelliteService(client, cache, new Satellite.Models.CurrentCoords { Longitude = float.Parse(lon), Latitude = float.Parse(lat) });
});
#pragma warning restore CS8604 // Possible null reference argument.

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
