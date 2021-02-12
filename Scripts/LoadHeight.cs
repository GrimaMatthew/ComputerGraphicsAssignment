using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable] // class that holds all of the variuables relvant to the terraine
class TerrainTextureData
{
    public Texture2D terrainTexture;
    public float minHeight;
    public float maxHeight;
    public Vector2 tileSize;
}


public class LoadHeight : MonoBehaviour {

private Terrain terrain;

private TerrainData tData; // terr data intial
     
[SerializeField]
private Texture2D heightMapImage; // height map image

[SerializeField]
private List<Texture2D> heightMapImages; // list of height map images

[SerializeField]
private List<TerrainTextureData> terrainTextureDataList; // creating a list of textures


[SerializeField]
private float Blend = 0.01f;  // to blend the terrains

[SerializeField]
private Vector3 heightMS = new Vector3(1.3f, 1.3f, 1); // heights for map 


[SerializeField]
private float noiseW = 0.001f; // noise levels for perlin

    [SerializeField]
private float noiseH = 0.001f; // noise levels for perlin



// Start is called before the first frame update
void Start()
{


    int randNum = Random.Range(0, 2);
    heightMapImage = heightMapImages[randNum];

    terrain = this.GetComponent<Terrain>();
    tData = Terrain.activeTerrain.terrainData;
    MakeHeightMap();
    TerraineTexture();

}



    void MakeHeightMap() 
    {

        float[,] htMap = tData.GetHeights(0, 0, tData.heightmapResolution, tData.heightmapResolution); // creating a two dimensional array getting the heigh from tdata 

        for (int w = 0; w < tData.heightmapResolution; w++) //going through the widht and the height 
        {
            for (int h = 0; h < tData.heightmapResolution; h++)
            {

                    htMap[w, h] = heightMapImage.GetPixel((int)(w * heightMS.x), (int)(h * heightMS.z)).grayscale * heightMS.y; // we are reading the pixels from the image and converting them
                    htMap[w, h] += Mathf.PerlinNoise(w * noiseW, h * noiseH); // adding perlin noise to the terraine

                }
        }

        tData.SetHeights(0, 0, htMap); // setting the height maps in the terrain data to  the one in the image

    }


    void TerraineTexture()
    {
        

        TerrainLayer[] tLayers = new TerrainLayer[terrainTextureDataList.Count]; // creating an array with of type terrainlayer

        for (int i = 0; i < terrainTextureDataList.Count; i++) // going through our list TextureDataList and adding them
        {
            
            {
                tLayers[i] = new TerrainLayer(); 
                tLayers[i].diffuseTexture = terrainTextureDataList[i].terrainTexture;
                tLayers[i].tileSize = terrainTextureDataList[i].tileSize;

            }

        }


        tData.terrainLayers = tLayers; // we set the terrainLayers to the tlayers array


        float[,] htMap = tData.GetHeights(0, 0, tData.heightmapResolution, tData.heightmapResolution); // 2 dimen array creating height map intial to tdata height map

        float[,,] alphaMap = new float[tData.alphamapWidth, tData.alphamapHeight, tData.alphamapLayers]; // 3 dimen array with alpha map 

        for (int h = 0; h < tData.alphamapHeight; h++) // going through the the height  and width of alpha
        {
            for (int w = 0; w < tData.alphamapWidth; w++)
            {

                float[] sMap = new float[tData.alphamapLayers]; // created  a splat map

                for (int i = 0; i < terrainTextureDataList.Count; i++)

                {
                    float minH = terrainTextureDataList[i].minHeight - Blend; // setting our terrains to the min and max height set plus adding the blending 
                    float maxH = terrainTextureDataList[i].maxHeight + Blend;

                    if (htMap[w, h] >= minH && htMap[w, h] <= maxH) 
                    {
                        sMap[i] = 1; 

                    }

                }

                NormaliseMap(sMap);

                for (int j = 0; j < terrainTextureDataList.Count; j++)
                {
                    alphaMap[w, h, j] = sMap[j];

                }

            }

        }

        tData.SetAlphamaps(0, 0, alphaMap);

    }

    void NormaliseMap(float[] sMap)
    {
        float tot = 0;
        for (int i = 0; i < sMap.Length; i++)
        {
            tot += sMap[i];
        }

        for (int i = 0; i < sMap.Length; i++)
        {
            sMap[i] = sMap[i] / tot;
        }
    }




}
