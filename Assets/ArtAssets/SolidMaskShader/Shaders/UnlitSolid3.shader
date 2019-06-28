// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "UnlitSolid3" {
	SubShader{
		Tags { "RenderType" = "Opaque" }
		LOD 200

		Pass {
			Tags { "LightMode" = "Always" }

			Fog { Mode Off }
			ZWrite On
			ZTest LEqual
			Cull Back
			Lighting Off
				
			CGPROGRAM
				#pragma vertex vert
				#pragma fragment frag
				#pragma fragmentoption ARB_precision_hint_fastest
				//fixed4 UnlitSolid0;
				//fixed4 UnlitSolid1;
				//fixed4 UnlitSolid2;
				fixed4 UnlitSolid3;
				//fixed4 UnlitSolid4;
				//fixed4 UnlitSolid5;
				//fixed4 UnlitSolid6;
				//fixed4 UnlitSolid7;
				//fixed4 UnlitSolid8;
				//fixed4 UnlitSolid9;

				#include "UnityCG.cginc"
				struct appdata {
					float4 vertex : POSITION;
				};

				struct v2f {
					float4 vertex : POSITION;
				};

				v2f vert(appdata v) {
					v2f o;
					o.vertex = UnityObjectToClipPos(v.vertex);
					return o;
				}

				fixed4 frag(v2f i) : COLOR {
					return UnlitSolid3;
				}
			ENDCG

		}
	}
}