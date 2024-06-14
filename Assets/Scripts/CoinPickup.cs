using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickup : MonoBehaviour
{
    [SerializeField] AudioClip coinPickupSFX;
    [SerializeField] int pointsToPickup = 100;

    bool wasCollected = false;

     void OnTriggerEnter2D(Collider2D other)
     {
        if (other.tag == "Player"||!wasCollected)
        {   
            wasCollected = true;
            //run method from other class
            FindObjectOfType<GameSession>().AddToScore(pointsToPickup);
            //do audiosource, method playclip, (what sound,where),volume if we want
            AudioSource.PlayClipAtPoint(coinPickupSFX,Camera.main.transform.position);
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
     }
}
