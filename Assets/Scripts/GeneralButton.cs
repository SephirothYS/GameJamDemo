using UnityEngine;
using UnityEngine.UI;

public class GeneralButton : MonoBehaviour
{
    public static GeneralButton Instance;

    public GameObject actionButton;
    private InteractiveEvent currentTriggerZone;
    private IconEvent Icon;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        if (actionButton != null)
        {
            actionButton.SetActive(false); // 初始状态下按钮不可见

            // 为按钮点击事件绑定方法
            actionButton.GetComponent<Button>().onClick.AddListener(OnButtonClick);
        }
    }

    public void ShowButton(Vector3 position, InteractiveEvent triggerZone)
    {
        if (actionButton != null)
        {
            currentTriggerZone = triggerZone;

            // 调整按钮位置
            Vector3 buttonPosition = position + new Vector3(2, 2, 0); // 这里的(1,1,0)是右上方的偏移，可以根据需要调整
            Vector2 screenPoint = Camera.main.WorldToScreenPoint(buttonPosition);
            actionButton.GetComponent<RectTransform>().position = screenPoint;

            actionButton.SetActive(true); // 显示按钮
        }
    }

    public void ConnectIcon(IconEvent ic)
    {
        Icon = ic;
    }

    public void HideButton()
    {
        if (actionButton != null)
        {
            currentTriggerZone = null;
            actionButton.SetActive(false); // 隐藏按钮
        }
    }

    private void OnButtonClick()
    {
        if (currentTriggerZone != null)
        {
            currentTriggerZone.EventStart(); // 触发当前格子的事件
            HideButton();
        }
        if (Icon != null)
        {
            Icon.IconDisapper();
        }
    }
}