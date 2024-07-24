using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="CharacterScriptableObject", menuName ="ScriptableObjects/Character")]
public class CharacterScriptableObject : ScriptableObject
{
    [SerializeField]
    GameObject startingWeapon;
    public GameObject StartingWeapon { get => startingWeapon; private set => startingWeapon = value;}
    
    [SerializeField]
    float maxHealth;
    public float MaxHealth { get => maxHealth; private set => maxHealth = value;}

    [SerializeField]
    float recovery;
    public float Recovery { get => recovery; private set => recovery = value;}
    
    [SerializeField]
    float moveSpeed;
    public float MoveSpeed { get => moveSpeed; private set => moveSpeed = value;}
    
    [SerializeField]
    float strength; //strength will be our basic damage multiplier to be used in future
    public float Strength { get => strength; private set => strength = value;}

    [SerializeField]
    float projectileSpeed; //likewise, projectile speed can be changed in future for items
    public float ProjectileSpeed { get => projectileSpeed; private set => projectileSpeed = value;}
}
