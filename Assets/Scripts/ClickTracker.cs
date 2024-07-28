using UnityEngine;
using UnityEngine.EventSystems;

public class ClickTracker : MonoBehaviour, IPointerClickHandler
{
    private bool hasBeenClicked = false;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (hasBeenClicked)
        {
            // �����Ѿ�����������������״ε���¼�
            Debug.Log("Object already clicked before.");
        }
        else
        {
            // �����״α����
            hasBeenClicked = true;
            OnFirstClick();
        }
    }

    private void OnFirstClick()
    {
        // ��������״α����ʱ���߼�
        Debug.Log("Object clicked for the first time.");

        // ���������������Զ����߼��������������
        DoSomethingOnFirstClick();
    }

    private void DoSomethingOnFirstClick()
    {
        // �Զ��崦���߼������磺�����ض��¼���
        Debug.Log("Handling the first click event.");
    }
}