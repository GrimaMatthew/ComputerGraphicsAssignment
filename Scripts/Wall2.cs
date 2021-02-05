using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall2 : MonoBehaviour
{
    [SerializeField]
    private Vector3 size = Vector3.one;
    private List<Material> materialsList;

    
    private int meshSize = 2;




   
    Vector3 t2;
    Vector3 t3;


    Vector3 b2;
    Vector3 b3;


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


        t2 = new Vector3(-size.x, size.y, size.z);// Bottom right of the top square 
        t3 = new Vector3(size.x, size.y, size.z);//bottom left of the top square


        //Bottom Vertices(just a change in Y)

      
        b2 = new Vector3(-size.x, -size.y, size.z);// Bottom right of the bottom square 
        b3 = new Vector3(size.x, -size.y, size.z);//bottom left of the bottom square


    }


    private void meshSquare()
    {
        //initialise MeshFilter
        MeshFilter meshFilter = this.GetComponent<MeshFilter>();

        // intiialise MeshBuilder
        MeshBuilder meshBuilder = new MeshBuilder(meshSize);



        //Right-side Square
        meshBuilder.BuildTriangle(b2, t2, t3, 0);
        meshBuilder.BuildTriangle(b2, b3, t3, 0);


        //Right-side Square
        meshBuilder.BuildTriangle(b2, t3, t2, 1);
        meshBuilder.BuildTriangle(b2, t3, b3, 1);





        //set mesh filters mesh to the mesh generate from out Meshbuilder
        meshFilter.mesh = meshBuilder.CreateMesh();

    }
}