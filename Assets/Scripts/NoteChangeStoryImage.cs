using UnityEngine;
using UnityEngine.UI;

public class TopAlignImageScaler : MonoBehaviour
{
    private Image[] imagesToScale = new Image[10];
    private RectTransform[] transforms = new RectTransform[10];
    private Sprite[] sourceImages = new Sprite[10]; // Source Image to be set

    void Start()
    {
        int length = sourceImages.Length;
        for (int i = 0; i < length; i++)
        {
            string name = "Stories/" + GetLabelStr(i + 1);
            Sprite loadSprite = Resources.Load<Sprite>(name);
            if (loadSprite != null)
            {
                sourceImages[i] = loadSprite;
            }
        }

        for (int i = 0; i < length; i++)
        {
            string name = "ImageStory" + (i + 1).ToString();
            Image imgStory = GameObject.Find(name).GetComponent<Image>();
            RectTransform rectTrans = GameObject.Find(name).GetComponent<RectTransform>();
            if (imgStory != null)
            {
                imagesToScale[i] = imgStory;
            }
            if (rectTrans != null)
            {
                transforms[i] = rectTrans;
            }
        }

        if (imagesToScale.Length != 0 && sourceImages.Length != 0)
        {
            for (int i = 0; i < length; i++)
            {
                SetImageAndScale(imagesToScale[i], transforms[i], sourceImages[i]);
            }
        }
    }
    
    string GetLabelStr(int num)
    {
        string str = num.ToString();
        return str.Length == 1 ? "0" + str : str;
    }

    public void SetImageAndScale(Image imageToScale, RectTransform transform, Sprite sprite)
    {
        imageToScale.sprite = sprite;

        // 获取原始图片的尺寸
        float originalWidth = sprite.rect.width;
        float originalHeight = sprite.rect.height;

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
