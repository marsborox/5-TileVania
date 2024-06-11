using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSession : MonoBehaviour
{
    [SerializeField] int playerLives = 3;
    
    //will be called every time this is called,
    //when we start game, die or restart
    //it will destroy new one
    private void Awake()
    {//so find agamesession object, .Length because 
        //we want to know how many game session we have
        int numGameSessions = FindObjectsOfType<GameSession>().Length;
        //if there is more than one destroy whatever appears
        //has to destroy game object because we found OBJECT of type
        if (numGameSessions > 1)
        {
            Destroy(gameObject);
        }
        else 
        { //each time you load a scene dont destroy it
            DontDestroyOnLoad(gameObject);
        }
    }
    public void ProcessPlayerDeath()
    {
        //we must add this into die Method in Player
        //FindObjectOfType<GameSession>().ProcessPlayerDeath();
        if (playerLives > 1)
        {
            TakeLife();
        }
        else 
        {
            ResetGameSession();
        }
    }
    void TakeLife()
    {
        playerLives--;
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
    private void ResetGameSession()
    {//we call method LoadScene from SceneManager
        //we call scene 0
        SceneManager.LoadScene(0);
        Destroy(gameObject);
    }
}
