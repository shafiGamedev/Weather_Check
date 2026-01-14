using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ToastUI : MonoBehaviour
{
    [Header("UI References :")]
    [SerializeField] private CanvasGroup uiCanvasGroup;
    [SerializeField] private RectTransform uiRectTransform;
    [SerializeField] private VerticalLayoutGroup uiContentVerticalLayoutGroup;
    [SerializeField] private Image uiImage;
    [SerializeField] private Text uiText;

    [Header("Toast Fade In/Out Duration :")]
    [Range(0.1f, 0.8f)]
    [SerializeField] private float fadeDuration = 0.3f;

    [Header("Position Settings")]
    [SerializeField] private Vector2 margin = new Vector2(10f, 10f); // Added margin variable

    private int maxTextLength = 300;

    void Awake()
    {
        uiCanvasGroup.alpha = 0f;
    }

    public void Init(string text, float duration, ToastPosition position)
    {
        Show(text, duration, position);
    }

    private void Show(string text, float duration, ToastPosition position)
    {
        uiText.text = (text.Length > maxTextLength) ? text.Substring(0, maxTextLength) + "..." : text;

        Dismiss();
        StartCoroutine(FadeInOut(duration, fadeDuration, position));
    }

    private IEnumerator FadeInOut(float toastDuration, float fadeDuration, ToastPosition position)
    {
        yield return null;

        // Force layout recalculation to ensure we get the correct sizeDelta before positioning
        LayoutRebuilder.ForceRebuildLayoutImmediate(uiRectTransform);
        
        yield return null;
        
        SetMessagePositionOnScreen(position);

        // Anim start
        yield return Fade(uiCanvasGroup, 0f, 1f, fadeDuration);
        yield return new WaitForSeconds(toastDuration);
        yield return Fade(uiCanvasGroup, 1f, 0f, fadeDuration);
        // Anim end
    }

    // THIS IS THE FUNCTION YOU WANTED TO ADD
    private void SetMessagePositionOnScreen(ToastPosition position)
    {
        RectTransform parentRect = uiRectTransform;

        parentRect.pivot = new Vector2(0, 0);

        Vector2 backgroundSize = parentRect.sizeDelta;

        switch (position)
        {
            case ToastPosition.TopLeft:
                parentRect.anchorMax = new Vector2(0, 1);
                parentRect.anchorMin = new Vector2(0, 1);
                parentRect.anchoredPosition = new Vector2(margin.x, -backgroundSize.y - margin.y);
                break;
            case ToastPosition.TopRight:
                parentRect.anchorMax = new Vector2(1, 1);
                parentRect.anchorMin = new Vector2(1, 1);
                parentRect.anchoredPosition = new Vector2(-backgroundSize.x - margin.x, -backgroundSize.y - margin.y);
                break;
            case ToastPosition.TopCenter:
                parentRect.anchorMax = new Vector2(0.5f, 1);
                parentRect.anchorMin = new Vector2(0.5f, 1);
                parentRect.anchoredPosition = new Vector2(-backgroundSize.x / 2, -backgroundSize.y - margin.y);
                break;

            case ToastPosition.MiddleLeft:
                parentRect.anchorMax = new Vector2(0, 0.5f);
                parentRect.anchorMin = new Vector2(0, 0.5f);
                parentRect.anchoredPosition = new Vector2(margin.x, -backgroundSize.y / 2);
                break;
            case ToastPosition.MiddleRight:
                parentRect.anchorMax = new Vector2(1, 0.5f);
                parentRect.anchorMin = new Vector2(1, 0.5f);
                parentRect.anchoredPosition = new Vector2(-backgroundSize.x - margin.x, -backgroundSize.y / 2);
                break;

            case ToastPosition.BottomLeft:
                parentRect.anchorMax = new Vector2(0, 0);
                parentRect.anchorMin = new Vector2(0, 0);
                parentRect.anchoredPosition = new Vector2(margin.x, margin.y);
                break;
            case ToastPosition.BottomCenter:
                parentRect.anchorMax = new Vector2(0.5f, 0);
                parentRect.anchorMin = new Vector2(0.5f, 0);
                parentRect.anchoredPosition = new Vector2(-backgroundSize.x / 2, margin.y);
                break;
            case ToastPosition.BottomRight:
                parentRect.anchorMax = new Vector2(1, 0);
                parentRect.anchorMin = new Vector2(1, 0);
                parentRect.anchoredPosition = new Vector2(-backgroundSize.x - margin.x, margin.y);
                break;

            case ToastPosition.MiddleCenter:
            default: // Default to Center
                parentRect.anchorMax = new Vector2(0.5f, 0.5f);
                parentRect.anchorMin = new Vector2(0.5f, 0.5f);
                parentRect.anchoredPosition = new Vector2(-backgroundSize.x / 2, -backgroundSize.y / 2);
                break;
        }
    }

    private IEnumerator Fade(CanvasGroup cGroup, float startAlpha, float endAlpha, float fadeDuration)
    {
        float startTime = Time.time;
        float alpha = startAlpha;

        if (fadeDuration > 0f)
        {
            //Anim start
            while (alpha != endAlpha)
            {
                alpha = Mathf.Lerp(startAlpha, endAlpha, (Time.time - startTime) / fadeDuration);
                cGroup.alpha = alpha;

                yield return null;
            }
        }

        cGroup.alpha = endAlpha;
    }

    public void Dismiss()
    {
        StopAllCoroutines();
        uiCanvasGroup.alpha = 0f;
    }

    private void OnDestroy()
    {
        ToastSpawner.isLoaded = false;
    }
}