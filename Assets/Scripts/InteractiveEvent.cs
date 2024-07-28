using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;
using UnityEngine.UI;
using UnityEngine.Rendering.PostProcessing;
using TbsFramework.HOMMExample;
using UnityEngine.Events;
using UnityEngine.Video;

public class InteractiveEvent : MonoBehaviour
{
    [Header("对话块名称")]
    public string blockName;

    public bool isOxygenPoint;
    public Canvas videoCanvas;
    public RawImage rawImage;
    private bool doOnce;
    private Flowchart flowchart;
    private bool canEnter;
    private List<int> oxygenValues = new List<int> { 20, 25, 30, 35, 40, 45, 50 };
    private List<int> weights = new List<int> { 5, 10, 15, 20, 15, 10, 5 };
    private List<float> probabilities = new List<float> { 0.06f, 0.13f, 0.19f, 0.25f, 0.19f, 0.13f, 0.06f };
    private List<float> cumulativeProbabilities;
    public PostProcessVolume post;
    public bool isEnding = false;
    // Start is called before the first frame update
    void Start()
    {
        if (videoCanvas != null && rawImage != null)
        {
            videoCanvas.gameObject.SetActive(false);
            rawImage.gameObject.SetActive(false);
        }
        flowchart = GameObject.Find("Flowchart").GetComponent<Flowchart>();
        doOnce = false;
        // 计算累积分布函数 (CDF)
        cumulativeProbabilities = new List<float>();
        float cumulative = 0f;
        foreach (var prob in probabilities)
        {
            cumulative += prob;
            cumulativeProbabilities.Add(cumulative);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void EventStart()
    {
        if (doOnce) return;
        if (canEnter)
        {
            doOnce = true;
            if (flowchart.HasBlock(blockName))
            {
                StartCoroutine(StartEventAfterDelay(0f));
            }
            if (isOxygenPoint)
            {
                StartCoroutine(StartOxygenSupply(0f));
            }
            if (videoCanvas != null && rawImage != null)
            {
                StartVideoPlay();
            }
            if (isEnding)
            {
                EndingEventStart();
            }
        }
    }

    void doExecute()
    {
        flowchart.ExecuteBlock(blockName);
        doOnce = true;
        int number = int.Parse(blockName.Substring(5));
        DataManager.Instance.unlockNote(number - 1);    
    }

    private IEnumerator StartEventAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay); 

        doExecute(); // 调用实际事件开始逻辑
    }

    private IEnumerator StartOxygenSupply(float delay)
    {
        yield return new WaitForSeconds(delay);

        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            HOMMUnit curPlayerData = player.GetComponent<HOMMUnit>();
            if (curPlayerData != null)
            {
                int increase = CalculateOxygenIncrease();
                curPlayerData.Oxygen += increase;
                if (curPlayerData.Oxygen > 100) curPlayerData.Oxygen = 100;
                doOnce = true;
            }
        }

    }
    private int CalculateOxygenIncrease()
    {
        float rnd = Random.Range(0f, 1f);
        for (int i = 0; i < cumulativeProbabilities.Count; i++)
        {
            if (rnd <= cumulativeProbabilities[i])
            {
                return oxygenValues[i];
            }
        }
        return oxygenValues[oxygenValues.Count - 1];
    }

    public void StartVideoPlay()
    {
        if (post)
        {
            post.weight = 1;
        }
        videoCanvas.gameObject.SetActive(true);
        rawImage.gameObject.SetActive(true);
        VideoPlayer vPlayer = rawImage.GetComponent<VideoPlayer>();
        vPlayer.Play();
        vPlayer.loopPointReached += OnVideoEnd;
    }

    void OnVideoEnd(VideoPlayer vp)
    {
        videoCanvas.gameObject.SetActive(false);
        rawImage.gameObject.SetActive(false);
        if (post)
        {
            post.weight = 0;
        }
    }

    public void EndingEventStart()
    {
        Canvas eventCanvas = videoCanvas;
        GameObject endingImage = eventCanvas.transform.Find("EndingImage").gameObject;
        GameObject trueEndButton = eventCanvas.transform.Find("TrueEndButton").gameObject;
        GameObject fakeEndButton = eventCanvas.transform.Find("FakeEndButton").gameObject;
        eventCanvas.gameObject.SetActive(true);
        endingImage.gameObject.SetActive(true);
        trueEndButton.gameObject.SetActive(true);
        fakeEndButton.gameObject.SetActive(true);
        if(post) post.weight = 1;
        if (DataManager.Instance.explorePoint >= 60)
        {
            trueEndButton.transform.Find("lock").gameObject.GetComponent<Image>().gameObject.SetActive(false);
            trueEndButton.transform.Find("HiddenText").gameObject.GetComponent<Text>().text = "离开星球";
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (doOnce) return;
        if (other.tag.Equals("Player"))
        {
            GameObject character = GameObject.Find("Character");
            if (!character) return;
            GeneralButton.Instance.ShowButton(character.transform.position, this); // 显示按钮
            canEnter = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (doOnce) return;
        if (other.tag.Equals("Player"))
        {
            GeneralButton.Instance.HideButton(); // 隐藏按钮
            canEnter = false;
        }
    }
}
