using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class logFadeInOut : MonoBehaviour
{
    public float fadeInDuration = 1.0f; // ���Գ���ʱ��
    public float fadeOutDuration = 1.0f; // ��������ʱ��
    public float delayBeforeFadeOut = 2.0f; // ���Ե�����֮����ӳ�ʱ��

    private Text text;
    private Color originalColor;

    void Start()
    {
        text = GetComponent<Text>();
        if (text == null)
        {
            Debug.LogError("û���ҵ� Text ���");
            return;
        }

        originalColor = text.color;
        StartCoroutine(FadeInAndOut());
    }

    private IEnumerator FadeInAndOut()
    {
        // ����
        yield return StartCoroutine(Fade(0, 1, fadeInDuration));

        // �ӳ�
        yield return new WaitForSeconds(delayBeforeFadeOut);

        // ����
        yield return StartCoroutine(Fade(1, 0, fadeOutDuration));

        // �������������ٶ���
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
