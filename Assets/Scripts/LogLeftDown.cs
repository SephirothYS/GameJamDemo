using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogLeftDown : MonoBehaviour
{
    private string prefabPath = "TextLog";
    private RectTransform rectTransform;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Log()
    {
        GameObject prefab = Resources.Load<GameObject>(prefabPath);
        GameObject instance = Instantiate(prefab, transform.position, transform.rotation);
        instance.transform.SetParent(transform.parent, false);
        rectTransform = instance.GetComponent<RectTransform>();
        if (rectTransform == null)
        {
            Debug.LogError("没有找到 RectTransform 组件");
            return;
        }

        // 将锚点设置为左下角
        rectTransform.anchorMin = new Vector2(0, 0);
        rectTransform.anchorMax = new Vector2(0, 0);

        rectTransform.anchoredPosition = new Vector2(110.0f, 20.0f);
        // instance.transform.position = new Vector3(110.0f, 20.0f, 0.0f);
        instance.SetActive(true);
    }
}
