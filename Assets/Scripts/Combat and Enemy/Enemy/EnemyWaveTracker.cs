using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWaveTracker : MonoBehaviour
{
    void OnDestroy()
    {
        var waveSpawner = GameObject.FindGameObjectWithTag("WaveSpawner");
        if (waveSpawner != null)
        {
            waveSpawner.GetComponent<WaveSpawner>().spawnedEnemies.Remove(gameObject);
        }
    }
}