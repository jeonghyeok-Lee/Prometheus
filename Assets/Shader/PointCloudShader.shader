Shader "Custom/PointCloudShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" { }
    }

    SubShader
    {
        Tags { "Queue" = "Overlay" }

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma exclude_renderers gles xbox360 ps3
            ENDCG
        }
    }
    
    SubShader
    {
        Tags { "Queue" = "Overlay" }

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma exclude_renderers gles xbox360 ps3
            ENDCG
        }
        
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma exclude_renderers gles xbox360 ps3
            ENDCG
            CGPROGRAM
            #pragma fragment frag
            ENDCG
        }
    }
}

CGPROGRAM
#pragma target 3.0

#include "UnityCG.cginc"

struct appdata
{
    float4 vertex : POSITION;
    float4 color : COLOR;
};

struct v2f
{
    float4 pos : POSITION;
    float4 color : COLOR;
};

uniform float _LimitDepth;

v2f vert(appdata v)
{
    v2f o;
    o.pos = UnityObjectToClipPos(v.vertex);
    o.color = v.color;
    return o;
}

fixed4 frag(v2f i) : COLOR
{
    // 포인트의 깊이값을 사용하여 색상 계산
    float normalizedDepth = i.pos.z / _LimitDepth;
    fixed4 col = lerp(fixed4(0, 0, 1, 1), fixed4(1, 1, 0, 1), normalizedDepth);
    return col * i.color;
}
ENDCG
