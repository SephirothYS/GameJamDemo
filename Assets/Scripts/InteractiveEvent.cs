using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;
using UnityEngine.UI;
using UnityEngine.Rendering.PostProcessing;
using TbsFramework.HOMMExample;

public class InteractiveEvent : MonoBehaviour
{
    [Header("对话块名称")]
    public string blockName;

    public bool isOxygenPoint;
    private bool doOnce;
    private Flowchart flowchart;
    private bool canEnter;
    private List<int> oxygenValues = new List<int> { 20, 25, 30, 35, 40, 45, 50 };
    private List<int> weights = new List<int> { 5, 10, 15, 20, 15, 10, 5 };
    private List<float> probabilities = new List<float> { 0.06f, 0.13f, 0.19f, 0.25f, 0.19f, 0.13f, 0.06f };
    private List<float> cumulativeProbabilities;
    // Start is called before the first frame update
    void Start()
    {
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
        EventStart();
    }

    void EventStart()
    {
        if (doOnce) return;
        if (canEnter)
        {
            doOnce = true;
            if (flowchart.HasBlock(blockName))
            {
                StartCoroutine(StartEventAfterDelay(1.5f));
            }
            if (isOxygenPoint)
            {
                StartCoroutine(StartOxygenSupply(1.0f));
            }
        }
    }

    void doExecute()
    {
        flowchart.ExecuteBlock(blockName);
        doOnce = true;
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag.Equals("Player"))
        {
            canEnter = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag.Equals("Player"))
        {
            canEnter = false;
        }
    }
}
