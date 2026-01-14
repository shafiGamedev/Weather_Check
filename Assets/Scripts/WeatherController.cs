using UnityEngine;

public class WeatherController : MonoBehaviour
{
    [Header("References")]
    public LocationService locationService; 
    public WeatherAPIService apiService;

    private void Start()
    {
        locationService.GetLocation(
            (coordinates) =>
            {
                Debug.Log($"Location Found: {coordinates}");
                apiService.GetWeather(coordinates.x, coordinates.y, OnWeatherSuccess, OnWeatherError);
            },
            (errorMessage) =>
            {
                ToastSpawner.Show(errorMessage, ToastPosition.MiddleCenter);
            }
        );
    }

    private void OnWeatherSuccess(WeatherResponse response)
    {
        if (response != null && response.daily != null && response.daily.temperature_2m_max.Length > 0)
        {
            float temp = response.daily.temperature_2m_max[0];
            string message = $"Temp: {temp}°C";
            ToastSpawner.Show(message, ToastPosition.MiddleCenter);
        }
        else
        {
            ToastSpawner.Show("Weather data empty.");
        }
    }

    private void OnWeatherError(string error)
    {
        ToastSpawner.Show("API Error: " + error, ToastPosition.BottomCenter);
    }
}