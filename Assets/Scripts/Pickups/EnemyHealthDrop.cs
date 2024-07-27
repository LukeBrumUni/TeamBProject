using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthDrop : MonoBehaviour
{
    [SerializeField]
    private float chanceOfHealthDrop;

    private PickupSpawner pickupSpawner;

    private void Awake()
    {
        pickupSpawner = FindObjectOfType<PickupSpawner>();
    }

    public void RandomlyDropHealth()
    {
        float random = Random.Range(0f,1f);

        if(chanceOfHealthDrop >= random)
        {
            pickupSpawner.SpawnCollectable(transform.position);
        }
    }
}
