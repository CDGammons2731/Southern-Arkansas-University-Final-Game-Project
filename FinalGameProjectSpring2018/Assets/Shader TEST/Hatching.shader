// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Hatching" 
{
	Properties 
	{
		
		ambient_color 	("Ambient",  Color) = (0.2, 0.2, 0.2)
			
		tile 	("Tile Factor", Float) = 1
		
				
		color_map 				("color_map", 2D	) = "white" {}
		
		Hatch0("hatch0", 2D	) = "white" {}
		Hatch1 	("hatch1", 2D	) = "white" {}
		Hatch2 	("hatch2", 2D	) = "white" {}
		Hatch3 	("hatch3", 2D	) = "white" {}
		Hatch4 	("hatch4", 2D	) = "white" {}
		Hatch5 	("hatch5", 2D	) = "white" {}
				
		lightpos ("PointLight", Vector) = (-10, 10, -10)
	}
	
	SubShader 	
	{ 
		Pass
		{
		Cull Back
		AlphaTest GEqual 16
			
CGPROGRAM //-----------
// Upgrade NOTE: excluded shader from Xbox360 because it uses wrong array syntax (type[size] name)
#pragma exclude_renderers xbox360
#pragma target 3.0
#pragma vertex 		vertex_shader

#pragma fragment pixel_shader_relaxedcone 
	
#pragma profileoption MaxTexInections=64
#include "UnityCG.cginc"

#define DEPTH_BIAS
#define BORDER_CLAMP


float tile;

sampler2D color_map;
float3 lightpos;
sampler2D Hatch0;
sampler2D Hatch1;
sampler2D Hatch2;
sampler2D Hatch3;
sampler2D Hatch4;
sampler2D Hatch5;

struct a2v 
{
	float4 vertex 		: POSITION;
	float4 tangent		: TANGENT; 
	float3 normal		: NORMAL; 
	float2 texCoord		: TEXCOORD0; 
};

struct v2f
{
	float4 hpos				: POSITION;
	float3 eye				: TEXCOORD0;
	float3 light			: TEXCOORD1;
	float2 texcoord 		: TEXCOORD2;
	float3 HatchWeights0	: TEXCOORD3;
	float3 HatchWeights1	: TEXCOORD4;
};

v2f vertex_shader(a2v IN, out float4 someValue:TEXCOORD3) 
{ 
	v2f OUT;
	
	// vertex position IN object space
	float4 pos = float4(IN.vertex.xyz, 1.0);
	
	// vertex position IN clip space
	OUT.hpos 		= UnityObjectToClipPos(pos);
	
	// copy color and texture coordINates
	OUT.texcoord 	= IN.texCoord.xy*tile;

	// compute modelview rotation only part
	float3x3 modelviewrot	=float3x3(UNITY_MATRIX_MV);
	float4x4 projectionMatrix = float4x4(UNITY_MATRIX_MVP);

	// tangent vectors IN view space	
	float3 IN_bINormal 		= cross( IN.normal, IN.tangent.xyz )*IN.tangent.w;	
	float3 tangent			= mul(modelviewrot,IN.tangent.xyz);
	float3 bINormal			= mul(modelviewrot,IN_bINormal.xyz);
	float3 normal			= mul(modelviewrot,IN.normal);
	float3x3 tangentspace	= float3x3(tangent,bINormal,normal);
	
	//yfs
	
	lightpos.x = 0 ; // cos(_Time.z)*30;  
	lightpos.z = 10; //sin(_Time.z)*30; 
	lightpos.xz = 100* normalize(lightpos.xz);
	lightpos.y = 500;	 
	
	
	// vertex position IN view space (with model transformations)
	float3 vpos=mul(UNITY_MATRIX_MV,pos).xyz; 
		
	// view and light IN tangent space
	OUT.eye=mul(tangentspace,vpos);
	
	float3 normalW = -normalize(pos);
	
	float3 lightDir = normalize(lightpos - IN.vertex.xyz);
	float diffuse = min(1.0,max(0,dot(-lightDir, normalW)));
	diffuse = diffuse * diffuse;
	
	//Calculate texture weights
	float hatchFactor = diffuse * 6.0;
	float3 weight0 = 0.0;
	float3 weight1 = 0.0;

	
	if(hatchFactor > 5.0) 
		{
			weight0.x = 1.0;
		}
		else if(hatchFactor>4.0)
		{
			weight0.x = 1.0 - (5.0 - hatchFactor);
			weight0.y = 1.0 - weight0.x;
		}
		else if(hatchFactor>3.0)
		{
			weight0.y = 1.0 - (4.0 - hatchFactor);
			weight0.z = 1.0 - weight0.y;
		}
		else if(hatchFactor>2.0)
		{
			weight0.z = 1.0 - (3.0 - hatchFactor);
			weight1.x = 1.0 - weight0.z;
		}
		else if(hatchFactor>1.0)
		{
			weight1.x = 1.0 - (2.0 - hatchFactor);
			weight1.y = 1.0 -weight1.x;
		}
		else if(hatchFactor>0.0)
		{
			weight1.y = 1.0 - (1.0 - hatchFactor);
			weight1.z = 1.0 - weight1.y;
		}
	OUT.HatchWeights0 = weight0;
	OUT.HatchWeights1 = weight1;
	

		
	return OUT; 
}



float4 weighths(
	sampler2D normal_map,
	float2 texcoord,
	v2f IN)
{
	
	// border clamp 
	float alpha=1;	
#ifdef BORDER_CLAMP 
	if (texcoord.x<0) 	 alpha=0;
	if (texcoord.y<0) 	 alpha=0;
	if (texcoord.x>tile) alpha=0;
	if (texcoord.y>tile) alpha=0;
#endif

	// compute final color
	float4 finalcolor;
	
	float2 TexCoord = IN.texcoord;
	
	float4 hatchTex0 = tex2D(Hatch0, TexCoord) * IN.HatchWeights0.x;
	float4 hatchTex1 = tex2D(Hatch1, TexCoord) * IN.HatchWeights0.y;
	float4 hatchTex2 = tex2D(Hatch2, TexCoord) * IN.HatchWeights0.z;
	float4 hatchTex3 = tex2D(Hatch3, TexCoord) * IN.HatchWeights1.x;
	float4 hatchTex4 = tex2D(Hatch4, TexCoord) * IN.HatchWeights1.y;
	float4 hatchTex5 = tex2D(Hatch5, TexCoord) * IN.HatchWeights1.z;
	float4 originalCol = tex2D(color_map, TexCoord);
	
	float4 hatchColor = hatchTex0 + 
						hatchTex1 + 
						hatchTex2 + 
						hatchTex3 + 
						hatchTex4 + 
						hatchTex5;
	 
	
	// finalcolor.xyz = hatchColor.xyz  ;
	//finalcolor = float4(hatchColor.xyz+att*diffuse_color*diff , 1);

	//finalcolor.w = 1.0;
	//finalcolor = originalCol; //*0.9 + float4(hatchColor.xyz, 1.0)*0.1;
	finalcolor = originalCol*0.7 + float4(hatchColor.xyz, 1.0)*0.3;
	return finalcolor;
}



float4 pixel_shader_relaxedcone(v2f IN):COLOR
{
	float3 p,v;	
	
	return weighths(color_map,p.xy, IN);
}



ENDCG //----------
		} // Pass 
	} // SubShader 
} // Shader
