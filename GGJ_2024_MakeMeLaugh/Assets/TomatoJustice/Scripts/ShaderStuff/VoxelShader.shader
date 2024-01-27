CGPROGRAM
#pragma vertex vert
#pragma fragment frag
#pragma include "UnityCG.cginc" // Include UnityCG.cginc


Shader"Custom/VoxelShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _VoxelSize ("Voxel Size", Float) = 0.1
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
LOD100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
#include "UnityCG.cginc"

struct appdata
{
    float4 vertex : POSITION;
    float3 normal : NORMAL;
    float2 uv : TEXCOORD0;
};

struct v2f
{
    float3 normal : TEXCOORD0;
    float2 uv : TEXCOORD1;
    float4 vertex : SV_POSITION;
};

sampler2D _MainTex;
float4 _MainTex_ST;
float _VoxelSize;

v2f vert(appdata v)
{
    v2f o;
                // Snap vertices to a voxel grid
    float3 voxelPos = round(v.vertex.xyz / _VoxelSize) * _VoxelSize;
    o.vertex = UnityObjectToClipPos(float4(voxelPos, 1.0));

                // Pass the normal to the fragment shader
    o.normal = v.normal;
    o.uv = TRANSFORM_TEX(v.uv, _MainTex);
    return o;
}

fixed4 frag(v2f i) : SV_Target
{
                // Flat shading calculations
    float3 normal = normalize(i.normal);
    float3 lightDir = normalize(float3(0.5, 1, 0.5));
    float diff = max(dot(normal, lightDir), 0.0);

    fixed4 col = tex2D(_MainTex, i.uv) * diff;
    return col;
}
            ENDCG
        }
    }
}
