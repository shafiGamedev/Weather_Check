using System;
using System.Collections;
using UnityEngine;


public class LocationService : MonoBehaviour
{
    public void GetLocation(Action<Vector2> onSuccess, Action<string> onError)
    {
        StartCoroutine(GetLocationCoroutine(onSuccess, onError));
    }

    private IEnumerator GetLocationCoroutine(Action<Vector2> onSuccess, Action<string> onError)
    {
        if (!Input.location.isEnabledByUser)
        {
            onError?.Invoke("Location not enabled.");
            yield break;
        }

        Input.location.Start(5f, 5f);

        int maxWait = 20;
        while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
        {
            yield return new WaitForSeconds(1);
            maxWait--;
        }

        // Service didn't initialize
        if (maxWait < 1)
        {
            onError?.Invoke("Timed out initializing location.");
            yield break;
        }

        if (Input.location.status == LocationServiceStatus.Failed)
        {
            onError?.Invoke("Unable to determine device location.");
        }
        else
        {
            Vector2 coordinates = new Vector2(Input.location.lastData.latitude, Input.location.lastData.longitude);
            onSuccess?.Invoke(coordinates);
        }

        Input.location.Stop();
    }
}