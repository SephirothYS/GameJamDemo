using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.UI;

public class PropInfoManager : MonoBehaviour
{
    string[] descs = new string[3]
    {
        "弹簧鞋：让你走得更久更轻松！",
        "指南针：指明你前进的方向！",
        "勘测仪：帮你更顺利地认识星球！",
    };

    public PropManager propManager;
    int mainPropNum;

    private Image shoes;
    private Image compass;
    private Image kanceyi;
    private List<Image> list = new List<Image>();

    private Text propDesc;
    private Text taskDesc;

    // bool f = false;

    // Start is called before the first frame update
    void Start()
    {
        shoes = transform.Find("MainProps/ImageShoes").GetComponent<Image>();
        compass = transform.Find("MainProps/ImageCompass").GetComponent<Image>();
        kanceyi = transform.Find("MainProps/ImageKanceyi").GetComponent<Image>();
        list.Add(shoes);
        list.Add(compass);
        list.Add(kanceyi);

        propDesc = transform.Find("ImageTask/TextDesc").GetComponent<Text>();
        // taskDesc = transform.Find("ImageTask/TextTaskDesc").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        // if (f == true) return;
        mainPropNum = propManager.GetBeginPropNum();
        propDesc.text = descs[mainPropNum - 1];
        for (int i = 0; i < list.Count; i++)
        {
            if (i + 1 == mainPropNum)
            {
                list[i].enabled = true;
            }
            else
            {
                list[i].enabled = false;
            }
        }
    }
}
