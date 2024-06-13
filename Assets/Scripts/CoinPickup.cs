using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickup : MonoBehaviour
{
    [SerializeField] AudioClip coinPickupSFX;
     void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {//do audiosource, method playclip, (what sound,where),volume if we want
            AudioSource.PlayClipAtPoint(coinPickupSFX,Camera.main.transform.position);
            Destroy(gameObject);
        }
    }
}
