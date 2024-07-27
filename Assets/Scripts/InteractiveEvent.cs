using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;
using UnityEngine.UI;

public class InteractiveEvent : MonoBehaviour
{
    [Header("¶Ô»°¿éÃû³Æ")]
    public string blockName;

    private bool doOnce;
    private Flowchart flowchart;
    private bool canEnter;
    public Image backGoundImage;
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
                backGoundImage.enabled = true;
                flowchart.ExecuteBlock(blockName);
                backGoundImage.enabled = false;
                doOnce = true;
            }
        }
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
