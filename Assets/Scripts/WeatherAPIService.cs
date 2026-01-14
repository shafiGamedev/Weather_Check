using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class WeatherAPIService : MonoBehaviour
{
    public void GetWeather(float lat, float lon, System.Action<WeatherResponse> onSuccess, System.Action<string> onError)
    {
        StartCoroutine(GetWeatherCoroutine(lat, lon, onSuccess, onError));
    }

    private IEnumerator GetWeatherCoroutine(float lat, float lon, System.Action<WeatherResponse> onSuccess, System.Action<string> onError)
    {
        string url = $"https://api.open-meteo.com/v1/forecast?latitude={lat}&longitude={lon}&timezone=auto&daily=temperature_2m_max";

        using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
        {
            yield return webRequest.SendWebRequest();

            if (webRequest.result == UnityWebRequest.Result.ConnectionError || webRequest.result == UnityWebRequest.Result.ProtocolError)
            {
                onError?.Invoke(webRequest.error);
            }
            else
            {
                string json = webRequest.downloadHandler.text;
                try
                {
                    WeatherResponse data = JsonUtility.FromJson<WeatherResponse>(json);
                    onSuccess?.Invoke(data);
                }
                catch (System.Exception e)
                {
                    onError?.Invoke("Failed to parse data.");
                }
            }
        }
    }
}