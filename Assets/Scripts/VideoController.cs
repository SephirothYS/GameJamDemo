using System.Collections;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class VideoController : MonoBehaviour
{
    public float fadeDuration = 0.5f; // 渐显时间
    Color targetColor;
    public Image mask;

    public VideoPlayer videoPlayer;
    public GameObject panelMask;

    public AudioSource audioSource;

    void Awake()
    {
        // 检查 VideoPlayer 组件是否已设置
        if (videoPlayer == null)
        {
            videoPlayer = GetComponent<VideoPlayer>();
        }

        // 注册播放完成事件
        videoPlayer.loopPointReached += OnVideoEnd;

        // 开始播放视频
        // PlayVideo();
    }

    public void PlayVideo()
    {
        panelMask.SetActive(true);
        
        gameObject.SetActive(true);
        Canvas canvas = gameObject.GetComponentInParent<Canvas>();
        canvas.sortingOrder = 3;

        targetColor = new Color(0f, 0f, 0f, 0f);
        StartCoroutine(FadeIn());

        if (videoPlayer != null)
        {
            videoPlayer.audioOutputMode = VideoAudioOutputMode.AudioSource;
            videoPlayer.SetTargetAudioSource(0, audioSource);
            videoPlayer.playOnAwake = false;//取消默认播放
            videoPlayer.IsAudioTrackEnabled(0);//开启音频声道
            videoPlayer.Play();
        }
    }

    private IEnumerator FadeIn()
    {
        Color initialColor = new Color(0f, 0f, 0f, 1f);
        float elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            // 计算当前颜色
            mask.color = Color.Lerp(initialColor, targetColor, elapsedTime / fadeDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        // 确保最终颜色为目标颜色
        mask.color = targetColor;
    }


    void PauseVideo()
    {
        if (videoPlayer != null)
        {
            videoPlayer.Pause();
        }
    }

    void OnVideoEnd(VideoPlayer vp)
    {
        SceneManager.LoadScene(1);

        Destroy(gameObject);
        Debug.Log("Video Ended");
    }

    public void AsyncLoadNextScene()
    {
        StartCoroutine(LoadNextSceneAsync());
    }

    private IEnumerator LoadNextSceneAsync()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(1);
        asyncLoad.allowSceneActivation = false;

        // 等待场景加载完成
        while (!asyncLoad.isDone)
        {
            if (asyncLoad.progress >= 0.9f)
            {
                // 场景加载完成，等待视频播放结束
                yield break;
            }
            yield return null;
        }
    }
}
