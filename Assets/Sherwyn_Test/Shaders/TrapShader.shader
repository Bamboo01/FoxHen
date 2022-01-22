Shader "Unlit/TrapShader"
{
	Properties
	{
		[PerRendererData] _MainTex ("Sprite Texture", 2D) = "white" {}
		_Color ("Tint", Color) = (1,1,1,1)
		[MaterialToggle] PixelSnap ("Pixel snap", Float) = 0
	}

	SubShader
	{
		Tags
		{ 
			"Queue"="Transparent" 
			"IgnoreProjector"="True" 
			"RenderType"="Transparent" 
			"PreviewType"="Plane"
			"CanUseSpriteAtlas"="True"
		}

		Cull Off
		Lighting Off
		ZWrite Off
		//Blend One OneMinusSrcAlpha
        Blend SrcAlpha OneMinusSrcAlpha

		Pass
		{
		CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma multi_compile _ PIXELSNAP_ON
			#include "UnityCG.cginc"
            #define MaxPlayers 4
			
			struct appdata_t
			{
				float4 vertex   : POSITION;
				float4 color    : COLOR;
				float2 texcoord : TEXCOORD0;
			};

			struct v2f
			{
				float4 vertex   : SV_POSITION;
				fixed4 color    : COLOR;
				float2 texcoord  : TEXCOORD0;
                float4 worldPos : TEXCOORD1;
			};

			fixed4 _Color;
			float3 _CurrPosition;
            float _MinDistance = 0.0f;
            float _MaxDistance = 1.0f;
            uniform float3 _PlayerPositions[MaxPlayers];
            uniform float3 _PlayerPivotPositions[MaxPlayers];
            uniform int _NumPlayers = 0;


			v2f vert(appdata_t IN)
			{
				v2f OUT;
				OUT.vertex = UnityObjectToClipPos(IN.vertex);
				OUT.worldPos = mul(unity_ObjectToWorld, IN.vertex);
				OUT.texcoord = IN.texcoord;
				OUT.color = IN.color * _Color;
				#ifdef PIXELSNAP_ON
				OUT.vertex = UnityPixelSnap (OUT.vertex);
				#endif

				return OUT;
			}

			sampler2D _MainTex;
			sampler2D _AlphaTex;
			float _AlphaSplitEnabled;

			fixed4 SampleSpriteTexture (float2 uv)
			{
				fixed4 color = tex2D (_MainTex, uv);

#if UNITY_TEXTURE_ALPHASPLIT_ALLOWED
				if (_AlphaSplitEnabled)
					color.a = tex2D (_AlphaTex, uv).r;
#endif //UNITY_TEXTURE_ALPHASPLIT_ALLOWED

				return color;
			}

			fixed4 frag(v2f IN) : SV_Target
			{
				float4 c = tex2D(_MainTex, IN.texcoord);

                if (_NumPlayers > 0 && c.a > 0.05f)
                {
                    float lowestDist = 99999999.0f;
                    float4 closestPlayer;
                    for (int i = 0; i < _NumPlayers; i++)
                    {
                        float dist = length(IN.worldPos - _PlayerPositions[i]);
                        if (dist < lowestDist)
                            lowestDist = dist;
                    }
                    
                    c.a = lowestDist / (_MaxDistance - _MinDistance);
                    c.a = clamp(0, 1, c.a);
                    c.a = 1.0f - c.a;
                    if (c.a > 0.4f)
                        c.a = 1.0f;
                }

				c.rgb *= c.a;

				return c;
			}
		ENDCG
		}
	}
}
