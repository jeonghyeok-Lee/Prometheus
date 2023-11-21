Shader "Custom/PointCloudShader"
{
    Properties
    {
        _Color ("Main Color", Color) = (1,1,1,1)
    }
    SubShader
    {
        Tags { "Queue" = "Overlay" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma exclude_renderers gles xbox360 ps3
            ENDCG
        }
    }
}

// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'
// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'
// Upgrade NOTE: replaced '_World2Object' with 'unity_WorldToObject'
// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MV,*)' with 'UnityObjectToViewPos(*)'

// Upgrade NOTE: removed normalize() and length() on UNITY_TRANSFER_* inputs

// Upgrade NOTE: replaced tex2D with SAMPLE_TEXTURE2D

// Upgrade NOTE: removed UNITY_MATRIX_MV from UNITY_TRANSFER_INPUT

// Upgrade NOTE: removed UNITY_MATRIX_P from UNITY_TRANSFER_INPUT

// Upgrade NOTE: removed UNITY_MATRIX_V from UNITY_TRANSFER_INPUT
