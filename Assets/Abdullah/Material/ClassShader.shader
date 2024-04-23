Shader "Unlit/ClassShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
		[HDR]_MovingTexture("MovingTexture", 2D) = "white" {}
        _multiply("Multiply Texture",Range(0, 1))=1

        [HDR]_Mask("Mask", 2D) = "white" {}
        _Speed("TillingSpeed", float) = 2
        _TextureSize("TillingSize", int) = 1
        _OffsetMask("OffsetMask",float)=0

        [HideInInspector]_TillingSpeed("", Vector) = (0,0,0,0)
        [HideInInspector]_Threshold("offset",float)=0.4
    }
    SubShader
    {
		Tags{ "RenderType" = "Transparent"  "Queue" = "Transparent+0" "IgnoreProjector" = "True"}
	Blend SrcAlpha OneMinusSrcAlpha
         
        Pass
        {
         
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

        uniform sampler2D _MainTexture;
		uniform sampler2D _MovingTexture;
		uniform float2 _TillingSpeed;
        uniform int _TextureSize;
		uniform float _Speed;
		uniform sampler2D _Mask;
        uniform float _OffsetMask;

		uniform float _FadeTexture;
        sampler2D _MainTex;
        float4 _MainTex_ST;
        float _Threshold;
        float  _multiply;


            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                float2 uv2 : TEXCOORD0;
            };
            struct v2f
            {
                float2 uv : TEXCOORD0;
                float2 uv2 : TEXCOORD1;

                float4 vertex : SV_POSITION;
            };

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.uv2 = TRANSFORM_TEX(v.uv2, _MainTex);

                float2 uv_TexCoordMovingTexture= v.uv2*float2(_TextureSize,1);
                //tilling
                uv_TexCoordMovingTexture.x= _Time.y*_Speed+uv_TexCoordMovingTexture.x;
                //offseting

                float2 panner = (uv_TexCoordMovingTexture);
                o.uv2= panner;

                return o;
            }

            float4 frag (v2f i) : SV_Target
            {

                // sample the texture
                fixed4 col = tex2D(_MainTex, i.uv);
                fixed4 col2 = tex2D(_MovingTexture, i.uv2);
                fixed4 blended;
                if (_multiply==1){
                                blended = col2.a > _Threshold ? col2*col : col;
                }
                else
                {
                                blended = col2.a > _Threshold ? col2 : col;
                }

                fixed4 mask = tex2D(_Mask, i.uv+float2(_OffsetMask,0));
                blended.a=(blended.a*mask.r)*3;
                return blended;
            }
            ENDCG
        }
    }
}
