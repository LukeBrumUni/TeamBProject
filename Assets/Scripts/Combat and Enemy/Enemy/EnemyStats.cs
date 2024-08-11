using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyStats : MonoBehaviour
{
    public EnemyScriptableObject enemyData;

    private Animator animator;


    //CURRENT stats
    float currentMoveSpeed;
    float currentHealth;
    float currentDamage;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Awake()
    {
        //overrides the 'max'
        currentMoveSpeed = enemyData.MoveSpeed;
        currentHealth = enemyData.MaxHealth;
        currentDamage = enemyData.Damage;
    }

    public void TakeDamage(float dmg)
    {
        currentHealth -= dmg;

        if (currentHealth <= 0)
        {
            Kill();
        }
    }

    public void Kill()
    {
        OnDied.Invoke();
        Destroy(gameObject, 0.5f);
    }

    public UnityEvent OnDied;

    private void OnCollisionStay2D (Collision2D col)
    {
        if(col.gameObject.CompareTag("Player"))
        {
            PlayerStats player = col.gameObject.GetComponent<PlayerStats>();
            player.TakeDamage(currentDamage);
        }
    }
}