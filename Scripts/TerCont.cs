using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerCont : MonoBehaviour
{

    GameObject ter;
    // Start is called before the first frame update
    void Start()
    {

        for (int i = 0; i <= 1000; i +=1000) // creating an additional two terrains from prefab
        {
           
               ter = Instantiate(Resources.Load<GameObject>("Terrain"), new Vector3(i, 0f, 0), Quaternion.identity);

            
        }

    }

}
