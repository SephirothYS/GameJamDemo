using UnityEngine;
using UnityEngine.EventSystems;

public class ClickTracker : MonoBehaviour, IPointerClickHandler
{
    private bool hasBeenClicked = false;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (hasBeenClicked)
        {
            // 对象已经被点击过，不触发首次点击事件
            Debug.Log("Object already clicked before.");
        }
        else
        {
            // 对象首次被点击
            hasBeenClicked = true;
            OnFirstClick();
        }
    }

    private void OnFirstClick()
    {
        // 处理对象首次被点击时的逻辑
        Debug.Log("Object clicked for the first time.");

        // 你可以在这里添加自定义逻辑或调用其他方法
        DoSomethingOnFirstClick();
    }

    private void DoSomethingOnFirstClick()
    {
        // 自定义处理逻辑（例如：触发特定事件）
        Debug.Log("Handling the first click event.");
    }
}