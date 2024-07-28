using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TbsFramework.HOMMExample;
using UnityEngine.Rendering.PostProcessing;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance;
    public GameObject CanvasNote;
    private ButtonManager BM;
    public GameObject character;
    private HOMMUnit characterData;
    public int explorePoint = 0;
    public int exploreFactor = 1;
    public PostProcessVolume post;
    public Canvas lowOxygenCanvas;
    // Start is called before the first frame update
    void Start()
    {
        if (CanvasNote != null)
        {
            BM = CanvasNote.GetComponent<ButtonManager>();       
        }
        if (character != null)
        {
            characterData = character.GetComponent<HOMMUnit>();
        }
        post.weight = 0.85f;
        if (lowOxygenCanvas != null)
        {
            lowOxygenCanvas.gameObject.SetActive(false);
        }
    }
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

    public void unlockNote(int number)
    {
        if (number >= 0 && number < 10)
        { 
            BM.unlockState[number] = true;
        }
    }

    public void increaseExplorePoint(int value)
    {
        explorePoint += value * exploreFactor;
    }

    // Update is called once per frame
    void Update()
    {
        if (characterData.Oxygen < 30)
        {
            float curTime = 0f;
            float totalTime = 1.5f;
            float curWeight = post.weight;
            while (curTime < 1.5f) 
            {
                curTime += Time.deltaTime;
                post.weight = Mathf.Clamp01(curTime / totalTime) * (1f - curWeight) + curWeight;
            }
            if (lowOxygenCanvas)
            {
                lowOxygenCanvas.gameObject.SetActive(true);
            }
        }
        else 
        {
            while (post.weight > 0.85)
            { 
                post.weight -= Time.deltaTime;
            }
            if (lowOxygenCanvas)
            {
                lowOxygenCanvas.gameObject.SetActive(false);
            }
        }
    }
}
