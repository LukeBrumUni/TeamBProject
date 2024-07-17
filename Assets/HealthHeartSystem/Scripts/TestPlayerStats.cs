/*
 *  Author: ariel oliveira [o.arielg@gmail.com]
 */

using UnityEngine;

public class TestPlayerStats : MonoBehaviour
{
    public delegate void OnHealthChangedDelegate();

    public GameOver gameOver;
    public OnHealthChangedDelegate onHealthChangedCallback;

    #region Sigleton
    private static TestPlayerStats instance;
    public static TestPlayerStats Instance
    {
        get
        {
            if (instance == null)
                instance = FindObjectOfType<TestPlayerStats>();
            return instance;
        }
    }
    #endregion

    [SerializeField]
    private float health;
    [SerializeField]
    private float maxHealth;
    [SerializeField]
    private float maxTotalHealth;

    public float Health { get { return health; } }
    public float MaxHealth { get { return maxHealth; } }
    public float MaxTotalHealth { get { return maxTotalHealth; } }

    public void Heal(float health)
    {
        this.health += health;
        ClampHealth();
    }

    public void TakeDamage(float dmg)
    {
        health -= dmg;
        ClampHealth();

        if (health <= 0)
        {
            gameOver.OnDeath();
            Debug.Log ("On Death is called");
        }
        else
        {
            Debug.Log ("Health is above 0");
        }
    }

    public void AddHealth()
    {
        if (maxHealth < maxTotalHealth)
        {
            maxHealth += 1;
            health = maxHealth;

            if (onHealthChangedCallback != null)
                onHealthChangedCallback.Invoke();
        }   
    }

    void ClampHealth()
    {
        health = Mathf.Clamp(health, 0, maxHealth);

        if (onHealthChangedCallback != null)
            onHealthChangedCallback.Invoke();
    }
}
