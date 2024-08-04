using UnityEngine;
using UnityEngine.UI;

public class GeneralButton : MonoBehaviour
{
    public static GeneralButton Instance;

    public GameObject actionButton;
    public GameObject trueButton;
    public GameObject badButton;
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
        if (trueButton != null)
        {
            trueButton.SetActive(false); // ��ʼ״̬�°�ť���ɼ�

            // Ϊ��ť����¼��󶨷���
            trueButton.GetComponent<Button>().onClick.AddListener(onTrueButtonClick);
        }
        if (badButton != null)
        {
            badButton.SetActive(false); // ��ʼ״̬�°�ť���ɼ�

            // Ϊ��ť����¼��󶨷���
            badButton.GetComponent<Button>().onClick.AddListener(onBadButtonClick);
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

    public void ShowEndingButton(Vector3 position, InteractiveEvent triggerZone)
    {
        currentTriggerZone = triggerZone;
        if (trueButton != null && badButton != null)
        {
            Vector3 trueButtonPosition = position + new Vector3(3f, 0f, 0);
            Vector3 badButtonPosition = position + new Vector3(2f, 2f, 0);
            Vector2 trueScreenPoint = Camera.main.WorldToScreenPoint(trueButtonPosition);
            Vector2 badScreenPoint = Camera.main.WorldToScreenPoint(badButtonPosition);
            trueButton.GetComponent<RectTransform>().position = trueScreenPoint;
            badButton.GetComponent<RectTransform>().position = badScreenPoint;
            trueButton.SetActive(true);
            badButton.SetActive(true);
            if (DataManager.Instance.explorePoint >= 60)
            {
                trueButton.transform.Find("lock").gameObject.GetComponent<Image>().gameObject.SetActive(false);
                trueButton.transform.Find("HiddenText").gameObject.GetComponent<Text>().text = "�������ź�";
            }
        }
    }

    private void onTrueButtonClick()
    {
        if (trueButton != null)
        {
            if(DataManager.Instance.explorePoint >= 60) trueButton.SetActive(false);
            currentTriggerZone.TrueEnding();
        }
    }

    private void onBadButtonClick()
    {
        if (badButton != null)
        {
            badButton.SetActive(false);
            currentTriggerZone.BadEnding();
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

    public void HideEndingButton()
    {
        if (trueButton != null)
        {
            currentTriggerZone = null;
            trueButton.SetActive(false);
        }
        if (badButton != null)
        {
            currentTriggerZone = null;
            badButton.SetActive(false);
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