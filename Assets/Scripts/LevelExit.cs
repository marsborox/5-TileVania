using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour
{
    [SerializeField]float levelLoadDelay = 0.5f;
    private void OnTriggerEnter2D(Collider2D other)
    {//in case we mess up physics we can specify tag
        if (other.tag == "Player")
        {
            StartCoroutine(ExitLevel());
        }
    }
    IEnumerator ExitLevel()
    {
        yield return new WaitForSecondsRealtime(levelLoadDelay);
        //current scene index - scene manager will get active scenes build index
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        //what is next we gonna load
        int nextSceneIndex = currentSceneIndex+1;

        //if we reach max level next level will be 0
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }
        //just load next level
        SceneManager.LoadScene(nextSceneIndex);
    }
}
