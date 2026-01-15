using UnityEngine;

public class ToastInstaller : MonoBehaviour
{
    [SerializeField] private ToastUI toastUIPrefab;

    private void Awake()
    {
        if (ToastSpawner.IsInitialized)
            return;

        ToastUI instance = Instantiate(toastUIPrefab);
        instance.name = "TOAST UI";
        DontDestroyOnLoad(instance.gameObject);

        ToastSpawner.Initialize(instance);
    }
}
