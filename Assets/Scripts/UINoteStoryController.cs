using TMPro;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class NotebookManager : MonoBehaviour
{
    public Text storyText1; // 页面上第一条故事内容
    public Text storyText2; // 页面上第二条故事内容
    public Button[] pageButtons;

    public bool[] unlockState = new bool[10];


    private string[] stories = new string[]
    {
        "我见证了一个悲惨的场景，我曾经称之为家的地方，美丽的蓝星，现在已经破碎，成为了宇宙中的尘埃。\r\n铭记那个时刻，那轰然而至的冲击波、令人窒息的震动，以及那让人心碎的景象，地球在宇宙的无垠轮回中炸为四散的碎片。\r\n这一切都是几乎无法接受的现实。我刚刚抵达的外太空行星的经验和数据，甚至尚未完成回传，我的探索和努力似乎一下子化为无物，沉入了宇宙的无底深渊。",

        "我设定了一道尺度漫长的解冻序列，让我陷入一种休眠状态，让未知的时间流逝。期间的所有事件，我都将之交给了命运的洗礼，只是它们将在我这段长眠的历程中逐渐揭晓。\r\n以上，仅剩下的智慧和勇气都留给了这个庞大而深邃的宇宙，我想我需要休息一阵子了。",

        "我被草率地拉回意识，找不到我所处环境的任何参考坐标。\r\n那时，仪表的时间读数对我来说如同一团混乱，无法区分过去多少已失去意义的时间。\r\n然而，时间混沌中，我仍然意识到自我凭借着微弱的人格存在于这个偌大的宇宙之中。\r\n我依然是人类，肌肤和血液的触感让我明白，我并未迷失。\r\n这个认知意味着我或许已经背负着寻找和建立新的人类文明的巨大任务，旅程才刚刚开始，而不知所终。",

        "当我控制飞船在黑暗的宇宙中无尽游弋时，友好的星球突然进入了扫描范围。\r\n这是份罕见的好运，我被一颗丰饶且美丽的星球吸引。\r\n我的内心五味杂陈。\r\n这颗星球与我深爱的地球惊人地相似，吸引着我的注意力。\r\n随时随刻，我都在揣测着，是不是在那漆黑的星域中，我发生了错觉。",
    };

    void Start()
    {
        InitUnlockState();
        // 更新页面内容

    }

    void InitUnlockState()
    {
        for (int i = 0; i < unlockState.Length; i++)
        {
            unlockState[i] = false;
        }
    }
}
