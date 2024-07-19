Shader "Custom/SmoothFogDissolve"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _DissolveTexture ("Dissolve Texture", 2D) = "white" {}
        _DissolveAmount ("Dissolve Amount", Range(0, 1)) = 0.985
        _FogColor ("Fog Color", Color) = (0.5, 0.5, 0.5, 0.5)
        _EdgeWidth ("Edge Width", Range(0, 0.1)) = 0.01
    }
    SubShader
    {
        Tags {"Queue"="Transparent" "RenderType"="Transparent"}
        LOD 100

        CGPROGRAM
        #pragma surface surf Lambert alpha

        sampler2D _MainTex;
        sampler2D _DissolveTexture;
        float _DissolveAmount;
        fixed4 _FogColor;
        float _EdgeWidth;

        struct Input
        {
            float2 uv_MainTex;
        };

        void surf (Input IN, inout SurfaceOutput o)
        {
            fixed4 c = tex2D (_MainTex, IN.uv_MainTex);
            fixed4 dissolveVal = tex2D(_DissolveTexture, IN.uv_MainTex);

            float dissolveThreshold = dissolveVal.r - _DissolveAmount;
            float alpha = smoothstep(0, _EdgeWidth, dissolveThreshold);

            o.Albedo = _FogColor.rgb;
            o.Alpha = _FogColor.a * alpha;
        }
        ENDCG
    }
    FallBack "Diffuse"
}