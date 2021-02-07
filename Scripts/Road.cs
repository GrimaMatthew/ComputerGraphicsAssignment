using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshCollider))]

public class Road : MonoBehaviour
{
    [SerializeField]
    private float radius = 30f; // this defines the radius of the path

    [SerializeField]
    private float segments = 300f;

    [SerializeField]
    private float lineWidth = 0.3f; // middle white line road marker

    [SerializeField]
    private float roadWidth = 8f; // width of the road on each side of the line

    [SerializeField]
    private float edgeWidth = 1f; // widht of our road barrier at the edge of our road

    [SerializeField]
    private float edgeHeight = 1f;

    [SerializeField]
    private static int submeshSize = 6;

    [SerializeField]
    private float wavyness = 5f;

    [SerializeField]
    private float waveScale = 0.1f;

    [SerializeField]
    private Vector2 waveOffset;

    [SerializeField]
    private Vector2 waveStep = new Vector2(0.01f, 0.01f);

    [SerializeField]
    private bool stripeCheck = true;


    MeshBuilder meshBuilder = new MeshBuilder(submeshSize);
    MeshFilter meshFilter;
    MeshCollider meshCollider;


    List<Vector3> points = new List<Vector3>();

    //[SerializeField]
    //private GameObject car;

    void Start()
    {
        meshFilter = this.GetComponent<MeshFilter>();

        meshCollider = this.GetComponent<MeshCollider>();

        PointMaker();


    }

    //3. This method will used to create the different segments for each segment we are going to draw the road marker 
    //   (i.e. white line in the middle), draw the road on each side of the line, draw the edges - all these are going 
    //   to be placed in different positions
    private void ExtrudeRoad(MeshBuilder meshBuilder, Vector3 pPrev, Vector3 pCurr, Vector3 pNext)
    {
        //Road Line
        Vector3 offset = Vector3.zero;
        Vector3 targetOffset = Vector3.forward * lineWidth;

        MakeRoadQuad(meshBuilder, pPrev, pCurr, pNext, offset, targetOffset, 0);

        //Road
        offset += targetOffset;
        targetOffset = Vector3.forward * roadWidth;

        MakeRoadQuad(meshBuilder, pPrev, pCurr, pNext, offset, targetOffset, 1);

 

        //Edge
        offset += targetOffset;
        targetOffset = Vector3.up * edgeHeight;

        MakeRoadQuad(meshBuilder, pPrev, pCurr, pNext, offset, targetOffset, 3);

        //Edge Top
        offset += targetOffset;
        targetOffset = Vector3.forward * edgeWidth;

        MakeRoadQuad(meshBuilder, pPrev, pCurr, pNext, offset, targetOffset, 3);

        //Edge
        offset += targetOffset;
        targetOffset = -Vector3.up * edgeHeight;

        MakeRoadQuad(meshBuilder, pPrev, pCurr, pNext, offset, targetOffset,4);

    }

    //4. Create each quad
    private void MakeRoadQuad(MeshBuilder meshBuilder, Vector3 pPrev, Vector3 p0, Vector3 p1,
                              Vector3 offset, Vector3 targetOffset, int submesh)
    {
        Vector3 forward = (p1 - p0).normalized;
        Vector3 forwardPrev = (p0 - pPrev).normalized;

        Quaternion lo = Quaternion.LookRotation(Vector3.Cross(forward, Vector3.up));
        Quaternion islo = Quaternion.LookRotation(Vector3.Cross(forwardPrev, Vector3.up));

        Vector3 tl = p0 + (islo * offset);
        Vector3 tr = p0 + (islo * (offset + targetOffset));
        Vector3 bl = p1 + (lo * offset);
        Vector3 br = p1 + (lo * (offset + targetOffset));


        meshBuilder.BuildTriangle(tl, tr, bl, submesh);
        meshBuilder.BuildTriangle(tr, br, bl, submesh);


        lo = Quaternion.LookRotation(Vector3.Cross(-forward, Vector3.up));
        islo = Quaternion.LookRotation(Vector3.Cross(-forwardPrev, Vector3.up));

        tl = p0 + (islo * offset);
        tr = p0 + (islo * (offset + targetOffset));
        bl = p1 + (lo * offset);
        br = p1 + (lo * (offset + targetOffset));



        meshBuilder.BuildTriangle(bl, br, tl, submesh);
        meshBuilder.BuildTriangle(br, tr, tl, submesh);


    }


    private void PointMaker()
    {


        //1. Divide the circular race track into segments denoted in degrees and each point is defined by each segment
        //   Create the points and store them in a list
        float segmentDegrees = 360f / segments;


        for (float degrees = 0; degrees < 360f; degrees += segmentDegrees)
        {
            Vector3 point = Quaternion.AngleAxis(degrees, Vector3.up) * Vector3.forward * radius;
            points.Add(point);
        }

        curve();

        //2. function to define the path - the path is defined by each segment
        for (int i = 1; i < points.Count + 1; i++)
        {
            Vector3 pPrev = points[i - 1];
            Vector3 pCurr = points[i % points.Count];
            Vector3 pNext = points[(i + 1) % points.Count];

            ExtrudeRoad(meshBuilder, pPrev, pCurr, pNext);
        }

        //car.transform.position = points[0];
        //car.transform.LookAt(points[1]);

        meshFilter.mesh = meshBuilder.CreateMesh();

        meshCollider.sharedMesh = meshFilter.mesh;

    }


    private void curve()
    {


        Vector2 wave = this.waveOffset; // wave method

        for (int i = 0; i < points.Count; i++)
        {
            wave += waveStep;

            Vector3 point = points[i];
            Vector3 centreDirection = point.normalized;

            float noise = Mathf.PerlinNoise(wave.x * waveScale, wave.y * waveScale);
            noise *= wavyness;

            float control = Mathf.PingPong(i, points.Count / 2f) / (points.Count / 2f);

            points[i] += centreDirection * noise * control;
        }
    }
}
