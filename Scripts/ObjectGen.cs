using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
class TreeData
{
    public GameObject treeMesh;
    public float minHeight;
    public float maxHeight;

}

[System.Serializable]
class RockData
{
    public GameObject treeMesh;
    public float minHeight;
    public float maxHeight;

}


public class ObjectGen : MonoBehaviour
{

    [SerializeField]
    private List<TreeData> treeD;

    [SerializeField]
    private List<RockData> rockD;


    private TerrainData tData;

    private int treeCap = 200 ;


    private int treeSpace = 15;

   
    private float randX = 5.0f;

    private float randZ = 5.0f;


    private int LayerIndex = 8;


    [SerializeField]
    private GameObject water;



    [SerializeField]
    private GameObject storm;

    [SerializeField]
    private GameObject rain;




    void Start()
    {
        tData = Terrain.activeTerrain.terrainData;
        MakeTree();
        AddWater();
        AddStorm(); //855-1500x -330 700z 947y
        Rain();



    }

    void MakeTree()
    {

        TreePrototype[] Protrees = new TreePrototype[treeD.Count];

        for (int i = 0; i < treeD.Count; i++)
        {
            Protrees[i] = new TreePrototype();
            Protrees[i].prefab = treeD[i].treeMesh;
        }

        tData.treePrototypes = Protrees;

        List<TreeInstance> treeInst = new List<TreeInstance>();

       
            for (int z = 0; z < tData.size.z; z += treeSpace)
            {
                for (int x = 0; x < tData.size.x; x += treeSpace)
                {
                    for (int treePrototypeIndex = 0; treePrototypeIndex < Protrees.Length; treePrototypeIndex++)
                    {
                        if (treeInst.Count < treeCap)
                        {
                            float cHeight = tData.GetHeight(x, z) / tData.size.y;

                            if (cHeight >= treeD[treePrototypeIndex].minHeight && cHeight <= treeD[treePrototypeIndex].maxHeight)
                            {
                                float RandomX = (x + Random.Range(-randX, randX)) / tData.size.x;
                                float RandomZ = (z + Random.Range(-randZ, randZ)) / tData.size.z;

                                TreeInstance tInstance = new TreeInstance();

                                tInstance.position = new Vector3(RandomX, cHeight, RandomZ);

                                Vector3 treePosition = new Vector3(RandomX * tData.size.x, cHeight * tData.size.y, RandomZ * tData.size.z) + this.transform.position;




                                RaycastHit raycastHit;

                                int layerMask = 1 << LayerIndex;

                                if (Physics.Raycast(treePosition, Vector3.down, out raycastHit, 100, layerMask) || Physics.Raycast(treePosition, Vector3.up, out raycastHit, 100, layerMask))
                                {

                                    float treeHeight = (raycastHit.point.y - this.transform.position.y) / tData.size.y;


                                    tInstance.position = new Vector3(tInstance.position.x, treeHeight, tInstance.position.z);
                                    tInstance.rotation = Random.Range(0, 360);
                                    tInstance.prototypeIndex = treePrototypeIndex;
                                    tInstance.color = Color.white;
                                    tInstance.lightmapColor = Color.white;
                                    tInstance.heightScale = 0.95f;
                                    tInstance.widthScale = 0.95f;
                               
                                    treeInst.Add(tInstance);


                                }


                            }

                        }
                    }
                }
            }
        

        tData.treeInstances = treeInst.ToArray();
    }

    void AddWater()
    {
        GameObject waterGameObject = GameObject.Find("water");

        if (!waterGameObject)
        {
            waterGameObject = Instantiate(water, new Vector3(622, 474, 702), this.transform.rotation);
            waterGameObject.name = "water";
        }



        waterGameObject.transform.localScale = new Vector3(800, 366, 800);



    }


    void AddStorm()
    {

        GameObject StormGameObject = GameObject.Find("storm");


        //855-1500x -330 700z 947y

        if (!StormGameObject)
        {
            int ranx = Random.Range(855, -1500);
            StormGameObject = Instantiate(storm, new Vector3(ranx, 700, 283), this.transform.rotation);
            StormGameObject.name = "storm";

        }

       
    

        StormGameObject.transform.localScale = new Vector3(5, 1, 5);


    }


    void Rain()
    {


     Instantiate(rain, this.transform.position, this.transform.rotation);
        
    }



    /*

      void MakeRock()
      {

          DetailPrototype[] Prorocks = new DetailPrototype[rockD.Count];

          for (int i = 0; i < rockD.Count; i++)
          {
              Prorocks[i] = new DetailPrototype();
              Prorocks[i].prototype = rockD[i].treeMesh;
          }

          tData.detailPrototypes = Prorocks;

          List<DetailPrototype> rockInst = new List<DetailPrototype>();


          for (int z = 0; z < tData.size.z; z += treeSpace)
          {
              for (int x = 0; x < tData.size.x; x += treeSpace)
              {
                  for (int treePrototypeIndex = 0; treePrototypeIndex < Prorocks.Length; treePrototypeIndex++)
                  {
                      if (rockInst.Count < treeCap)
                      {
                          float cHeight = tData.GetHeight(x, z) / tData.size.y;

                          if (cHeight >= rockD[treePrototypeIndex].minHeight && cHeight <= rockD[treePrototypeIndex].maxHeight)
                          {
                              float RandomX = (x + Random.Range(-randX, randX)) / tData.size.x;
                              float RandomZ = (z + Random.Range(-randZ, randZ)) / tData.size.z;

                              DetailPrototype tInstance = new DetailPrototype();

                              tInstance.prototype.transform.position = new Vector3(RandomX, cHeight, RandomZ);

                              Vector3 treePosition = new Vector3(RandomX * tData.size.x, cHeight * tData.size.y, RandomZ * tData.size.z) + this.transform.position;


                              RaycastHit raycastHit;

                              int layerMask = 1 << LayerIndex;

                              if (Physics.Raycast(treePosition, Vector3.down, out raycastHit, 100, layerMask) || Physics.Raycast(treePosition, Vector3.up, out raycastHit, 100, layerMask))
                              {

                                  float treeHeight = (raycastHit.point.y - this.transform.position.y) / tData.size.y;


                                  tInstance.prototype.transform.position = new Vector3(tInstance.prototype.transform.position.x, treeHeight, tInstance.prototype.transform.position.z);
                                  rockInst.Add(tInstance);


                              }


                          }

                      }
                  }
              }
          }


          tData.detailPrototypes = rockInst.ToArray();
      }

      */



}
