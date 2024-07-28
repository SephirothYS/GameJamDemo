using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GeneralEvent : MonoBehaviour
{
    public static GeneralEvent Instance;

    public Canvas GeneralEventCanvas;
    public Image seaImage;
    // Start is called before the first frame update
    void Start()
    {
        GeneralEventCanvas.gameObject.SetActive(false);
        seaImage.gameObject.SetActive(false);
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

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartEvent()
    {
        GeneralEventCanvas.gameObject.SetActive(true);
        seaImage.gameObject.SetActive(true);

        StartCoroutine(waitForEnd(1f));
    }

    private IEnumerator waitForEnd(float delay)
    {
        yield return new WaitForSeconds(delay);

        EndEvent();
    }
    public void EndEvent()
    {
        GeneralEventCanvas.gameObject.SetActive(false);
        seaImage.gameObject.SetActive(false);
    }

}
