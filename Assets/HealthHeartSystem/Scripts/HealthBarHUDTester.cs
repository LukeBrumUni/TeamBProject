/*
 *  Author: ariel oliveira [o.arielg@gmail.com]
 */

using UnityEngine;

public class HealthBarHUDTester : MonoBehaviour
{
    public void AddHealth()
    {
        TestPlayerStats.Instance.AddHealth();
    }

    public void Heal(float health)
    {
        TestPlayerStats.Instance.Heal(health);
    }

    public void Hurt(float dmg)
    {
        TestPlayerStats.Instance.TakeDamage(dmg);
    }
}
