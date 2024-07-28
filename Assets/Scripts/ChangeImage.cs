using UnityEngine;
using UnityEngine.UI;

public class TopAlignImageScaler : MonoBehaviour
{
    public Image imageToScale;
    public RectTransform transform;
    public Sprite sourceImage; // Source Image to be set

    void Start()
    {
        if (imageToScale != null && sourceImage != null)
        {
            SetImageAndScale(sourceImage);
        }
    }

    public void SetImageAndScale(Sprite sprite)
    {
        imageToScale.sprite = sprite;

        // 获取原始图片的尺寸
        float originalWidth = sprite.rect.width;
        float originalHeight = sprite.rect.height;

        // 获取Image组件的RectTransform
        // RectTransform imageRectTransform = imageToScale.rectTransform;

        // // 获取父容器的RectTransform
        // RectTransform parentRectTransform = imageRectTransform.parent.GetComponent<RectTransform>();

        // 计算缩放比例
        float widthRatio = transform.rect.width / originalWidth;
        float heightRatio = transform.rect.height / originalHeight;
        float scaleRatio = Mathf.Min(widthRatio, heightRatio);

        // 计算新的尺寸
        float newWidth = originalWidth * scaleRatio;
        float newHeight = originalHeight * scaleRatio;

        // 设置Image的尺寸
        transform.sizeDelta = new Vector2(newWidth, newHeight);

        // 设置Image的对齐方式为顶部对齐
        transform.anchorMin = new Vector2(0.5f, 1);
        transform.anchorMax = new Vector2(0.5f, 1);
        transform.pivot = new Vector2(0.5f, 1);
        Vector2 anchoredPosition = transform.anchoredPosition;
        anchoredPosition.y = 23.9999f;
        transform.anchoredPosition = anchoredPosition;
        // transform.anchoredPosition = new Vector2(0, 0);
    }
}
