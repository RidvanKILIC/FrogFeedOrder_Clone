Shader "Custom/TongueShader"
{
    Properties
    {
        _MainTex("Texture", 2D) = "white" {}
        _StretchAmount("Stretch Amount", Range(0, 1)) = 0.0
    }

        SubShader
        {
            Tags { "RenderType" = "Opaque" }

            CGPROGRAM
            #pragma surface surf Lambert

            struct Input
            {
                float2 uv_MainTex;
            };

            sampler2D _MainTex;
            float _StretchAmount;

            void surf(Input IN, inout SurfaceOutput o)
            {
                // Stretch the UV coordinates based on _StretchAmount
                float2 stretchedUV = IN.uv_MainTex;
                stretchedUV.y *= 1.0 + _StretchAmount;

                // Sample the texture
                fixed4 c = tex2D(_MainTex, stretchedUV);

                // Output the final color
                o.Albedo = c.rgb;
                o.Alpha = c.a;
            }
            ENDCG
        }

            FallBack "Diffuse"
}
