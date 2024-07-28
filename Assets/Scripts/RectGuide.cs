using UnityEngine;
using UnityEngine.UI;

public class RectGuide : GuideBase
{
    protected float width;//ïÎ¿Õ¿í
    protected float height;//ïÎ¿Õ¸ß

    float scalewidth;
    float scaleheight;
    public override void Guide(Canvas canvas, RectTransform target)
    {
        base.Guide(canvas, target);
        //¼ÆËã¿í¸ß
        width = (targetCorners[3].x - targetCorners[0].x) / 2;
        height = (targetCorners[1].y - targetCorners[0].y) / 2;
        material.SetFloat("_SliderX", width);
        material.SetFloat("_SliderY", height);
    }

    public override void Guide(Canvas canvas, RectTransform target, float scale, float time)
    {
        this.Guide(canvas, target);

        scalewidth = width * scale;
        scaleheight = height * scale;
        material.SetFloat("_SliderX", scalewidth);
        material.SetFloat("_SliderY", scaleheight);

        this.time = time;
        isScaling = true;
        timer = 0;

    }

    protected override void Update()
    {
        base.Update();
        if (isScaling)
        {
            this.material.SetFloat("_SliderX", Mathf.Lerp(scalewidth, width, timer));
            this.material.SetFloat("_SliderY", Mathf.Lerp(scaleheight, height, timer));
        }
    }
}