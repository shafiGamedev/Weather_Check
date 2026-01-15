using System;
using System.Collections;
using UnityEngine;

public class LocationService : MonoBehaviour
{
    private Action<Vector2> _cachedOnSuccess;
    private Action<string> _cachedOnError;

    public void GetLocation(Action<Vector2> onSuccess, Action<string> onError)
    {
        _cachedOnSuccess = onSuccess;
        _cachedOnError = onError;

        StartCoroutine(InitializeLocationCoroutine());
    }

    private void OnApplicationFocus(bool hasFocus)
    {
        if (hasFocus)
        {
            if (Input.location.status == LocationServiceStatus.Stopped ||
                Input.location.status == LocationServiceStatus.Failed)
            {
                Debug.Log("App resumed, attempting to restart location service.");

                if (_cachedOnSuccess != null && _cachedOnError != null)
                {
                    StartCoroutine(InitializeLocationCoroutine());
                }
            }
        }
    }

    private IEnumerator InitializeLocationCoroutine()
    {
        if (!Input.location.isEnabledByUser)
        {
            _cachedOnError?.Invoke("Location service is not enabled by user.");
            yield break;
        }

        Input.location.Start(5f, 5f);

        int maxWait = 20;
        while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
        {
            yield return new WaitForSeconds(1);
            maxWait--;
        }

        if (maxWait < 1)
        {
            _cachedOnError?.Invoke("Timed out initializing location.");
            yield break;
        }

        if (Input.location.status == LocationServiceStatus.Failed)
        {
            _cachedOnError?.Invoke("Unable to determine device location.");
        }
        else
        {
            // Success!
            Vector2 coordinates = new Vector2(Input.location.lastData.latitude, Input.location.lastData.longitude);
            Debug.Log($"Location Service Success: {coordinates}");

            _cachedOnSuccess?.Invoke(coordinates);
        }
    }
}