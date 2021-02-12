using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndCar : MonoBehaviour
{
    Scene currentScene;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        currentScene = SceneManager.GetActiveScene();
    }

    private void OnTriggerEnter(Collider other)
    {

        if (currentScene.name == "RaceOne")
        {
            print("Level1");
            GameManager.level1 = true;
          
        }

        if (currentScene.name == "RaceTwo")
        {
            GameManager.level2 = true;
            print("Level2");
        }


        if (currentScene.name == "RaceThree")
        {
            GameManager.complete = true;
            print("Lest");
        }
    }
}
