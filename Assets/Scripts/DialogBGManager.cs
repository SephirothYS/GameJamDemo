using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Rendering.PostProcessing;

public class DialogBGManager : MonoBehaviour
{
    public Canvas dialogCanvas;
    public Image dialogBackground;
    public PostProcessVolume postProcessVolume;

    private void Start()
    {
        dialogCanvas.gameObject.SetActive(false);
        dialogBackground.gameObject.SetActive(false);
    }

    public void StartDialogue()
    {
        dialogCanvas.gameObject.SetActive(true);
        dialogBackground.gameObject.SetActive(true);
        postProcessVolume.weight = 1;
    }

    public void EndDialogue()
    {
        dialogCanvas.gameObject.SetActive(false);
        dialogBackground.gameObject.SetActive(false);
        postProcessVolume.weight = 0;
    }

    private void Update()
    {

    }
}