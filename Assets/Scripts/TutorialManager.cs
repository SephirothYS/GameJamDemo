using UnityEngine;
using TbsFramework.HOMMExample;
using TbsFramework;

public class TutorialManager : MonoBehaviour
{
    public GameObject player;
    private void Start()
    {
        player = GameObject.FindWithTag("Player");
        Invoke("AutoClickPlayer", 0.1f); // �ӳ��Զ������ҽ�ɫ
    }

    private void AutoClickPlayer()
    {
        // �����������ҵ���Ĵ����߼�����������и����� PlayerClick()
        if (player != null)
        {
            // ������ĵ���߼���ģ�������������������¼�
            player.GetComponent<HOMMUnit>().OnMouseDown();
        }
    }

}