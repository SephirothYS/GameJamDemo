using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BeginPropManager : MonoBehaviour
{
    enum EPropNum
    {
        SHOES = 1,
        COMPASS = 2,
        KANCEYI = 3,
    }

    private static bool hasSpawnedTip = false;
    private GameObject panelProp;
    public PropManager pm;

    public GameObject tipPrefab;

    private Image panelFrontImage;

    private GameObject[] buttons = new GameObject[3];

    void Start()
    {
        // 订阅场景加载完成事件
        SceneManager.sceneLoaded += OnSceneLoaded;

        // // 确保PopupPanel开始时是隐藏的
        // if (popupPanel != null)
        // {
        //     popupPanel.SetActive(false);
        // }


        // 获取Panel的子对象Button
        GameObject shoes = GameObject.Find("PanelProp/ButtonShoes");
        GameObject compass = GameObject.Find("PanelProp/ButtonCompass");
        GameObject kanceyi = GameObject.Find("PanelProp/ButtonKanceyi");
        buttons[0] = shoes;
        buttons[1] = compass;
        buttons[2] = kanceyi;

        shoes.GetComponent<Button>().onClick.AddListener(OnButtonShoesClicked);
        compass.GetComponent<Button>().onClick.AddListener(OnButtonCompassClicked);
        kanceyi.GetComponent<Button>().onClick.AddListener(OnButtonKanceyiClicked);

        GameObject pFront = GameObject.Find("PanelFront");
        panelFrontImage = pFront.GetComponent<Image>();
        pFront.SetActive(false);
    }

    void Update()
    {

    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // 显示PopupPanel
        if (panelProp != null)
        {
            panelProp.SetActive(true);
        }
        hasSpawnedTip = false;
    }

    void OnDestroy()
    {
        // 取消订阅场景加载完成事件
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void Refresh()
    {
        int num = pm.GetBeginPropNum() - 1;
        for (int i = 0; i < buttons.Length; i++)
        {
            Color color = buttons[i].GetComponent<Image>().color;
            color.a = 0.2f;
            buttons[i].GetComponent<Image>().color = color;
        }
        Color c = buttons[num].GetComponent<Image>().color;
        c.a = 1.0f;
        buttons[num].GetComponent<Image>().color = c;

        panelFrontImage.gameObject.SetActive(true);
        Color colorpF = panelFrontImage.color;
        colorpF.a = 0.2f;
        panelFrontImage.color = colorpF;
    }

    void OnButtonShoesClicked()
    {
        ShowTip();
        pm.SetBeginPropNum((int)EPropNum.SHOES);
        DataManager.Instance.SetOxygenFactor(2);
        Refresh();
    }

    void OnButtonCompassClicked()
    {
        ShowTip();
        pm.SetBeginPropNum((int)EPropNum.COMPASS);
        Refresh();
    }

    void OnButtonKanceyiClicked()
    {
        ShowTip();
        pm.SetBeginPropNum((int)EPropNum.KANCEYI);
        DataManager.Instance.SetExpploreFactor(1.4f);
        Refresh();
    }

    void ShowTip()
    {
        if (hasSpawnedTip == true) return;
        GameObject instance = Instantiate(tipPrefab, transform.position, transform.rotation);
        // Canvas canvas = instance.GetComponent<Canvas>();
        instance.transform.SetParent(transform, false);
        instance.SetActive(true);
        instance.transform.position = Vector3.zero;

        RectTransform rectTransform = instance.GetComponent<RectTransform>();
        if (rectTransform != null)
        {
            // 设置锚点在中心
            rectTransform.anchorMin = new Vector2(0.5f, 0.5f);
            rectTransform.anchorMax = new Vector2(0.5f, 0.5f);
            rectTransform.pivot = new Vector2(0.5f, 0.5f);

            // 设置位置在父对象的正中央
            rectTransform.anchoredPosition = Vector2.zero;

        }
        hasSpawnedTip = true;

        panelProp = instance.transform.Find("PanelTip").gameObject;
        Button confirmButton = panelProp.transform.Find("ButtonConfirm").GetComponent<Button>();
        confirmButton.onClick.AddListener(ClosePropSelect);
        Button cancelButton = panelProp.transform.Find("ButtonCancel").GetComponent<Button>();
        cancelButton.onClick.AddListener(ClosePropCancel);
    }

    private void ClosePropSelect()
    {
        Destroy(gameObject);
    }

    private void ClosePropCancel()
    {
        panelFrontImage.gameObject.SetActive(false);
    }
    
    public static void ToggleHasSpawned()
    {
        hasSpawnedTip = !hasSpawnedTip;
    }
}
