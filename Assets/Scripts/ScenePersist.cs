using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScenePersist : MonoBehaviour
{
    private void Awake()
    {//so find agamesession object, .Length because 
        //we want to know how many game session we have
        int numGamePersists = FindObjectsOfType<ScenePersist>().Length;

        //if there is more than one destroy whatever appears
        //has to destroy game object because we found OBJECT of type
        if (numGamePersists > 1)
        {
            Destroy(gameObject);
        }
        else
        { //each time you load a scene dont destroy it
            DontDestroyOnLoad(gameObject);
        }
    }

    public void ResetScenePersist()
    {
        Destroy(gameObject);
    }

}
