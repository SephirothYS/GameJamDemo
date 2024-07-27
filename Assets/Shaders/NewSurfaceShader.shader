Shader "Custom/SmoothFogDissolve"
{
    Properties
    {
        _MainTex("Texture", 2D) = "white" {}
        _DissolveTexture("Dissolve Texture", 2D) = "white" {}
        _DissolveAmount("Dissolve Amount", Range(0, 1)) = 0.985
        _EdgeColor("Edge Color", Color) = (1, 1, 1, 1)  // the color of the dissolve edge
        _EdgeWidth("Edge Width", Range(0, 0.1)) = 0.01
    }
        SubShader
        {
            Tags {"Queue" = "Transparent" "RenderType" = "Transparent"}
            LOD 100

            CGPROGRAM
            #pragma surface surf Lambert alpha

            sampler2D _MainTex;
            sampler2D _DissolveTexture;
            float _DissolveAmount;
            fixed4 _EdgeColor;
            float _EdgeWidth;

            struct Input
            {
                float2 uv_MainTex;
            };

            void surf(Input IN, inout SurfaceOutput o)
            {
                fixed4 c = tex2D(_MainTex, IN.uv_MainTex);  // get the sprite texture
                fixed4 dissolveVal = tex2D(_DissolveTexture, IN.uv_MainTex);

                float dissolveThreshold = dissolveVal.r - _DissolveAmount;
                float alpha = smoothstep(0, _EdgeWidth, dissolveThreshold);

                o.Albedo = c.rgb;  // retain the original sprite texture color
                fixed4 edgeColor = _EdgeColor * alpha;  // calculate the dissolve edge color
                c.rgb = lerp(c.rgb, edgeColor.rgb, edgeColor.a);

                o.Albedo = c.rgb;  // output the final color
                o.Alpha = c.a * alpha;  // apply dissolve based on alpha
            }
            ENDCG
        }
            FallBack "Diffuse"
}