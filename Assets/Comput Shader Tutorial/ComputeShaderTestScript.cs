using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComputeShaderTestScript : MonoBehaviour
{
    public ComputeShader computeShader;
    public RenderTexture renderTexture;
    void Start()
    {
        renderTexture = new RenderTexture(800, 800, 100);
        
        //Open texture to be written to by shader
        renderTexture.enableRandomWrite = true;
        renderTexture.Create();
        
        //upload pointer to shader:
        computeShader.SetTexture(0, "Result", renderTexture);  
        
        computeShader.Dispatch(0, renderTexture.width/8, renderTexture.height/8, 1);
        

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
