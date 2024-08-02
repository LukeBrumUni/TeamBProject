using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{


    public float pickupHealth = 20f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.CompareTag("Player"))
        {
          PlayerStats playerStats = col.gameObject.GetComponent<PlayerStats>();
            if (playerStats != null)
            {
                if (playerStats.currentHealth != 100) // change to reference of characterData if health changes happen later
                {
                playerStats.RestoreHealth(pickupHealth);
                Destroy(gameObject);
                }
            }
        }
    }
}
