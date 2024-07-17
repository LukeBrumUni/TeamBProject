using System.Collections;
using System.Collections.Generic;
using UnityEngine.Timeline;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [Header("Weapon Options")]
    public WeaponScriptableObject weaponData;
    float currentCooldown;

    protected PlayerMovement pm; //determining weapon dir

    protected virtual void Start()
    {
        pm = FindObjectOfType<PlayerMovement>();
        currentCooldown = weaponData.CooldownDuration; //set cooldown @ start
    }

    protected virtual void Update()
    {
        currentCooldown -= Time.deltaTime;
        if(currentCooldown <= 0f)
        {
            Attack();
        }
    }

    protected virtual void Attack()
    {
        currentCooldown = weaponData.CooldownDuration;
    }
}
