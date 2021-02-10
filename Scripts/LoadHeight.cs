using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
class TerrainTextureData
{
    public Texture2D terrainTexture;
    public float minHeight;
    public float maxHeight;
    public Vector2 tileSize;
}


public class LoadHeight : MonoBehaviour {

private Terrain terrain;

private TerrainData tData;

[SerializeField]
private Texture2D heightMapImage;

[SerializeField]
private List<Texture2D> heightMapImages;

[SerializeField]
private List<TerrainTextureData> terrainTextureDataList;


[SerializeField]
private float Blend = 0.01f;

[SerializeField]
private Vector3 heightMS = new Vector3(1.3f, 1.3f, 1);


[SerializeField]
private float noiseW = 0.001f;

[SerializeField]
private float noiseH = 0.001f;



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

        float[,] htMap = tData.GetHeights(0, 0, tData.heightmapResolution, tData.heightmapResolution);

        for (int w = 0; w < tData.heightmapResolution; w++)
        {
            for (int h = 0; h < tData.heightmapResolution; h++)
            {

                    htMap[w, h] = heightMapImage.GetPixel((int)(w * heightMS.x), (int)(h * heightMS.z)).grayscale * heightMS.y;
                    htMap[w, h] += Mathf.PerlinNoise(w * noiseW, h * noiseH);

                }
        }

        tData.SetHeights(0, 0, htMap);

    }


    void TerraineTexture()
    {
        

        TerrainLayer[] tLayers = new TerrainLayer[terrainTextureDataList.Count];

        for (int i = 0; i < terrainTextureDataList.Count; i++)
        {
            
            {
                tLayers[i] = new TerrainLayer();
                tLayers[i].diffuseTexture = terrainTextureDataList[i].terrainTexture;
                tLayers[i].tileSize = terrainTextureDataList[i].tileSize;

            }

        }


        tData.terrainLayers = tLayers;


        float[,] htMap = tData.GetHeights(0, 0, tData.heightmapResolution, tData.heightmapResolution);

        float[,,] alphaMap = new float[tData.alphamapWidth, tData.alphamapHeight, tData.alphamapLayers];

        for (int h = 0; h < tData.alphamapHeight; h++)
        {
            for (int w = 0; w < tData.alphamapWidth; w++)
            {

                float[] sMap = new float[tData.alphamapLayers];

                for (int i = 0; i < terrainTextureDataList.Count; i++)

                {
                    float minH = terrainTextureDataList[i].minHeight - Blend;
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
