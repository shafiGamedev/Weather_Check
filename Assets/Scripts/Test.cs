using UnityEngine;

public class Test : MonoBehaviour
{
    [SerializeField] float timer;
    [SerializeField] string msg;

    [SerializeField] ToastPosition toastPosition;
    public void ShowMessage()
    {
        ToastSpawner.Show(msg, timer, toastPosition);
    }
}
