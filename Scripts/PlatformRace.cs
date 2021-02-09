using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformRace : MonoBehaviour
{

    GameObject plane;

    // Start is called before the first frame update
    void Start()
    {

        for (int z = -400; z <= 240; z += 220)
        {
            for (int x = -351; x <= 108; x += 456)
            {
                plane = Instantiate(Resources.Load<GameObject>("Plane"), new Vector3(x, -0.01f, z), Quaternion.identity);
            }
            
        }

        for (int z = -400; z <= 240; z += 220)
        {
            
                plane = Instantiate(Resources.Load<GameObject>("Plane"), new Vector3(-131.5f, -0.02f, z), Quaternion.identity);
        

        }



    }

  
}
