using UnityEngine;

public enum ToastPosition
{
    TopLeft,TopCenter,TopRight,MiddleLeft,MiddleCenter,MiddleRight,BottomLeft,BottomCenter,BottomRight
}

public class ToastSpawner
{
    public static bool isLoaded = false;

    private static ToastUI toastUI;

    private static void Prepare()
    {
        if (!isLoaded)
        {
            GameObject instance = MonoBehaviour.Instantiate(Resources.Load<GameObject>("ToastUI"));
            instance.name = "TOAST UI";
            toastUI = instance.GetComponent<ToastUI>();
            isLoaded = true;
        }
    }

    public static void Show(string text)
    {
        Prepare();
        toastUI.Init(text, 2F, ToastPosition.BottomCenter);
    }

    public static void Show(string text, float duration)
    {
        Prepare();
        toastUI.Init(text, duration, ToastPosition.BottomCenter);
    }

    public static void Show(string text, float duration, ToastPosition position)
    {
        Prepare();
        toastUI.Init(text, duration, position);
    }

    public static void Show(string text, ToastPosition position)
    {
        Prepare();
        toastUI.Init(text, 2F, position);
    }

    public static void Dismiss()
    {
        if (isLoaded)
            toastUI.Dismiss();
    }
}

