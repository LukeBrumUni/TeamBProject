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
    private PlayerStats playerStats;

    private float clampedHealth;
    
    void Start()
    {

        

        if (playerStats == null)
        {
            playerStats = GetComponent<PlayerStats>();
        }

        if (healthText == null)
        {
            healthText = GetComponent<TMP_Text>();
        }
    }

    void Update()
    {
        clampedHealth = Mathf.Ceil(playerStats.currentHealth);

        if (playerStats != null && healthText != null)
        {
            healthText.text = "Health: " + clampedHealth;

            if (clampedHealth <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}