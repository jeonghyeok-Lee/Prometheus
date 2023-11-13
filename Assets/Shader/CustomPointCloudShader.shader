Shader "Custom/CustomPointCloudShader"
{
    Properties
    {
        _Color ("Point Color", Color) = (1,1,1,1)
        _PointSize ("Point Size", Range (0.1, 1000))
    }

    SubShader
    {
        Tags { "Queue" = "Geometry+1" }
        LOD 100

        CGPROGRAM
        #pragma vertex vert
        #pragma exclude_renderers gles xbox360 ps3

        #include "UnityCG.cginc"

        struct appdata
        {
            float4 vertex : POSITION;
        };

        struct v2f
        {
            float4 pos : POSITION;
        };

        uniform float _PointSize;

        v2f vert(appdata v)
        {
            v2f o;
            o.pos = UnityObjectToClipPos(v.vertex);
            o.pos.xy += o.pos.z * (_PointSize * 0.5);
            return o;
        };

        fixed4 frag(v2f i) : COLOR
        {
            return fixed4(1, 1, 1, 1); // White color for each point
        };

    }
    ENDCG
    
}