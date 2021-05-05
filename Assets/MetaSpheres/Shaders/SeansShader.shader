Shader "Unlit/MarchingCubes"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
            #pragma multi_compile_fog

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;

            uniform float sphereCount;
            uniform float spheres[500];
            
            v2f vert(appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);

                return o;
            }

            

           float dotThree(float3 v1, float3 v2)
            {
                return v1.x * v2.x + v1.y * v2.y + v1.z * v2.z;
            }


            float sumOfSpheres(float2 pos)
            {
                float sum = 0.0f;
                for (int i = 0; i < sphereCount*3; i+=3)
                {
                    sum += spheres[i+2] * spheres[i+2] / (pow(pos.x - spheres[i], 2.0) + pow(
                        pos.y - spheres[i+1], 2.0));
                }
                return sum;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                // sample the texture
                fixed4 col = fixed4(0,0,0,1);

                float2 pos = i.uv;
                pos *= 20;
                pos -= 10;

                col.rgb = step(1.0, sumOfSpheres(pos)); 
                
                return col;
            }
            ENDCG
        }
    }
}


