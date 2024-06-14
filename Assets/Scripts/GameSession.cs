using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSession : MonoBehaviour
{
    [SerializeField] int playerLives = 3;
    [SerializeField] int score = 0;
    [SerializeField] TextMeshProUGUI livesText;
    [SerializeField] TextMeshProUGUI scoreText;
    


    
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
    private void Start()
    {
        livesText.text = playerLives.ToString();
        scoreText.text = score.ToString();
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

    public void AddToScore(int pointsToAdd)
    {
        score += pointsToAdd;
        scoreText.text = score.ToString();
    }
    void TakeLife()
    {
        playerLives--;
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
        //we must add this here so everytime plr dies
        //this updates the text
        livesText.text = playerLives.ToString();
    }
    private void ResetGameSession()
    {
        FindObjectOfType<ScenePersist>().ResetScenePersist();
        
        //we call method LoadScene from SceneManager
        //we call scene 0
        SceneManager.LoadScene(0);
        Destroy(gameObject);
    }
}
