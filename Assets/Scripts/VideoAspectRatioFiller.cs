using UnityEngine;
using UnityEngine.UI;

public class VideoAspectRatioFitter : MonoBehaviour
{
    public RectTransform videoDisplay;
    public RectTransform leftFill;
    public RectTransform rightFill;
    public RenderTexture renderTexture;

    void Start()
    {
        
    }

    public void AdjustAspectRatio()
    {
        // 获取屏幕的宽高比
        float screenAspect = (float)Screen.width / (float)Screen.height;
        // 获取视频的宽高比
        float videoAspect = (float)renderTexture.width / (float)renderTexture.height;

        // 视频宽高比小于屏幕宽高比时，视频左右填充
        if (videoAspect < screenAspect)
        {
            float fillWidth = (Screen.width - (Screen.height * videoAspect)) / 2;

            leftFill.sizeDelta = new Vector2(fillWidth, videoDisplay.rect.height);
            rightFill.sizeDelta = new Vector2(fillWidth, videoDisplay.rect.height);

            // 设置填充的位置
            leftFill.anchoredPosition = new Vector2(fillWidth / 2, 0);
            rightFill.anchoredPosition = new Vector2(-fillWidth / 2, 0);

            leftFill.gameObject.SetActive(true);
            rightFill.gameObject.SetActive(true);
        }
        else
        {
            leftFill.gameObject.SetActive(false);
            rightFill.gameObject.SetActive(false);
        }
    }

    void Update()
    {
        // 可选：如果屏幕分辨率可能发生变化，可以在 Update 中实时调整
        // AdjustAspectRatio();
    }
}
