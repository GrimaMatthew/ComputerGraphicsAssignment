using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MeshClassTerraine {


    public static float[,] MeshMap(int mWidth, int mHeight , float cal)
    {
        float[,] meMap = new float[mWidth, mHeight];

        if(cal <=0)
        {
            cal = 0.0001f; 
        }
        for (int y = 0; y < mHeight; y++)
        {
            for (int x = 0; x < mWidth; x++)
            {
                float Xcor = x / cal;
                float Ycor = y /cal;

                float pNum = Mathf.PerlinNoise(Xcor, Ycor);
                meMap[x, y] = pNum;

            }
        }

        return meMap;
    }
   
}
