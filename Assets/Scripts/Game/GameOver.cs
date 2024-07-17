using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WeaponSystem;

public class GameOver : MonoBehaviour

{
    // Start is called before the first frame update
    public bool isDead = false;

    private GameObject player;
    [SerializeField] GameObject deathPanel;
  

    void Start()
    {
       player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void OnDeath()
    {
      if (isDead == false)
      {
      Destroy(player);
      deathPanel.SetActive(!deathPanel.activeSelf);
      isDead = true;
      }
      
    }

    public bool IsDead()
    {
      return isDead;
    }
}
