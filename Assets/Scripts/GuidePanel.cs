using UnityEngine;

public class GuidePanel : MonoBehaviour
{
    GuideController guideController;
    Canvas canvas;

    private void Start()
    {
        canvas = transform.GetComponentInParent<Canvas>();
        guideController = transform.GetComponent<GuideController>();
        //这句代码的参数代表（哪个画布，哪个对象需要被镂空引导，镂空的类型，镂空缩放动画前的比例，镂空缩放动画的时长）
        GameObject obj = GameObject.Find("Forest_Cell (37)");
        RectTransform rct = obj.GetComponent<RectTransform>();
        guideController.Guide(canvas, rct, GuideType.Circle, 2, 0.5f);
    }
}