using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeMaker : MonoBehaviour
{
    // Gameobject REfs
    GameObject plane;
    GameObject StartPoint;
    GameObject EndPoint;
    GameObject Walls;
    GameObject Obsti;
    // Start is called before the first frame update


    /// <summary>
    /// In this script the different game objects were spawned at different 
    /// </summary>
    void Start()
    {
        float x = Random.Range(7, 30);
        float z = Random.Range(10, 200);
        float x1 = Random.Range(210, 222);
        float z1 = Random.Range(10, 200);


        plane = Instantiate(Resources.Load<GameObject>("Plane"), new Vector3(0f, 0f), Quaternion.identity);
        StartPoint = Instantiate(Resources.Load<GameObject>("Pyri"), new Vector3(x, 0f, z), Quaternion.identity);
        EndPoint = Instantiate(Resources.Load<GameObject>("Pyri"), new Vector3(x1, 0f, z1), Quaternion.identity);


        for (int i = 0; i <= 1000; i++)
        {
            float x2 = Random.Range(30, 208);
            float z2 = Random.Range(2, 225);
            Walls = Instantiate(Resources.Load<GameObject>("Wall"), new Vector3(x2, 0f, z2), Quaternion.identity);

        }

        for (int i = 0; i <= 500; i++)
        {
            float x2 = Random.Range(30, 208);
            float z2 = Random.Range(2, 225);
            Walls = Instantiate(Resources.Load<GameObject>("Wall2"), new Vector3(x2, 0f, z2), Quaternion.identity);

        }

        for (int i = 0; i <= 600; i++)
        {
            float x2 = Random.Range(32, 208);
            float z2 = Random.Range(2, 225);
            Obsti = Instantiate(Resources.Load<GameObject>("Cube"), new Vector3(x2, 2f, z2), Quaternion.identity);

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
