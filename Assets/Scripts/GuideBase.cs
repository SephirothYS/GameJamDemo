using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class GuideBase : MonoBehaviour
{
    protected Material material;//����
    protected Vector3 center;//�ο�����
    protected RectTransform target;//��������Ŀ�����
    protected Vector3[] targetCorners = new Vector3[4];//����Ŀ��ı߽�

    protected float timer;//��ʱ�������ﵽ�������ٲ���
    protected float time;//���嶯��ʱ��
    protected bool isScaling;//�Ƿ���������
                             //�鷽�����������ȥ��д�����������ж϶����Ƿ񲥷ţ�������ţ��Ͱ��ռȶ���ʱ���������
    protected virtual void Update()
    {
        if (isScaling)
        {
            timer += Time.deltaTime * 1 / time;
            if (timer >= 1)
            {
                timer = 0;
                isScaling = false;
            }
        }
    }
    //����������ȡĿ��������ĸ������������ĵ㣬��Ϊ���ھ��λ���Բ��Ч����������Ե����ĵ���ȷ����
    public virtual void Guide(Canvas canvas, RectTransform target)
    {
        material = GetComponent<Image>().material;
        this.target = target;
        //��ȡ�ĸ������������
        target.GetWorldCorners(targetCorners);
        //��������ת��Ļ����
        for (int i = 0; i < targetCorners.Length; i++)
        {
            targetCorners[i] = WorldToScreenPoints(canvas, targetCorners[i]);
        }
        //�������ĵ�
        center.x = targetCorners[0].x + (targetCorners[3].x - targetCorners[0].x) / 2;
        center.y = targetCorners[0].y + (targetCorners[1].y - targetCorners[0].y) / 2;
        //�������ĵ�
        material.SetVector("_Center", center);
    }
    //Ϊ��������̳е�ʱ��ֱ����д�Ϳ��ԣ���Ϊ���κ�Բ�εĶ�����ʽ��һ������������߰뾶�й�
    public virtual void Guide(Canvas canvas, RectTransform target, float scale, float time)
    {

    }
    //�����ת��
    public Vector2 WorldToScreenPoints(Canvas canvas, Vector3 world)
    {
        //������ת��Ļ
        Vector2 screenPoint = RectTransformUtility.WorldToScreenPoint(canvas.worldCamera, world);
        Vector2 localPoint;
        //��Ļת�ֲ�����
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.GetComponent<RectTransform>(), screenPoint, canvas.worldCamera, out localPoint);
        return localPoint;
    }
}