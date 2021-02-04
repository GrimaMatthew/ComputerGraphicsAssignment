using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshBuilder
{

    private List<Vector3> verti = new List<Vector3>();// list of vertices - store our points in our mesh

    private List<int> indi = new List<int>(); // list of indices that point to the index location in our verticles  list to make up triangles

    private List<Vector3> normals = new List<Vector3>(); // this defines the direction of each vertex one  to one mapping with vertices

    private List<Vector2> uvs = new List<Vector2>(); // Store the coordinates of our uvs  wrapper is 2D

    private List<int>[] submeshIn = new List<int>[] { }; // an array of submesh indices


    public MeshBuilder(int submeshCount)
    {
        submeshIn = new List<int>[submeshCount];

        for (int i = 0; i < submeshCount; i++)
        {
            submeshIn[i] = new List<int>();
        }

    }

   

    public void BuildTriangle(Vector3 p0, Vector3 p1, Vector3 p2, int subMesh)
    {
        Vector3 normal = Vector3.Cross(p1 - p0, p2 - p0).normalized;
        BuildTriangle(p0, p1, p2, normal, subMesh);
    }


    public void BuildTriangle(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 normal, int subMesh)
    {
      
        for (int j=0; j<3; j++)
        {
            indi.Add(verti.Count+j);

        }

        for (int j = 0; j < 3; j++)
        {
            submeshIn[subMesh].Add(verti.Count+j);

        }


        // add each point  to our vertices list
        verti.Add(p0);
        verti.Add(p1);
        verti.Add(p2);

        // add normals to our normal list  one normal for each point
        for(int i =0; i<3; i++)
        {
            normals.Add(normal);
        }
       
        // add each UV coordinate to out UV List
        uvs.Add(new Vector2(0, 0));
        uvs.Add(new Vector2(0, 1));
        uvs.Add(new Vector2(1, 1));


    }

    public Mesh CreateMesh()
    {
        Mesh mesh = new Mesh();

        mesh.vertices = verti.ToArray();

        mesh.triangles = indi.ToArray();

        mesh.normals = normals.ToArray();

        mesh.uv = uvs.ToArray();

        mesh.subMeshCount = submeshIn.Length;

        for (int i = 0; i < submeshIn.Length; i++)
        {
            if (submeshIn[i].Count < 3)
            {
                mesh.SetTriangles(new int[3] { 0, 0, 0 }, i);
            }
            else
            {
                mesh.SetTriangles(submeshIn[i].ToArray(), i);
            }
        }


        return mesh;
    }



}
