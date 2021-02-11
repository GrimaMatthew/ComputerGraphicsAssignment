using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
class TreeData // the class which contains all of the variables for each tree
{
    public GameObject treeMesh; //mesh 
    public float minHeight; //height min
    public float maxHeight; //height max

}

[System.Serializable]
class RockData
{
    public GameObject rockMesh;
    public float minHeight;
    public float maxHeight;

}


public class ObjectGen : MonoBehaviour
{

    [SerializeField]
    private List<TreeData> treeD; // list of tree data 

    [SerializeField]
    private List<RockData> rockD;


    private TerrainData tData; // terain data reference 

    private int treeCap = 200 ; // how many trees are spawned 


    private int treeSpace = 15; // space between one tree and another

   
    private float randX = 5.0f; 

    private float randZ = 5.0f;


    private int LayerIndex = 8; // index which terrain is on 


    [SerializeField]
    private GameObject water; // to spawn water
     


    [SerializeField]
    private GameObject storm;// to spawn storm

    [SerializeField]
    private GameObject rain;// to spawn rain


    [SerializeField]
    private GameObject cloud;// to spawn clouds




    void Start()
    {
        tData = Terrain.activeTerrain.terrainData; // init terrain data 
        MakeTree(); 
        AddWater();
        AddStorm(); 
        Rain();
        Cloud();


    }

    void MakeTree()
    {

        TreePrototype[] Protrees = new TreePrototype[treeD.Count]; // Treeprotype stores different tree models which you can use to paint trees to your terrain 

        for (int i = 0; i < treeD.Count; i++)
        {
            Protrees[i] = new TreePrototype();
            Protrees[i].prefab = treeD[i].treeMesh;
        }

        tData.treePrototypes = Protrees;

        List<TreeInstance> treeInst = new List<TreeInstance>();  // List of our instacnes 

       
            for (int z = 0; z < tData.size.z; z += treeSpace) // i looped through our terraine the width and the length (x,z)
            {
                for (int x = 0; x < tData.size.x; x += treeSpace)
                {
                    for (int treePrototypeIndex = 0; treePrototypeIndex < Protrees.Length; treePrototypeIndex++) // for each tree prototype i checked its min  and max height 
                    {
                        if (treeInst.Count < treeCap)
                        {
                            float cHeight = tData.GetHeight(x, z) / tData.size.y; 

                            if (cHeight >= treeD[treePrototypeIndex].minHeight && cHeight <= treeD[treePrototypeIndex].maxHeight)// to check if it is the range of that particular height 
                        {
                                float RandomX = (x + Random.Range(-randX, randX)) / tData.size.x; // if it falls within the range i created an instance of that tree 
                                float RandomZ = (z + Random.Range(-randZ, randZ)) / tData.size.z;

                                TreeInstance tInstance = new TreeInstance();

                                tInstance.position = new Vector3(RandomX, cHeight, RandomZ); // i positioned the tree  + or - a random value which i generated in those coordinates

                                Vector3 treePosition = new Vector3(RandomX * tData.size.x, cHeight * tData.size.y, RandomZ * tData.size.z) + this.transform.position;




                                RaycastHit raycastHit; // for trees not to float and be on the ground i used raycast for each tree a raycast is casted from the tree and when the raycast hits the terraine the distance between the tree and terraine is measured

                                int layerMask = 1 << LayerIndex;

                                if (Physics.Raycast(treePosition, Vector3.down, out raycastHit, 100, layerMask) || Physics.Raycast(treePosition, Vector3.up, out raycastHit, 100, layerMask))
                                {

                                    float treeHeight = (raycastHit.point.y - this.transform.position.y) / tData.size.y; // treeheight the distance from the tree to the terraine i minused the y coordinate to get the exact y position


                                    tInstance.position = new Vector3(tInstance.position.x, treeHeight, tInstance.position.z); // we positioned each tree on our terrain 
                                    tInstance.rotation = Random.Range(0, 360);
                                    tInstance.prototypeIndex = treePrototypeIndex;
                                    tInstance.color = Color.white; // this is like opacity 
                                    tInstance.lightmapColor = Color.white;// this is like opacity 
                                    tInstance.heightScale = 0.95f; //height
                                    tInstance.widthScale = 0.95f; // width
                               
                                    treeInst.Add(tInstance); // we added it to the list


                                }


                            }

                        }
                    }
                }
            }
        

        tData.treeInstances = treeInst.ToArray(); // I then assigned the tree instances to our list 
    }

    void AddWater()
    {
        GameObject waterGO = GameObject.Find("water"); // looking for gameobject with name water

        if (!waterGO) // if null
        {
            waterGO = Instantiate(water, new Vector3(622, 474, 702), this.transform.rotation); // Instantiate  water at that particular position
            waterGO.name = "water"; // name it water 
        }



        waterGO.transform.localScale = new Vector3(800, 366, 800); // adjust the scale



    }


    void AddStorm()
    {

        GameObject StormGameObject = GameObject.Find("storm"); //looking for gameobject with name water


        //855-1500x -330 700z 947y

        if (!StormGameObject) // if null
        {
            int ranx = Random.Range(855, -1500); // random x location
            StormGameObject = Instantiate(storm, new Vector3(ranx, 700, 283), this.transform.rotation);// Instantiate storm at  position
            StormGameObject.name = "storm"; // name storm

        }

       
    

        StormGameObject.transform.localScale = new Vector3(5, 1, 5); // setting the scale of the storm


    }


    void Rain()
    {


     Instantiate(rain, this.transform.position, this.transform.rotation); // instintating rain 
        
    }


    void Cloud()
    {
        cloud = Instantiate(cloud, new Vector3(-2018,664, 13068), this.transform.rotation);// instintating clouds at position
        cloud.transform.localScale = new Vector3(100, 1, 100); // scalling the clouds to cover  all terrain 
    }



    /*

      void MakeRock() -- Had issues with creating an instance of the rocks
      {

          DetailPrototype[] Prorocks = new DetailPrototype[rockD.Count];

          for (int i = 0; i < rockD.Count; i++)
          {
              Prorocks[i] = new DetailPrototype();
              Prorocks[i].prototype = rockD[i].rockMesh;
          }

          tData.detailPrototypes = Prorocks;

          List<DetailPrototype> rockInst = new List<DetailPrototype>();


          for (int z = 0; z < tData.size.z; z += rockSpace)
          {
              for (int x = 0; x < tData.size.x; x += rockSpace)
              {
                  for (int rockPrototypeIndex = 0; rockPrototypeIndex < Prorocks.Length; rockPrototypeIndex++)
                  {
                      if (rockInst.Count < rockCap)
                      {
                          float cHeight = tData.GetHeight(x, z) / tData.size.y;

                          if (cHeight >= rockD[treePrototypeIndex].minHeight && cHeight <= rockD[rockPrototypeIndex].maxHeight)
                          {
                              float RandomX = (x + Random.Range(-randX, randX)) / tData.size.x;
                              float RandomZ = (z + Random.Range(-randZ, randZ)) / tData.size.z;

                              DetailPrototype rInstance = new DetailPrototype();

                              tInstance.prototype.transform.position = new Vector3(RandomX, cHeight, RandomZ); ---- Null Ref

                              Vector3 rockPosition = new Vector3(RandomX * tData.size.x, cHeight * tData.size.y, RandomZ * tData.size.z) + this.transform.position;


                              RaycastHit raycastHit;

                              int layerMask = 1 << LayerIndex;

                              if (Physics.Raycast(rockPosition, Vector3.down, out raycastHit, 100, layerMask) || Physics.Raycast(rockPosition, Vector3.up, out raycastHit, 100, layerMask))
                              {

                                  float rockHeight = (raycastHit.point.y - this.transform.position.y) / tData.size.y;


                                  rInstance.prototype.transform.position = new Vector3(tInstance.prototype.transform.position.x, rockHeight, tInstance.prototype.transform.position.z);
                                  rockInst.Add(rInstance);


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
