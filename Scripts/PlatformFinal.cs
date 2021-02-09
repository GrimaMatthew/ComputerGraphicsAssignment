using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformFinal : MonoBehaviour
{

    GameObject plane; 
    // Start is called before the first frame update
    void Start()
    {

        for (int x = -790; x <= 533; x += 220)
        {
            
           plane = Instantiate(Resources.Load<GameObject>("Plane"), new Vector3(x, -0.01f, 187), Quaternion.identity);
           

        }
        for (int x = -790; x <= 533; x += 220)
        {

            plane = Instantiate(Resources.Load<GameObject>("Plane"), new Vector3(x, -0.01f, 412), Quaternion.identity);


        }

        for (int z = -1245; z <= 25; z += 200)
        {
            

            for (int x = -790; x <= 700; x += 100)
            {
                plane = Instantiate(Resources.Load<GameObject>("Plane"), new Vector3(x, -0.01f, z), Quaternion.identity);

            }

        }

    }

   
}
