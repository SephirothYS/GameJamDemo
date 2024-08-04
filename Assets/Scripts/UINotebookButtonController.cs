using System.IO;
using System;
using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    private string unselectedColorStr = "#856943";
    private string selectedColorStr = "#CC4E14";

    private Text label1;
    private Text label2;
    private Image questionMark1;
    private Image questionMark2;

    private Button[] buttons = new Button[5]; // 所有按钮的数组
    private Image[] stories = new Image[10]; // 全部石板描述文字

    public float selectedWidth = 200f; // 选中时的宽度
    public float normalWidth = 100f; // 未选中时的宽度
    public float transitionSpeed = 5f; // 变化速度

    public bool[] unlockState = new bool[10];

    private int selectedButtonNum = 0;

    // string[] paragraphs;

    void Awake()
    {
        int length = buttons.Length;
        for (int i = 0; i < buttons.Length; i++)
        {
            string name = "ButtonNum" + (i + 1).ToString();
            GameObject button = GameObject.Find(name);
            buttons[i] = button.GetComponent<Button>();
        }

        int lenStories = stories.Length;
        for (int i = 0; i < stories.Length; i++)
        {
            string name = "ImageStory" + (i + 1).ToString();
            GameObject story = GameObject.Find(name);
            stories[i] = story.GetComponent<Image>();
        }

        GameObject text1 = GameObject.Find("TextLabel1");
        label1 = text1.GetComponent<Text>();
        GameObject text2 = GameObject.Find("TextLabel2");
        label2 = text2.GetComponent<Text>();

        GameObject mark1 = GameObject.Find("ImageQuestionMark");
        questionMark1 = mark1.GetComponent<Image>();
        GameObject mark2 = GameObject.Find("ImageQuestionMark2");
        questionMark2 = mark2.GetComponent<Image>();

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
        RefreshColor();
        RefreshPage();
    }

    void InitUnlockState()
    {
        for (int i = 0; i < unlockState.Length; i++)
        {
            unlockState[i] = false;
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
        RefreshColor();
        RefreshPage();
    }

    void RefreshColor()
    {
        Color unselectedColor;
        if (ColorUtility.TryParseHtmlString(unselectedColorStr, out unselectedColor))
        foreach (var button in buttons)
        {
            Image img = button.GetComponent<Image>();
            img.color = unselectedColor;
        }

        Color selectedColor;
        if (ColorUtility.TryParseHtmlString(selectedColorStr, out selectedColor))
        {
            Image img = buttons[selectedButtonNum].GetComponent<Image>();
            img.color = selectedColor;
        }
    }

    string GetLabelStr(int num)
    {
        string str = num.ToString();
        return str.Length == 1 ? "0" + str : str;
    }

    void RefreshText(int num)
    {
        if (unlockState[num] == true)
        {
            stories[num].enabled = true;
        }
        else
        {
            if (num % 2 == 0)
            {
                questionMark1.enabled = true;
            }
            else
            {
                questionMark2.enabled = true;
            }
        }
    }

    void RefreshPage()
    {
        int no1 = selectedButtonNum * 2;
        int no2 = selectedButtonNum * 2 + 1;

        label1.text = GetLabelStr(no1 + 1);
        label2.text = GetLabelStr(no2 + 1);

        questionMark1.enabled = false;
        questionMark2.enabled = false;

        for (int i = 0; i < stories.Length; i++)
        {
            stories[i].enabled = false;
        }

        RefreshText(no1);
        RefreshText(no2);
    }
}
