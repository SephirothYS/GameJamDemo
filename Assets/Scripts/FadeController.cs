using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using System.Collections;

public class FadeController : MonoBehaviour
{
    public Image fadeImage;  // 引用 FadeImage
    public VideoPlayer videoPlayer;  // 引用 VideoPlayer
    public RawImage rawImage;

    public float blackDuration = 2.0f;

    private void Start()
    {
        // // 确保 FadeImage 一开始是全透明的
        // SetAlpha(1.0f);
        // // 开始淡入效果
        // StartCoroutine(FadeIn());

        videoPlayer.loopPointReached += OnVideoEnd;
    }

    private void SetAlpha(float alpha)
    {
        Color color = fadeImage.color;
        color.a = alpha;
        fadeImage.color = color;
    }

    private IEnumerator FadeIn()
    {
        float duration = 2.0f;  // 淡入持续时间
        float currentTime = 0.0f;

        while (currentTime < blackDuration)
        {
            currentTime += Time.deltaTime;
            yield return null;
        }

        currentTime = 0.0f;
        while (currentTime < duration)
        {
            float alpha = Mathf.Lerp(1.0f, 0.0f, currentTime / duration);
            SetAlpha(alpha);
            currentTime += Time.deltaTime;
            yield return null;
        }

        SetAlpha(0.0f);
        // 淡入完成后开始播放视频
        videoPlayer.Play();
    }

    private IEnumerator FadeOut()
    {
        float duration = 2.0f;  // 淡出持续时间
        float currentTime = 0.0f;

        

        while (currentTime < duration)
        {
            float alpha = Mathf.Lerp(0.0f, 1.0f, currentTime / duration);
            SetAlpha(alpha);
            currentTime += Time.deltaTime;
            yield return null;
        }

        SetAlpha(1.0f);
    }

    private void OnVideoEnd(VideoPlayer vp)
    {
        // 停止视频播放
        videoPlayer.Pause();
        rawImage.enabled = false;
        // StartCoroutine(FadeOut());
    }
}
