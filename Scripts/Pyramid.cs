using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]


public class Pyramid : MonoBehaviour
{

    [SerializeField]
    private float pyramidSize = 5f; // the height of the pyramid top to bottom 

    private static int subMeshSize = 4;
    Vector3 top; 
    Vector3 base0;
    Vector3 base2;
    Vector3 base1;

    MeshBuilder meshBuiler = new MeshBuilder(subMeshSize);


    // Update is called once per frame
    void Start()
    {
        MeshFilter meshFilter = this.GetComponent<MeshFilter>();
        AddPoint();
        MakeP();
        MaterialList();
        meshFilter.mesh = meshBuiler.CreateMesh();


        StartCoroutine(RotatePyri(180, Vector3.up)); // rotatin the pyriamd to show colours 


    }

    private void MaterialList() // adding Materials 
    {

        List<Material> materialsList = new List<Material>();


        for (int i = 0; i <= 4; i++)
        {
            Material randMaterial = new Material(Shader.Find("Specular"));
            randMaterial.color = UnityEngine.Random.ColorHSV();
            materialsList.Add(randMaterial);

        }

        MeshRenderer meshRenderer = this.GetComponent<MeshRenderer>();
        meshRenderer.materials = materialsList.ToArray();

    }

    private void AddPoint()
    {
        //Add point

        top = new Vector3(0, pyramidSize);//Top Point 0 is origin adding y only the size at the very top

        base0 = Quaternion.AngleAxis(0f, Vector3.up) * Vector3.forward * pyramidSize;// describes a rotation around a direction in this case rotating around the up 

        base1 = Quaternion.AngleAxis(240f, Vector3.up) * Vector3.forward * pyramidSize; // 120 degree backwards

        base2 = Quaternion.AngleAxis(120f, Vector3.up) * Vector3.forward * pyramidSize; // 120 degree backwards

    }

    private void MakeP()
    {
        
        //Build Triangle
        meshBuiler.BuildTriangle(base0, base1, base2, 0);
        meshBuiler.BuildTriangle(base1, base0, top, 1);
        meshBuiler.BuildTriangle(base2, top, base0, 2);
        meshBuiler.BuildTriangle(top, base2, base1, 3);

    }


    IEnumerator RotatePyri(float angle, Vector3 ax) // to see the different colours
    {
 
        float rotSpeed = angle / 4; // speed of rotation

        while (true)
        {
           
            Quaternion intialRot = transform.rotation; // saving  the starting rotation 

            float deltaAngle = 0;

          
            while (deltaAngle < angle)  // robject will contune to rotatate until the angle is reached 
            {
                deltaAngle += rotSpeed * Time.deltaTime;
                deltaAngle = Mathf.Min(deltaAngle, angle);

                transform.rotation = intialRot * Quaternion.AngleAxis(deltaAngle, ax);

                yield return null;
            }

          
            yield return new WaitForSeconds(0.5f);
        }
    }
}

