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
            actionButton.SetActive(false); // ��ʼ״̬�°�ť���ɼ�

            // Ϊ��ť����¼��󶨷���
            actionButton.GetComponent<Button>().onClick.AddListener(OnButtonClick);
        }
    }

    public void ShowButton(Vector3 position, InteractiveEvent triggerZone)
    {
        if (actionButton != null)
        {
            currentTriggerZone = triggerZone;

            // ������ťλ��
            Vector3 buttonPosition = position + new Vector3(2, 2, 0); // �����(1,1,0)�����Ϸ���ƫ�ƣ����Ը�����Ҫ����
            Vector2 screenPoint = Camera.main.WorldToScreenPoint(buttonPosition);
            actionButton.GetComponent<RectTransform>().position = screenPoint;

            actionButton.SetActive(true); // ��ʾ��ť
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
            actionButton.SetActive(false); // ���ذ�ť
        }
    }

    private void OnButtonClick()
    {
        if (currentTriggerZone != null)
        {
            currentTriggerZone.EventStart(); // ������ǰ���ӵ��¼�
            HideButton();
        }
        if (Icon != null)
        {
            Icon.IconDisapper();
        }
    }
}