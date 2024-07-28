using System.IO;
using System;
using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    //string path = "D:\\GameJam\\shiban_content.txt";
    public Text text1;
    public Text text2;

    public Button[] buttons; // 所有按钮的数组
    public float selectedWidth = 200f; // 选中时的宽度
    public float normalWidth = 100f; // 未选中时的宽度
    public float transitionSpeed = 5f; // 变化速度

    public bool[] unlockState = new bool[10];

    private int selectedButtonNum = 0;

    string[] paragraphs;

    void Start()
    {
        // 读取文件
        //string fileContent = File.ReadAllText(path);
        //paragraphs = fileContent.Split(new string[] { "//\r\n\r\n" }, StringSplitOptions.None);

        // 初始化按钮状态
        for (int i = 0; i < buttons.Length; i++)
        {
            // 设置每个按钮的初始宽度
            buttons[i].GetComponent<RectTransform>().sizeDelta = new Vector2(normalWidth, buttons[i].GetComponent<RectTransform>().sizeDelta.y);
            // 为每个按钮添加点击事件监听器
            int ii = i;
            buttons[i].onClick.AddListener(() => OnButtonClick(ii));
        }
        if (buttons != null)
        {
            selectedButtonNum = 0;
        }
        InitUnlockState();
        RefreshText();
    }

    void InitUnlockState()
    {
        for (int i = 0; i < unlockState.Length; i++)
        {
            unlockState[i] = true;
        }
    }

    void Update()
    {
        // 平滑过渡到目标宽度
        foreach (var button in buttons)
        {
            var rectTransform = button.GetComponent<RectTransform>();
            float targetWidth = (button == buttons[selectedButtonNum]) ? selectedWidth : normalWidth;
            float currentWidth = rectTransform.sizeDelta.x;
            float newWidth = Mathf.Lerp(currentWidth, targetWidth, Time.deltaTime * transitionSpeed);
            rectTransform.sizeDelta = new Vector2(newWidth, rectTransform.sizeDelta.y);
        }
    }

    void OnButtonClick(int num)
    {
        // 设置当前选中的按钮
        selectedButtonNum = num;
        RefreshText();
    }

    void RefreshText()
    {
        int no1 = selectedButtonNum * 2;
        int no2 = selectedButtonNum * 2 + 1;

        text1.text = paragraphs[no1];
        text2.text = paragraphs[no2];

        if (unlockState[no1])
        {
            text1.enabled = true;
        }
        else
        {
            text1.enabled = false;
        }

        if (unlockState[no2])
        {
            text2.enabled = true;
        }
        else
        {
            text2.enabled = false;
        }
    }
}
