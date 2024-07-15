using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyChase : MonoBehaviour
{

    public GameObject player;
    public float speed;
    private float distance;
    private SpriteRenderer spriteRenderer;
    
    private bool isPlayerInTrigger;

    public float damageCooldown = 1f; // 1 second cooldown
    private bool isInvulnerable = false;

    public float baseEnemyDamage = 0.5f;
    
    
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        player = GameObject.FindGameObjectWithTag("Player");
        
        
    }

    // Update is called once per frame
    void Update()
    {
        

        transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, speed * Time.deltaTime);
        
        spriteRenderer.flipX = player.transform.position.x < this.transform.position.x;
      
    }

     void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject == player)
        {
            if(isInvulnerable == false)
            {
            isPlayerInTrigger = true;
            StartCoroutine(DamagePlayerOverTime());
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject == player)
        {
            isPlayerInTrigger = false;
            // isInvulnerable = false;
            StopCoroutine(DamagePlayerOverTime());
        }
    }   
  
    IEnumerator DamagePlayerOverTime()
    {
        while (isPlayerInTrigger) // original is a while statement
        {
            PlayerStats.Instance.TakeDamage(baseEnemyDamage);
            yield return new WaitForSeconds(damageCooldown); 
        }
        
    }
    
}


