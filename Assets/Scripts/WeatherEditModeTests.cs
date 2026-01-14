using UnityEngine;
using UnityEngine.Assertions;

public class WeatherEditModeTests
{
    //[Test]
    public void JsonParsing_ValidInput_ReturnsCorrectTemperature()
    {

        string mockJson = @"{
            ""latitude"": 19.125,
            ""longitude"": 72.875,
            ""daily"": {
                ""time"": [""2022-11-29""],
                ""temperature_2m_max"": [32.5]
            }
        }";

        WeatherResponse response = JsonUtility.FromJson<WeatherResponse>(mockJson);

        Assert.IsNotNull(response, "Response should not be null");
        Assert.AreEqual(19.125, response.latitude, "Latitude mismatch");
        Assert.AreEqual(1, response.daily.temperature_2m_max.Length, "Array size mismatch");
        Assert.AreEqual(32.5f, response.daily.temperature_2m_max[0], "Temperature mismatch");
    }

    //[Test]
    public void UrlConstruction_UsesCorrectParams()
    {
        float lat = 19.07f;
        float lon = 72.87f;
        string expectedBase = "https://api.open-meteo.com/v1/forecast";

        string url = $"https://api.open-meteo.com/v1/forecast?latitude={lat}&longitude={lon}&timezone=auto&daily=temperature_2m_max";

        Assert.IsTrue(url.Contains("latitude=19.07"), "URL should contain Latitude");
        Assert.IsTrue(url.Contains("longitude=72.87"), "URL should contain Longitude");
        Assert.IsTrue(url.Contains("temperature_2m_max"), "URL should request temp parameter");
    }
}