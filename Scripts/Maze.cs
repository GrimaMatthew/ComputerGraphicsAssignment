using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Maze : MonoBehaviour
{
    GameObject plane;
    GameObject StartPoint;
    GameObject EndPoint;

    GameObject wallStraight;
    GameObject wallCorner;

    GameObject cube;


    public GameObject Player;
    // Start is called before the first frame update
    void Start()
    {

        float x = Random.Range(7, 30);
        float z = Random.Range(10, 200);
        float x1 = Random.Range(210, 222);
        float z1 = Random.Range(10, 200);


        plane = Instantiate(Resources.Load<GameObject>("Plane"), new Vector3(0f, 0f), Quaternion.identity);
        StartPoint = Instantiate(Resources.Load<GameObject>("Pyri"), new Vector3(x, 0f, z), Quaternion.identity);
        EndPoint = Instantiate(Resources.Load<GameObject>("Pyri"), new Vector3(x1, 0f, z1), Quaternion.identity);

        StraightWall();
        cornerWall();
        CubeMaker();

        Player.transform.position = new Vector3(x,2f,z);
     












    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void CubeMaker()
    {

        for (int i = 110; i <= 140; i += 10)
        {
            for (int c = 170; c <= 190; c += 10)
            {
                cube = Instantiate(Resources.Load<GameObject>("Cube"), new Vector3(c, 1.3f, i), Quaternion.identity);

            }
        }


        for (int i = 110; i <= 140; i += 10)
        {
            for (int c = 170; c <= 190; c += 10)
            {
                cube = Instantiate(Resources.Load<GameObject>("Cube"), new Vector3(i, 1.3f, c), Quaternion.identity);

            }
        }

        for (int i = 61; i <= 89; i += 10)
        {
            for (int c = 28; c <= 46; c += 10)
            {
                cube = Instantiate(Resources.Load<GameObject>("Cube"), new Vector3(i, 1.3f, c), Quaternion.identity);

            }
        }

    }



    private void StraightWall()
    {
        for (int i = 42; i < 200; i += 10)
        {
            wallStraight = Instantiate(Resources.Load<GameObject>("Wall2"), new Vector3(i, 0f, 20), Quaternion.identity);

        }

        for (int i = 80; i < 180; i += 10)
        {
            wallStraight = Instantiate(Resources.Load<GameObject>("Wall2"), new Vector3(i, 0f, 50), Quaternion.identity);

        }
        wallStraight = Instantiate(Resources.Load<GameObject>("Wall2"), new Vector3(48, 0f, 100), Quaternion.identity);


        for (int r = 48; r <= 120; r += 15)
        {
            wallStraight = Instantiate(Resources.Load<GameObject>("Wall2"), new Vector3(r, 0f, 140), Quaternion.identity);


        }


        wallCorner = Instantiate(Resources.Load<GameObject>("Wall"), new Vector3(40, -5f, 180), Quaternion.identity);

        for (int r = 48; r <= 140; r += 15)
        {
            wallStraight = Instantiate(Resources.Load<GameObject>("Wall2"), new Vector3(r, 0f, 161), Quaternion.identity);


        }

        for (int r = 48; r <= 160; r += 15)
        {
            wallStraight = Instantiate(Resources.Load<GameObject>("Wall2"), new Vector3(r, 0f, 197), Quaternion.identity);


        }


        for (int r = 168; r <= 200; r += 18)
        {
            wallStraight = Instantiate(Resources.Load<GameObject>("Wall2"), new Vector3(r, 0f, 200), Quaternion.identity);


        }

        for (int r = 114; r <= 190; r += 18)
        {
            wallStraight = Instantiate(Resources.Load<GameObject>("Wall2"), new Vector3(r, 0f, 121), Quaternion.identity);


        }

        for (int r = 125; r <= 170; r += 18)
        {

            for (int f = 60; f <= 110; f += 10)
            {
                wallStraight = Instantiate(Resources.Load<GameObject>("Wall2"), new Vector3(r, 0f, f), Quaternion.identity);


            }


        }



        wallStraight = Instantiate(Resources.Load<GameObject>("Wall2"), new Vector3(169, 0f, 80), Quaternion.identity);
        wallStraight = Instantiate(Resources.Load<GameObject>("Wall2"), new Vector3(186, 0f, 81), Quaternion.identity);


        for (int i = 122; i < 185; i += 15)
        {
            wallStraight = Instantiate(Resources.Load<GameObject>("Wall2"), new Vector3(i, 0f, 41), Quaternion.identity);

        }

        wallStraight = Instantiate(Resources.Load<GameObject>("Wall2"), new Vector3(186, 0f, 41), Quaternion.identity);


        for (int i = 55; i <= 200; i += 15)
        {
            wallStraight = Instantiate(Resources.Load<GameObject>("Wall2"), new Vector3(i, 0f, 168), Quaternion.identity);


        }

    }


    private void cornerWall()
    {


        for (int d = 20; d <= 150; d += 10)

        {
            wallCorner = Instantiate(Resources.Load<GameObject>("Wall"), new Vector3(200, -5f, d), Quaternion.identity);


        }


        for (int j = 25; j <= 100; j += 25)
        {
            wallCorner = Instantiate(Resources.Load<GameObject>("Wall"), new Vector3(100, -5f, j), Quaternion.identity);


        }



        for (int r = 70; r <= 100; r += 25)
        {
            wallCorner = Instantiate(Resources.Load<GameObject>("Wall"), new Vector3(72, -5f, r), Quaternion.identity);


        }

        for (int r = 42; r <= 140; r += 20)
        {
            wallCorner = Instantiate(Resources.Load<GameObject>("Wall"), new Vector3(40, -5f, r), Quaternion.identity);


        }



        wallCorner = Instantiate(Resources.Load<GameObject>("Wall"), new Vector3(148, -5f, 142), Quaternion.identity);




        wallCorner = Instantiate(Resources.Load<GameObject>("Wall"), new Vector3(161, -5f, 200), Quaternion.identity);


        wallCorner = Instantiate(Resources.Load<GameObject>("Wall"), new Vector3(196, -5f, 210), Quaternion.identity);





        for (int i = 62; i < 140; i += 15)
        {
            wallCorner = Instantiate(Resources.Load<GameObject>("Wall"), new Vector3(196, -5f, i), Quaternion.identity);

        }

        wallCorner = Instantiate(Resources.Load<GameObject>("Wall"), new Vector3(178, -5f, 93), Quaternion.identity);





        wallCorner = Instantiate(Resources.Load<GameObject>("Wall"), new Vector3(97.6f, -5f, 202), Quaternion.identity);



        //97.6 202  wall


    }
}
