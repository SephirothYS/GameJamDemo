using UnityEngine;
using UnityEngine.UI;

public class TipButtonManager : MonoBehaviour
{
    public Button confirm;
    public Button cancel;

    public GameObject prefab;

    // Start is called before the first frame update
    void Start()
    {
        GameObject panelIip = transform.Find("PanelTip").gameObject;
        confirm = panelIip.transform.Find("ButtonConfirm").GetComponent<Button>();
        cancel = panelIip.transform.Find("ButtonCancel").GetComponent<Button>();
        confirm.onClick.AddListener(OnConfirmButtonClick);
        cancel.onClick.AddListener(OnCancelButtonClick);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnConfirmButtonClick()
    {
        GameObject pref = Instantiate(prefab, transform.position, transform.rotation);
        pref.transform.parent = GameObject.Find("CanvasUI").transform;
        pref.GetComponent<RectTransform>().localScale = new Vector3(0.4f, 0.4f, 0.4f);
        Destroy(gameObject);
    }

    void OnCancelButtonClick()
    {
        Destroy(gameObject);
    }

    void OnDestroy()
    {
        BeginPropManager.ToggleHasSpawned();
    }
}
