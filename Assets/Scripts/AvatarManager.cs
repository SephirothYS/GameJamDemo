using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TbsFramework.HOMMExample;
using Fungus;

public class AvatarManager : MonoBehaviour
{
    enum EState
    {
        HEALTHY,
        CHOKING,
        DYING
    }

    public float chokingThreshold = 75.0f;
    public float dyingThreshold = 25.0f;

    private Image imageHealthy;
    private Image imageChoking;
    private Image imageDying;
    private Image black;

    public HOMMUnit character;

    EState curState = EState.HEALTHY;

    public void SetAlpha(Image image, float alpha)
    {
        Color color = image.color;
        color.a = Mathf.Clamp01(alpha); // alpha值在0到1之间
        image.color = color;
    }

    // Start is called before the first frame update
    void Start()
    {
        imageHealthy = GameObject.Find("ImageHealthy").GetComponent<Image>();
        imageChoking = GameObject.Find("ImageChoking").GetComponent<Image>();
        imageDying = GameObject.Find("ImageDying").GetComponent<Image>();
        black = GameObject.Find("ImageBlackFace").GetComponent<Image>();
        ChangeToState(EState.HEALTHY);
    }

    void ChangeToState(EState state)
    {
        curState = state;

        if (curState == EState.HEALTHY)
        {
            RefreshState(1.0f, 0.0f, 0.0f);
        }
        else if (curState == EState.CHOKING)
        {
            RefreshState(0.0f, 1.0f, 0.0f);
        }
        else if (curState == EState.DYING)
        {
            RefreshState(0.0f, 0.0f, 1.0f);
        }
    }

    void RefreshState(float aHealthy, float aChoking, float aDying)
    {
        SetAlpha(imageHealthy, aHealthy);
        SetAlpha(imageChoking, aChoking);
        SetAlpha(imageDying, aDying);
    }

    // Update is called once per frame
    void Update()
    {
        if (character.Oxygen <= dyingThreshold)
        {
            ChangeToState(EState.DYING);
        }
        else if (character.Oxygen <= chokingThreshold)
        {
            ChangeToState(EState.CHOKING);
        }
        else
        {
            ChangeToState(EState.HEALTHY);
        }
    }

    public void RemoveBlackFace()
    {
        Color c = black.color;
        c.a = 0.0f;
        black.color = c;
    }
}
