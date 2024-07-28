Shader "Custom/BlurShadowWithTransparency"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _ShadowColor ("Shadow Color", Color) = (0,0,0,0.5)
        _ShadowOffset ("Shadow Offset", Vector) = (5, -5, 0, 0)
        _BlurSize ("Blur Size", Range(0, 1)) = 0.1
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue"="Overlay" }
        LOD 200

        Pass
        {
            Blend SrcAlpha OneMinusSrcAlpha
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata_t
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float4 pos : POSITION;
                float2 uv : TEXCOORD0;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float4 _ShadowColor;
            float4 _ShadowOffset;
            float _BlurSize;

            v2f vert (appdata_t v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            half4 frag (v2f i) : SV_Target
            {
                half4 color = tex2D(_MainTex, i.uv);
                if (color.a < 0.1)
                    discard;

                half4 shadow = _ShadowColor;

                // Blur shadow
                float2 uvOffset = _ShadowOffset.xy * _BlurSize;
                shadow *= tex2D(_MainTex, i.uv + uvOffset);
                shadow += tex2D(_MainTex, i.uv - uvOffset);
                shadow += tex2D(_MainTex, i.uv + float2(uvOffset.x, -uvOffset.y));
                shadow += tex2D(_MainTex, i.uv + float2(-uvOffset.x, uvOffset.y));
                shadow /= 4.0;

                // Combine shadow and main texture
                return color * (1 - shadow.a) + shadow;
            }
            ENDCG
        }
    }
    FallBack "Transparent/Cutout/VertexLit"
}
