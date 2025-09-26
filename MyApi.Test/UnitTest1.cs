using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using MyApi.Models;

namespace MyApi.Test;

public class WeatherForecastTests : IClassFixture<WebApplicationFactory<Program>> {
    private readonly WebApplicationFactory<Program> _factory;

    public WeatherForecastTests(WebApplicationFactory<Program> factory) {
        _factory = factory;
    }

    [Fact]
    public async Task GetWeatherForecast_ReturnsSuccessAndData() {
        var client = _factory.CreateClient();
        var response = await client.GetAsync("/weatherforecast");
        response.EnsureSuccessStatusCode();
        var forecasts = await response.Content.ReadFromJsonAsync<WeatherForecast[]>();
        Assert.NotNull(forecasts);
        Assert.Equal(5, forecasts.Length);
    }

    [Fact]
    public void WeatherForecast_Constructor_AssignsProperties() {
        var date = DateOnly.FromDateTime(DateTime.Now);
        var temperatureC = 20;
        var summary = "Sunny";

        var forecast = new WeatherForecast(date, temperatureC, summary);

        Assert.Equal(date, forecast.Date);
        Assert.Equal(temperatureC, forecast.TemperatureC);
        Assert.Equal(summary, forecast.Summary);
    }
}
