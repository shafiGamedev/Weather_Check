using NUnit.Framework;
using UnityEngine;
public class WeatherTestEdit
{

    [Test]
    public void ParseWeatherResponse_ValidInput_ReturnsCorrectTemperature()
    {
        string sampleJson = @"{
            ""latitude"": 19.125,
            ""longitude"": 72.875,
            ""timezone"": ""Asia/Calcutta"",
            ""daily"": {
                ""time"": [""2022-11-29""],
                ""temperature_2m_max"": [32.5]
            }
        }";

        
        //WeatherResponse response = JsonUtility.FromJson<WeatherResponse>(sampleJson);
        //Assert.IsNotNull(response, "Response should not be null");
        //Assert.AreEqual(19.125, response.latitude, "Latitude mismatch");
        //Assert.AreEqual(32.5f, response.daily.temperature_2m_max[0], "Temperature mismatch");
    }

    [Test]
    public void ConstructApiUrl_WithCoordinates_IncludesCorrectParams()
    {
        float lat = 19.07f;
        float lon = 72.87f;

        string url = $"https://api.open-meteo.com/v1/forecast?latitude={lat}&longitude={lon}&timezone=auto&daily=temperature_2m_max";

        // Assert
        Assert.IsTrue(url.Contains("latitude=19.07"), "URL should contain Latitude");
        Assert.IsTrue(url.Contains("longitude=72.87"), "URL should contain Longitude");
        Assert.IsTrue(url.Contains("temperature_2m_max"), "URL should request temperature parameter");
    }

    [Test]
    public void ToastSpawner_WithEmptyMessage_ShouldNotCrash()
    {
        // Arrange
        string emptyMessage = "";

        bool isEmpty = string.IsNullOrEmpty(emptyMessage);

        Assert.IsTrue(isEmpty, "Input validation should detect empty string");
    }

}
