using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{

    [SerializeField]
    private Vector3 size = Vector3.one;
    private List<Material> materialsList;

    [SerializeField]
    private int meshSize = 2;



    Vector3 t1;
    Vector3 t2;
    Vector3 b1;
    Vector3 b2;
   

    private void Start()
    {
        WallMaker();
        meshSquare();
        StartCoroutine(AddMat());
    }




    IEnumerator AddMat()
    {

        materialsList = new List<Material>();

        for (int i = 0; i <= 2; i++)
        {
            Material randMaterial = new Material(Shader.Find("Specular"));
            randMaterial.color = UnityEngine.Random.ColorHSV();
            materialsList.Add(randMaterial);

        }

        MeshRenderer meshRenderer = this.GetComponent<MeshRenderer>();
        meshRenderer.materials = materialsList.ToArray();

        yield return null;

    }





    private void WallMaker()
    {

        //TopVertices
     
        t1 = new Vector3(-size.x, size.y, -size.z); 
        t2 = new Vector3(-size.x, size.y, size.z);
      

        //Bottom Vertices
 
        b1 = new Vector3(-size.x, -size.y, -size.z); //bottom Right
        b2 = new Vector3(-size.x, -size.y, size.z);// Bottom right of the bottom square 
       

    }


    private void meshSquare()
    {
        //initialise MeshFilter
        MeshFilter meshFilter = this.GetComponent<MeshFilter>();

        // intiialise MeshBuilder
        MeshBuilder meshBuilder = new MeshBuilder(meshSize);



        //Wall
        meshBuilder.BuildTriangle(b1, t2, t1, 0);
        meshBuilder.BuildTriangle(b1, b2, t2, 0);

        meshBuilder.BuildTriangle(b1, t1, t2, 1);
        meshBuilder.BuildTriangle(b1, t2, b2, 1);




        //set mesh filters mesh to the mesh generate from out Meshbuilder
        meshFilter.mesh = meshBuilder.CreateMesh();

    }
}