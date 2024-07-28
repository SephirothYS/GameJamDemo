using UnityEngine;

public class GuidePanel : MonoBehaviour
{
    GuideController guideController;
    Canvas canvas;

    private void Start()
    {
        canvas = transform.GetComponentInParent<Canvas>();
        guideController = transform.GetComponent<GuideController>();
        //������Ĳ��������ĸ��������ĸ�������Ҫ���ο��������οյ����ͣ��ο����Ŷ���ǰ�ı������ο����Ŷ�����ʱ����
        GameObject obj = GameObject.Find("Forest_Cell (37)");
        RectTransform rct = obj.GetComponent<RectTransform>();
        guideController.Guide(canvas, rct, GuideType.Circle, 2, 0.5f);
    }
}