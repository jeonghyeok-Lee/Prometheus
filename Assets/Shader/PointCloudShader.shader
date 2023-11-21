Shader "Custom/PointCloudShader"
{
    Properties
    {
        _Colors("Colors", Color[]) = {}
        _DepthIntensity("Depth Intensity", Range(0, 1)) = 1.0
    }

    SubShader
    {
        Tags { "Queue" = "Overlay" }
        LOD 100

        CGPROGRAM
        #pragma surface surf Lambert

        struct Input
        {
            float4 color : COLOR;
        };

        sampler2D _Colors;
        float _DepthIntensity;

        void surf(Input IN, inout SurfaceOutput o)
        {
            fixed4 c = tex2D(_Colors, IN.uv);
            c.rgb *= _DepthIntensity;
            o.Albedo = c.rgb;
        }
        ENDCG
    }
}
