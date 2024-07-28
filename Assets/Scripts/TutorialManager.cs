using UnityEngine;
using TbsFramework.HOMMExample;
using TbsFramework;

public class TutorialManager : MonoBehaviour
{
    public GameObject player;
    public GameObject[] targetGrids; // 高亮的格子们
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
        Invoke("AutoClickPlayer", 1.0f); // 延迟自动点击玩家角色
    }

    private void AutoClickPlayer()
    {
        // 在这里调用玩家点击的处理逻辑，这里假设有个方法 PlayerClick()
        if (player != null)
        {
            // 根据你的点击逻辑来模拟点击，例如调用鼠标点击事件
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
        // 使用高亮处理，例如改变颜色或添加高亮效果
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
            // 正确的格子被点击，处理下一步
            currentStep++;
            if (currentStep < targetGrids.Length)
            {
                StartHighlightStep();
            }
            else
            {
                // 引导完成
                //dialogBox.SetActive(false);
            }
        }
        else
        {
            // 点击无效或提示
            Debug.Log("错误的点击！");
        }
    }

}