using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class logFadeInOut : MonoBehaviour
{
    public float fadeInDuration = 1.0f; // 渐显持续时间
    public float fadeOutDuration = 1.0f; // 渐隐持续时间
    public float delayBeforeFadeOut = 2.0f; // 渐显到渐隐之间的延迟时间

    private Text text;
    private Color originalColor;

    void Start()
    {
        text = GetComponent<Text>();
        if (text == null)
        {
            Debug.LogError("没有找到 Text 组件");
            return;
        }

        originalColor = text.color;
        StartCoroutine(FadeInAndOut());
    }

    private IEnumerator FadeInAndOut()
    {
        // 渐显
        yield return StartCoroutine(Fade(0, 1, fadeInDuration));

        // 延迟
        yield return new WaitForSeconds(delayBeforeFadeOut);

        // 渐隐
        yield return StartCoroutine(Fade(1, 0, fadeOutDuration));

        // 渐隐结束后销毁对象
        Destroy(gameObject);
    }

    private IEnumerator Fade(float startAlpha, float endAlpha, float duration)
    {
        float elapsedTime = 0;
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(startAlpha, endAlpha, elapsedTime / duration);
            SetAlpha(alpha);
            yield return null;
        }
        SetAlpha(endAlpha);
    }

    private void SetAlpha(float alpha)
    {
        if (text != null)
        {
            Color color = text.color;
            color.a = alpha;
            text.color = color;
        }
    }
}
