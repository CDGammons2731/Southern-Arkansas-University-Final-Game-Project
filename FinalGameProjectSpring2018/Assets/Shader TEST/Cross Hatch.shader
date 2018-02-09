// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'
Shader "Unlit/Morgan/Cross Hatch"{

	//Variables
	Properties{
		_MainTexture("Main Color (RGB)", 2D) = "white"{}
		_Color("Color", Color) = (1,1,1,1) 
	}

	SubShader{
		Pass{

			CGPROGRAM

			#pragma vertex vertexFunction
			#pragma fragment fragmentFunction

			#include "UnityCG.cginc"


			
			//Vertices
			//Normal
			//Color float 4 RGBA
			//uv
			struct appdata {
				float4 vertex :POSITION; //float4 = 4 values x,y,z,w
				float2 uv :TEXCOORD0; //float2 = 2 values x,y
				float3 normal : NORMAL;

			};
			
			struct v2f {
				float4 position : SV_POSITION;
				float2 uv : TEXCOORD0;
			};

			//More Properties. Same as above.
			float4 _Color;
			sampler2D _MainTexture;

			//Build our object
		    v2f vertexFunction(appdata IN) {

				v2f OUT;

				OUT.position = (UnityObjectToClipPos(IN.vertex) //UNITY_MATRIX_MVP
				OUT.uv = IN.uv; 

			return OUT;

			}

			//Color it in!
			fixed4 fragmentFunction(v2f IN) : SV_Target{

				//Sample main texture for color
				float4 textureColor = tex2D(_MainTexture, IN.uv);

				return textureColor * _Color;

			}
			
			

			//Vertex
			//Build The Object

			//Fragment
			//Color it in

			ENDCG
		}

	}


}