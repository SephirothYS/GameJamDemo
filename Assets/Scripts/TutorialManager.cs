using UnityEngine;
using TbsFramework.HOMMExample;
using TbsFramework;

public class TutorialManager : MonoBehaviour
{
    public GameObject player;
    private void Start()
    {
        player = GameObject.FindWithTag("Player");
        Invoke("AutoClickPlayer", 0.1f); // 延迟自动点击玩家角色
    }

    private void AutoClickPlayer()
    {
        // 在这里调用玩家点击的处理逻辑，这里假设有个方法 PlayerClick()
        if (player != null)
        {
            // 根据你的点击逻辑来模拟点击，例如调用鼠标点击事件
            player.GetComponent<HOMMUnit>().OnMouseDown();
        }
    }

}