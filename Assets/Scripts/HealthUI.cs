using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using WeaponSystem;
using TMPro;

public class HealthUI : MonoBehaviour
{
    public TMP_Text healthText;

    [SerializeField] 
    PlayerStats playerStats;
    
    float health;

    void Start()
        {
                playerStats = GetComponent<PlayerStats>();
                healthText = GetComponent<TMP_Text>();
        }

     void Update()
        {
               healthText.text = "Health:" + playerStats.currentHealth;
               
               if(playerStats.currentHealth <= 0)
                {
                  Destroy(gameObject);
                }
        }
}
