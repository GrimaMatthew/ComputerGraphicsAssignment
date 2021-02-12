using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))] // Getting mesh filter
[RequireComponent(typeof(MeshRenderer))] // Getting mesh renderer 
[RequireComponent(typeof(MeshCollider))]// Getting mesh collider

public class Road : MonoBehaviour
{
    [SerializeField]
    private float radius = 30f; // this defines the radius of the path

    [SerializeField]
    private float segments = 300f; // the number of extrusions we perform in this circle 

    [SerializeField]
    private static float lineWidth = 0.3f; // middle white line road marker

    [SerializeField]
    private float roadWidth = 8f; // width of the road how wide tarmac is 

    [SerializeField]
    private float edgeWidth = 1f; // widht of our road barrier at the edge of our road

    [SerializeField]
    private float edgeHeight = 1f; // height of barrier 

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
    private bool stripeCheck = true; // stripe the barrier 


    MeshBuilder meshBuilder = new MeshBuilder(submeshSize); // ref to mesh build
    MeshFilter meshFilter;
    MeshCollider meshCollider;


    List<Vector3> points = new List<Vector3>(); // points for our circle 


    Vector3 offset = Vector3.zero;
    Vector3 targetOffset = Vector3.forward * lineWidth;

    Vector3 carPos;

    GameObject cube;
    GameObject Wall;

    [SerializeField]
    private GameObject car; // ref to car 

    void Start()
    {
        meshFilter = this.GetComponent<MeshFilter>(); // ref to mesh filter

        meshCollider = this.GetComponent<MeshCollider>();// ref to mesh collider

        PointMaker();

        MaterialList();

        carPos = car.transform.position; // setting possition

        StartCoroutine(endP());

    }


   


    //3. This method will used to create the different segments for each segment we are going to draw the road marker 
    //   (i.e. white line in the middle), draw the road on each side of the line, draw the edges - all these are going 
    //   to be placed in different positions
    private void edges(MeshBuilder meshBuilder, Vector3 pPrev, Vector3 pCurr, Vector3 pNext)
    {

      



        int stripeSubmesh = Random.RandomRange(3,5); // the stripe mesch changes the colours at the barrier.



        
        //Edge innder
        offset += targetOffset;
        targetOffset = Vector3.up * edgeHeight;

        RoadQuad(meshBuilder, pPrev, pCurr, pNext, offset, targetOffset, stripeSubmesh);

        //Edge top
        offset += targetOffset;
        targetOffset = Vector3.forward * edgeWidth;

        RoadQuad(meshBuilder, pPrev, pCurr, pNext, offset, targetOffset, stripeSubmesh);

        //Edge outer 
        offset += targetOffset;
        targetOffset = -Vector3.up * edgeHeight;

        RoadQuad(meshBuilder, pPrev, pCurr, pNext, offset, targetOffset, stripeSubmesh);

    }

    //4. Create each quad
    private void RoadQuad(MeshBuilder meshBuilder, Vector3 pPrev, Vector3 current, Vector3 next,
                              Vector3 offset, Vector3 targetOffset, int submesh)
    {
        Vector3 forward = (next - current).normalized; // the direction the road 
        Vector3 forwardPrev = (current - pPrev).normalized; // setting up the previous to know where the road was previously so road isnt segmented 


        //This is for the outer road 
        Quaternion lo = Quaternion.LookRotation(Vector3.Cross(forward, Vector3.up)); // creating a rotation to the right or left of foward .cross allows us to get the thir direction between vectors will retunr the foward
        Quaternion islo = Quaternion.LookRotation(Vector3.Cross(forwardPrev, Vector3.up)); // previous point

        // target offset is the width while offset is the position from which the road extrud
        Vector3 tl = current + (islo * offset); 
        Vector3 tr = current + (islo * (offset + targetOffset));
        Vector3 bl = next + (lo * offset);
        Vector3 br = next + (lo * (offset + targetOffset));


        meshBuilder.BuildTriangle(tl, tr, bl, submesh); // building the track
        meshBuilder.BuildTriangle(tr, br, bl, submesh);


        //This is for the inner road 


        lo = Quaternion.LookRotation(Vector3.Cross(-forward, Vector3.up)); // inverting the foward direction to give us to opposite direction 
        islo = Quaternion.LookRotation(Vector3.Cross(-forwardPrev, Vector3.up));

        tl = current + (islo * offset);
        tr = current + (islo * (offset + targetOffset));
        bl = next + (lo * offset);
        br = next + (lo * (offset + targetOffset));



        meshBuilder.BuildTriangle(bl, br, tl, submesh); // We have too re arrange so that the normals face the right way 
        meshBuilder.BuildTriangle(br, tr, tl, submesh);


    }


    private void PointMaker()
    {


     
        float segmentDegrees = 360f / segments;  // dividing the segments we want by 360 full rotation to get the segment degree


        for (float degrees = 0; degrees < 360f; degrees += segmentDegrees) //360 f is a full rotation all around the circle at different increments of degrees 
        {
            Vector3 point = Quaternion.AngleAxis(degrees, Vector3.up) * Vector3.forward * radius; //This is going to create a point with the radius i created
            points.Add(point); //addind the points to our list
        }

        curve();

        
        for (int i = 1; i < points.Count + 1; i++) // going through points list // getting 3 different points from the points array 
        {
            Vector3 pPrev = points[i - 1]; // the previous point 
            Vector3 pCurr = points[i % points.Count]; // the current point % to stay in the array avoid out of range error 
            Vector3 pNext = points[(i + 1) % points.Count]; // the next point

            roadEd(meshBuilder, pPrev, pCurr, pNext); // building the road

            edges(meshBuilder, pPrev, pCurr, pNext);
        }




        int randomNum = Random.RandomRange(0,points.Count-1); // setting the random location of the car
        car.transform.position = points[randomNum];
        car.transform.LookAt(points[1]);


        float carx = points[randomNum].x + 5;

        float carz = points[randomNum].z+ 2;

        cube = Instantiate(Resources.Load<GameObject>("Cube"), new Vector3(carx,6f,carz), Quaternion.identity); // setting the start marker
        cube.name = "Starter";

        meshFilter.mesh = meshBuilder.CreateMesh();

        meshCollider.sharedMesh = meshFilter.mesh; // populating mesh collider 

    }


    private void curve()// wave method
    {

        // applying perlin noise to each point in the circle 
        Vector2 wave = this.waveOffset; 

        for (int i = 0; i < points.Count; i++) // going through points 
        {
            wave += waveStep; // waves changing by step 

            Vector3 point = points[i]; // ref to point
            Vector3 centreDirection = point.normalized; // direction from the centre 

            float noise = Mathf.PerlinNoise(wave.x * waveScale, wave.y * waveScale); // adding the perlin noise
            noise *= wavyness;

            float control = Mathf.PingPong(i, points.Count / 2f) / (points.Count / 2f); // to fix the edges at the start and along the track  ping pong between 0 and 1 

            points[i] += centreDirection * noise * control;
        }
    }


    private void roadEd(MeshBuilder meshBuilder, Vector3 pPrev, Vector3 pCurr, Vector3 pNext) {

        //Road Line
        offset = Vector3.zero;
        targetOffset = Vector3.forward * lineWidth; //  linewidth is in the center of the track 

    
        RoadQuad(meshBuilder, pPrev, pCurr, pNext, offset, targetOffset, 0); // setting up the road buildin method 

        //Road - making the road 
        offset += targetOffset; // the  target offset is offset  to start from where i stopped 
        targetOffset = Vector3.forward * roadWidth;

        RoadQuad(meshBuilder, pPrev, pCurr, pNext, offset, targetOffset, 1);




    }


    private void MaterialList()// adding the materials randomly 
    {

        List<Material> materialsList = new List<Material>(); 


        for (int i = 0; i <= 6; i++)
        {
            Material randMaterial = new Material(Shader.Find("Specular"));
            randMaterial.color = UnityEngine.Random.ColorHSV();
            materialsList.Add(randMaterial);

        }

        MeshRenderer meshRenderer = this.GetComponent<MeshRenderer>();
        meshRenderer.materials = materialsList.ToArray();

    }


    IEnumerator endP()
    { 
        yield return new WaitForSeconds(15f); // loading the end marker to go to the next race track. 

        float Xc = carPos.x;
        float Yc = carPos.y-6f;
        float Zc = carPos.z;



        Wall = Instantiate(Resources.Load<GameObject>("RaceEnd"), new  Vector3 (Xc,Yc,Zc), Quaternion.identity);

     

        yield return null;



    }
}
