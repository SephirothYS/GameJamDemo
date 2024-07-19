using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

public class NpcEntity : MonoBehaviour
{
    [Header("npc名字，需要与Block名字一致")]
    public string npcName;

    private bool speakOnce;
    private Flowchart flowchart;
    private bool canSay;
    // Start is called before the first frame update
    void Start()
    {
        flowchart = GameObject.Find("Flowchart").GetComponent<Flowchart>();
        speakOnce = false;
    }

    // Update is called once per frame
    void Update()
    {
        Say();
    }

    void Say()
    {
        if (speakOnce) return;
        if (canSay)
        {
            if(flowchart.HasBlock(npcName))
            {
                flowchart.ExecuteBlock(npcName);
                speakOnce = true;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag.Equals("Player"))
        {
            canSay = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag.Equals("Player"))
        {
            canSay = false;
        }
    }
}
