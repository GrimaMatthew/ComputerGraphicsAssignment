using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager inst;

    bool quiteg = false;
    public static bool level1 = false;
    public static bool level2 = false;
    public static bool complete = false;

    private void Awake()
    {
        Singleton();


    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        QuitsGame();
        loadGames();
    }


    private void Singleton()
    {
        if (inst != null)
        {
            Destroy(gameObject);
        }
        else
        {
            inst = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void loadGames()
    {

        if (level1)
        {

            SceneManager.LoadScene("RaceTwo");
            level1 = false;

        }

        if (level2)
        {
            SceneManager.LoadScene("RaceThree");
            level2 = false;

        }

        if (complete)
        {
            SceneManager.LoadScene("Home");
            complete = false;

        }

    }

    public void loadShapes()
    {
        
        SceneManager.LoadScene("SampleScene");
            

    }

    public void loadMaze()
    {

        SceneManager.LoadScene("Maze");


    }

    public void loadRace()
    {

        SceneManager.LoadScene("RaceOne");


    }


    public void loadAdventure()
    {

        SceneManager.LoadScene("Terraine");

    }

    public void QuitsGame()
    {

        if (Input.GetKeyDown("q"))
        {
            quiteg = true;
            if (quiteg)
            {
                SceneManager.LoadScene("Home");
                quiteg = false;

            }
      

        }


    }


}
