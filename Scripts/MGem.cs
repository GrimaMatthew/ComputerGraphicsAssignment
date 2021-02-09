using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MGem : MonoBehaviour
{
    public int mHeight;
    public int mWidth;
    public float noiseScale;


    public void GenerateMap() {

        float[,] nMap = MeshClassTerraine.MeshMap(mHeight, mHeight, noiseScale);

    }
 
    
}
