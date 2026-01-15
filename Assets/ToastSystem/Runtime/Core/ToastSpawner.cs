public enum ToastPosition
{
    TopLeft,TopCenter,TopRight,MiddleLeft,MiddleCenter,MiddleRight,BottomLeft,BottomCenter,BottomRight
}

public static class ToastSpawner
{
    private static ToastUI toastUI;
    public static bool IsInitialized => toastUI != null;

    public static void Initialize(ToastUI ui)
    {
        toastUI = ui;
    }

    public static void Show(string text)
    {
        Show(text, 2f, ToastPosition.BottomCenter);
    }

    public static void Show(string text, float duration)
    {
        Show(text, duration, ToastPosition.BottomCenter);
    }

    public static void Show(string text, ToastPosition position)
    {
        Show(text, 2f, position);
    }

    public static void Show(string text, float duration, ToastPosition position)
    {
        if (!IsInitialized)
        {
            UnityEngine.Debug.LogError("ToastSpawner not initialized. Add ToastInstaller to scene.");
            return;
        }

        toastUI.Init(text, duration, position);
    }

    public static void Dismiss()
    {
        if (IsInitialized)
            toastUI.Dismiss();
    }
}

