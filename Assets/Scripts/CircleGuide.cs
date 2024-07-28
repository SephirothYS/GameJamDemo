using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CircleGuide : GuideBase
{
    private float r;//�οհ뾶
    private float scaleR;//�仯֮��İ뾶��С

    //�̳�GuideBase���࣬��д���Ļ�ȡĿ��λ��ͬʱ�޸İ뾶�ķ���
    public override void Guide(Canvas canvas, RectTransform target)
    {
        base.Guide(canvas, target);//�̳л��������ȡ���ĵ�ļ���
        //����뾶
        float width = (targetCorners[3].x - targetCorners[0].x) / 2;
        float height = (targetCorners[1].y - targetCorners[0].y) / 2;
        r = Mathf.Sqrt(width * width + height * height);
        this.material.SetFloat("_Slider", r);
    }
    //��д���ද����������ȡ�뾶ֵ���ﵽ����Ч��
    public override void Guide(Canvas canvas, RectTransform target, float scale, float time)
    {
        this.Guide(canvas, target);//��Ҫ���ĵ㣬����ֱ�ӵ�����һ������
        scaleR = r * scale;
        this.material.SetFloat("_Slider", scaleR);

        this.time = time;
        isScaling = true;
        timer = 0;
    }

    protected override void Update()
    {
        base.Update();
        if (isScaling)
        {
            this.material.SetFloat("_Slider", Mathf.Lerp(scaleR, r, timer));
        }
    }

}