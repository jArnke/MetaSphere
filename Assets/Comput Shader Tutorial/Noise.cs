using System.Collections;
using UnityEngine;

public static class Noise 
{
    
    /*
     * Fill a 2D array of with perlin noise:
     * From Sebastian Lague
     */
    public static float[,] GenerateNoiseMap(int width, int height, float scale)
    {
        float[,] NoiseMap = new float[width, height];

        if (scale <= 0)
        {
            scale = 0.0001f;
        }
        
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < height; x++)
            {
                float sampleX = x / scale;
                float sampleY = y / scale;

                NoiseMap[y, x] = Mathf.PerlinNoise(x, y);
            }
        }
        return NoiseMap;
    }
    
    
}
