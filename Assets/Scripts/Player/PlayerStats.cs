using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    CharacterScriptableObject characterData;

    private Color originalColor; // test
    public SpriteRenderer spriteRenderer; //TEST
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
    public Color flashColor = Color.blue; // colour to flash when invinc -- REMOVE IF YOU WANT TO DO THIS WITH THE SPRITE!!!!! (putting in caps so i dont forget this time hehe)
    public float flashDuration = 1.7f;
    public float recoveryDelay = 10f;
    public float recoveryRate = 1f;
    private bool isDamaged = false;
    private Coroutine recoveryCoroutine;
    private WaveSpawner waveSpawner;


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

    void Start()
    {
        originalColor = spriteRenderer.color;
        waveSpawner = FindObjectOfType<WaveSpawner>();
    }

    void Update()
    {
        if (invincibilityTimer > 0)
        {
            StartCoroutine(FlashBlue());
            invincibilityTimer -= Time.deltaTime;
        }
        //If the invincibility timer has reached 0, set the invincibility flag to false
        else if (isInvincible)
        {
            isInvincible = false;
        }

        if(isDamaged) // If damaged, then start recovering health after a short delay
        {
            if(recoveryCoroutine != null)
            {
                StopCoroutine(recoveryCoroutine);
            }
            recoveryCoroutine = StartCoroutine(RecoverHealthAfterDelay());
            isDamaged = false;
        }

        // Recover();
    }

    IEnumerator RecoverHealthAfterDelay()
    {
        yield return new WaitForSeconds(recoveryDelay);

        while (currentHealth < characterData.MaxHealth && currentHealth <= 50)
        {
            currentHealth += recoveryRate;
             if (currentHealth > characterData.MaxHealth)
            {
                currentHealth = characterData.MaxHealth;
            }
            yield return new WaitForSeconds(0.5f);
        }
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
                SFXManager.instance.PlayDeathSFX(); //TESTEST
                Kill();
            }
            isDamaged = true;
        }
    }

    public void Kill()
    {
        if(!StateManager.instance.isGameOver)
        {
            StateManager.instance.WaveCounterUI(waveSpawner.currWave);
            StateManager.instance.GameOver();
        }
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


    private IEnumerator FlashBlue() //TEST
    {
        spriteRenderer.color = flashColor;

        yield return new WaitForSeconds(flashDuration);

        spriteRenderer.color = originalColor;
    }
}
