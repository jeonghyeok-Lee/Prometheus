Shader "Custom/PointCloudShader"
{
    Properties {
        _Color ("Main Color", Color) = (.5,.5,.5,1)
        _OutlineColor ("Outline Color", Color) = (1,0,0,1)
        _Outline ("Outline width", Range (0.002, 0.1)) = 0.005
        _PointSize ("Point Size", Range (0.1, 10.0)) = 1.0
        _MainTex ("Base (RGB)", 2D) = "white" { }
    }
    SubShader {
        Tags { "Queue"="Overlay" }
        LOD 100

        CGPROGRAM
        #pragma surface surf Lambert

        struct Input {
            float2 uv_MainTex;
        };

        sampler2D _MainTex;

        fixed4 _Color;
        fixed4 _OutlineColor;
        float _Outline;
        float _PointSize;

        void surf(Input IN, inout SurfaceOutput o) {
            fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
            o.Albedo = c.rgb;

            // Add outline
            fixed alpha = tex2D(_MainTex, IN.uv_MainTex).a;
            fixed4 outColor = (_Outline > 0) ? _OutlineColor : c;
            o.Alpha = lerp(alpha, outColor.a, _Outline);

            // Adjust point size using Material value
            float pointSize = _PointSize;
            o.Vertex = o.Vertex * pointSize;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
