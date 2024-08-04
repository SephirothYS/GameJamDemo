using TbsFramework.HOMMExample;
using UnityEngine;
using UnityEngine.UI;

public class SliderFillAmountControl : MonoBehaviour
{
    public Image image; // 拖拽Image组件到此字段
    public HOMMUnit character;
    private Text text;

    void Start()
    {

    }

    void Update()
    {
        image.fillAmount = character.Oxygen / 100.0f;
        text = GameObject.Find("TextCount").GetComponent<Text>();
        text.text = ((int)(character.Oxygen)).ToString();

    }
}
