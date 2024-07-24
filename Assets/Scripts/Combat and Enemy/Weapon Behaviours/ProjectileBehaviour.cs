using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehaviour : MonoBehaviour
{
    public WeaponScriptableObject weaponData;
    protected Vector3 direction;
    public float destroyAfterSeconds;

    //Current stats
    protected float currentDamage;
    protected float currentSpeed;
    protected float currentCooldownDuration;
    protected int currentPierce;

    void Awake()
    {
        currentDamage = weaponData.Damage;
        currentSpeed = weaponData.Speed;
        currentCooldownDuration = weaponData.CooldownDuration;
        currentPierce = weaponData.Pierce;
    }

    protected virtual void Start()
    {
        Destroy(gameObject, destroyAfterSeconds);
    }

    public void DirectionChecker(Vector3 dir)
    {
        direction = dir;

        float dirx = direction.x;
        float diry = direction.y;
        float spriteAngleOffset = 0f; //this changes depending on sprite orientation
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        angle += spriteAngleOffset;
        transform.rotation = Quaternion.Euler(0, 0, angle);

        Vector3 scale = transform.localScale;
        Vector3 rotation = transform.rotation.eulerAngles; //sets the rotation for prefab for player direction

        if (dirx < 0 && diry == 0) //rotate to invert it
        {
            scale.x = scale.x * -1;
            scale.y = scale.y * -1;
        }
    }

    protected virtual void OnTriggerEnter2D(Collider2D col)
    {
        //Reference the script from the collided collider and deal damage using TakeDamage()
        if(col.CompareTag("Enemy"))
        {
            EnemyStats enemy = col.GetComponent<EnemyStats>();
            enemy.TakeDamage(currentDamage); //Make sure to use currentDamage instead of weaponData.Damage in case any damage multipliers in the future
            ReducePierce();
        }
    }

    void ReducePierce() //projectiles will only hit once
    {
        currentPierce--;
        if(currentPierce <= 0)
        {
            Destroy(gameObject);
        }
    }
}

