Shader "DoubleSided"
{
    Properties
    {
        _Color ("Color", Color) = (.2,.5,1,.3)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _Glossiness ("Smoothness", Range(0,1)) = 0.5
        _Metallic ("Metallic", Range(0,1)) = 0.5
        _Reflection("Reflection Intensity", Range(0.1,10)) = 5.2
        _Saturation("Reflection Saturation", Range(0.6,2.2)) = 1.2
        _DotProduct("Rim effect", Range(-1,1)) = 0.044
        _Fresnel("Fresnel Coefficient", Range(-1,10)) = 5.0

    }
    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue"="Transparent" "IgnoreProjector" = "True"}
        

        Blend SrcAlpha OneMinusSrcAlpha
        LOD 200
        ZWrite Off

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Standard fullforwardshadows
        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0

        sampler2D _MainTex;
        float _ColorMult;
        float _Reflection;
        float _DotProduct;
        float _Saturation;
        float _Fresnel;
        float _Refraction;
        float _Reflectance;

        struct Input
        {
            float2 uv_MainTex;
           float3 viewDir;
             float3 worldNormal;
        };

        half _Glossiness;
        half _Metallic;
        fixed4 _Color;

        // Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
        // See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
        // #pragma instancing_options assumeuniformscaling
        UNITY_INSTANCING_BUFFER_START(Props)
            // put more per-instance properties here
        UNITY_INSTANCING_BUFFER_END(Props)


        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            // Albedo comes from a texture tinted by color
            fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
            o.Albedo = c.rgb;
            // Metallic and smoothness come from slider variables
            o.Metallic = _Metallic;
            o.Smoothness = _Glossiness;
            float border = 1 - (abs(dot(IN.viewDir, IN.worldNormal)));
            float alpha = (border * (1 - _DotProduct) + _DotProduct);
            o.Alpha = ((c.a * _Reflection) * alpha);

}
        ENDCG
    }
    FallBack "Diffuse"
}
