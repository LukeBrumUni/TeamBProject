using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : WeaponController
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    protected override void Attack()
    {
        base.Attack();
        GameObject spawnedGun = Instantiate(weaponData.Prefab);
        spawnedGun.transform.position = transform.position; //same pos as player object
        spawnedGun.GetComponent<GunBehaviour>().DirectionChecker(pm.lastMovedVector); // Use the property
    }
}
