using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    CharacterScriptableObject characterData;

    //Current stats
    [HideInInspector]
    public float currentHealth;
    [HideInInspector]
    public float currentRecovery;
    [HideInInspector]
    public float currentMoveSpeed;
    [HideInInspector]
    public float currentStrength;
    [HideInInspector]
    public float currentProjectileSpeed;
    [HideInInspector]

    //Spawned Weapon
    public List<GameObject> spawnedWeapons;

    public GameOver gameOver;

     public delegate void OnHealthChangedDelegate();
     public OnHealthChangedDelegate onHealthChangedCallback;

    private static PlayerStats instance;
    public static PlayerStats Instance
    {
        get
        {
            if (instance == null)
                instance = FindObjectOfType<PlayerStats>();
            return instance;
        }
    }

    //I-Frames
    [Header("I-Frames")]
    public float invincibilityDuration;
    float invincibilityTimer;
    bool isInvincible;

    void Awake()
    {
        characterData = CharacterSelector.GetData();
        CharacterSelector.instance.DestroySingleton();


        //Assign the variables
        currentHealth = characterData.MaxHealth;
        currentRecovery = characterData.Recovery;
        currentMoveSpeed = characterData.MoveSpeed;
        currentStrength = characterData.Strength;
        currentProjectileSpeed = characterData.ProjectileSpeed;

        //Spawn the starting weapon
        SpawnWeapon(characterData.StartingWeapon);
    }
    
    


    void Update()
    {
        if (invincibilityTimer > 0)
        {
            invincibilityTimer -= Time.deltaTime;
        }
        //If the invincibility timer has reached 0, set the invincibility flag to false
        else if (isInvincible)
        {
            isInvincible = false;
        }

        Recover();
    }


    public void TakeDamage(float dmg)
    {
        //If the player is not currently invincible, reduce health and start invincibility
        if (!isInvincible)
        {
            currentHealth -= dmg;

            invincibilityTimer = invincibilityDuration;
            isInvincible = true;

            if (currentHealth <= 0)
            {
                Kill();
            }
        }
    }

    public void Kill()
    {
        gameOver.OnDeath();
        Debug.Log("PLAYER IS DEAD");
    }

    public void RestoreHealth(float amount)
    {
        // Only heal the player if their current health is less than their maximum health
        if (currentHealth < characterData.MaxHealth)
        {
            currentHealth += amount;

            // Make sure the player's health doesn't exceed their maximum health
            if (currentHealth > characterData.MaxHealth)
            {
                currentHealth = characterData.MaxHealth;
            }
        }
    }

    public float MaxHealth
    {
       get { return characterData.MaxHealth; }
    }

    void Recover()
    {
        if(currentHealth < characterData.MaxHealth)
        {
            currentHealth += currentRecovery * Time.deltaTime;

            // Make sure the player's health doesn't exceed their maximum health
            if (currentHealth > characterData.MaxHealth)
            {
                currentHealth = characterData.MaxHealth;
            }
        }
    }

    public void SpawnWeapon(GameObject weapon)
    {
        //Spawn the starting weapon
        GameObject spawnedWeapon = Instantiate(weapon, transform.position, Quaternion.identity);
        spawnedWeapon.transform.SetParent(transform);    //Set the weapon to be a child of the player
        spawnedWeapons.Add(spawnedWeapon);  //Add it to the list of spawned weapons
    }
}
