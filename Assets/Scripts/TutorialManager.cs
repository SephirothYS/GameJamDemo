using UnityEngine;
using TbsFramework.HOMMExample;
using TbsFramework;

public class TutorialManager : MonoBehaviour
{
    public GameObject player;
    public GameObject[] targetGrids; // �����ĸ�����
    //public GuidedInputHandler inputHandler;
    //public GameObject dialogBox;
    //public Material highlightMaterial;
    //public HighlightController highlightController;
    //public ClickFilter CF;

    private int currentStep = 0;

    public void StartGuide(RectTransform highlightArea)
    {
        //inputHandler.SetInteractiveArea(highlightArea);
    }

    public void EndGuidance()
    {
        //inputHandler.ClearInteractiveArea();
    }

    private void Start()
    {
        player = GameObject.FindWithTag("Player");
        Invoke("AutoClickPlayer", 1.0f); // �ӳ��Զ������ҽ�ɫ
    }

    private void AutoClickPlayer()
    {
        // �����������ҵ���Ĵ����߼�����������и����� PlayerClick()
        if (player != null)
        {
            // ������ĵ���߼���ģ�������������������¼�
            player.GetComponent<HOMMUnit>().OnMouseDown();
        }

        StartHighlightStep();
    }

    private void StartHighlightStep()
    {
        if (targetGrids.Length > 0)
        {
            //highlightController.SetHighlightTarget(targetGrids[currentStep].transform);
            HighlightGrid(targetGrids[currentStep]);
            //StartGuide(targetGrids[currentStep])
            //dialogBox.SetActive(true);
        }
    }

    private void HighlightGrid(GameObject grid)
    {
        // ʹ�ø�����������ı���ɫ����Ӹ���Ч��
        HOMMHex hex = grid.GetComponent<HOMMHex>();
        if (hex)
        {
            hex.MarkAsTutorial();
        }
    }

    public void OnGridClicked(GameObject clickedGrid)
    {
        if (clickedGrid == targetGrids[currentStep])
        {
            // ��ȷ�ĸ��ӱ������������һ��
            currentStep++;
            if (currentStep < targetGrids.Length)
            {
                StartHighlightStep();
            }
            else
            {
                // �������
                //dialogBox.SetActive(false);
            }
        }
        else
        {
            // �����Ч����ʾ
            Debug.Log("����ĵ����");
        }
    }

}