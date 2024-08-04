using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FadeInBlackScreen : MonoBehaviour
{
    public float fadeDuration = 0.5f; // 渐显时间
    private Image blackScreenImage;
    private Color initialColor;
    private Color targetColor;

    void Start()
    {
        blackScreenImage = GetComponent<Image>();

        // 设置初始颜色为完全透明的黑色
        initialColor = new Color(0f, 0f, 0f, 1f);
        targetColor = new Color(0f, 0f, 0f, 0f); // 目标颜色为完全不透明的黑色

        // 确保黑幕颜色为初始颜色
        blackScreenImage.color = initialColor;

        // 开始渐显过程
        StartCoroutine(FadeIn());
    }

    private IEnumerator FadeIn()
    {
        float elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            // 计算当前颜色
            blackScreenImage.color = Color.Lerp(initialColor, targetColor, elapsedTime / fadeDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        // 确保最终颜色为目标颜色
        blackScreenImage.color = targetColor;
        gameObject.SetActive(false);
    }
}
