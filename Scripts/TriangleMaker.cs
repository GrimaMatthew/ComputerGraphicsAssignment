﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]


public class TriangleMaker : MonoBehaviour
{
    [SerializeField]
    private Vector3 size = Vector3.one;

    private List<Material> materialList;

    // Update is called once per frame
    void Update()
    {
        //intialise mesh filter
        MeshFilter meshFilter = this.GetComponent<MeshFilter>();

        //initialise mesh builder
        MeshBuilder meshBuilder = new MeshBuilder(1);// 1 when we only have 1 strinagle submesh count


        //build our triangle
        Vector3 p0 = new Vector3(size.x, size.y,-size.z);
        Vector3 p1 = new Vector3(-size.x, size.y, -size.z);
        Vector3 p2 = new Vector3(-size.x,size.y, size.z);


        meshBuilder.BuildTriangle(p0, p1, p2, 0);



        //assigned mesh filter's mesh to the one generated by the mesh builder 
        meshFilter.mesh = meshBuilder.CreateMesh();

        //initialise our mesh render and assign the material list to the mesh rendere's material list
        MeshRenderer meshRender = this.GetComponent<MeshRenderer>();

        AddMaterials();
        meshRender.materials = materialList.ToArray();

        
    }

    private void AddMaterials()
    {
        Material greenMat = new Material(Shader.Find("Specular"));
        greenMat.color = Color.green;

        materialList = new List<Material>();
        materialList.Add(greenMat);
    }
}
