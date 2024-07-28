using UnityEngine;
using UnityEngine.Video;

public class VideoController : MonoBehaviour
{
    public VideoPlayer videoPlayer;

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
        PlayVideo();
    }

    void PlayVideo()
    {
        if (videoPlayer != null)
        {
            videoPlayer.Play();
        }
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
        Debug.Log("Video Ended");
        // 这里可以添加播放结束后的逻辑
    }
}
