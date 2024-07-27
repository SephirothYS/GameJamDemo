using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;
using UnityEngine.UI;
using UnityEngine.Rendering.PostProcessing;

public class InteractiveEvent : MonoBehaviour
{
    [Header("对话块名称")]
    public string blockName;

    private bool doOnce;
    private Flowchart flowchart;
    private bool canEnter;
    // Start is called before the first frame update
    void Start()
    {
        flowchart = GameObject.Find("Flowchart").GetComponent<Flowchart>();
        doOnce = false;
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
            if (flowchart.HasBlock(blockName))
            {
                StartCoroutine(StartEventAfterDelay(1.5f));
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
