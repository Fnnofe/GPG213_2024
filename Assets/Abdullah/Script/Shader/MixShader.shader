Shader "MixShader"
{
    Properties
    {
        _myColor ("Color", Color) = (1.0, 1.0, 1.0, 1.0)
        _AOColor ("Ambient Color", Color) = (1.0, 1.0, 1.0, 1.0)
        _RimColor ("Rim Color", Color) = (1.0, 1.0, 1.0, 1.0)
        _RimPower ("Rim Power", Range(0, 10)) = 1.0
    }
    SubShader
    {

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            uniform float4 _myColor;
            uniform float4 _AOColor;
            uniform float4 _RimColor;
            uniform float _RimPower;

            struct vertexInput
            {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
            };

            struct vertexOutput
            {
                float4 pos : SV_POSITION;
                float rim : TEXCOORD0; // Rim light value
                float3 normal : TEXCOORD1; // Pass normal to fragment shader
                float4 col : COLOR;

            };

            vertexOutput vert (vertexInput v)
            {
             vertexOutput o;

             //shading
            float3 normalDirection= normalize(mul(float4(v.normal,0.0),unity_WorldToObject).xyz);
            float3 lightDirection= normalize(_WorldSpaceLightPos0.xyz);
            float3 ambiantShadoow = UNITY_LIGHTMODEL_AMBIENT.xyz * _AOColor;
            
            float3 diffuseReflection = max(0.3, dot(normalDirection, lightDirection));

            
                o.col= float4(diffuseReflection* _myColor.rgb+ambiantShadoow.rgb*10,1.0);
                o.pos= UnityObjectToClipPos(v.vertex);
 
                //Rimlight                      view from camera           find the position of the object in space
                float3 viewDirection = normalize(_WorldSpaceCameraPos - mul(unity_ObjectToWorld, v.vertex).xyz);

                o.normal = v.normal;
                //                      gradiant effect 
                float rimLight = 1 - (dot(v.normal, viewDirection));

                //  
                o.rim = pow(rimLight, _RimPower);
                return o;
            }

            float4 frag (vertexOutput i) : SV_TARGET0
            {
                float3 rimLight = _RimColor.rgb * i.rim;
                float3 finalColor =  i.col + rimLight;
                return float4(finalColor.rgb, 1.0);
            }
            ENDCG
        }
    }
}
