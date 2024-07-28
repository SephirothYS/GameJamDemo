using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IconEvent : MonoBehaviour
{
    private bool needIcon;
    private GameObject curGameObject;
    // Start is called before the first frame update
    void Start()
    {
        needIcon = false;
        curGameObject = this.gameObject;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void IconDisapper()
    {
        if (needIcon)
        {
            SpriteRenderer SRenderer = curGameObject.GetComponent<SpriteRenderer>();
            if (SRenderer != null)
            {
                SRenderer.enabled = false;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Player"))
        {
            needIcon = true;
            GeneralButton.Instance.ConnectIcon(this);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag.Equals("Player"))
        {
            needIcon = false;
        }
    }
}
