using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour

{
    // Start is called before the first frame update

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
      Destroy(player);
      deathPanel.SetActive(!deathPanel.activeSelf);
    }
}
