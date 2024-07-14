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
    
    
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        player = GameObject.FindGameObjectWithTag("Player");
        
        
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector2.Distance(transform.position, player.transform.position);
        Vector2 direction = player.transform.position - transform.position;

        transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, speed * Time.deltaTime);
        
        spriteRenderer.flipX = player.transform.position.x < this.transform.position.x;
      
    }

     void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject == player)
        {
            isPlayerInTrigger = true;
            StartCoroutine(DamagePlayerOverTime());
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject == player)
        {
            isPlayerInTrigger = false;
            StopCoroutine(DamagePlayerOverTime());
        }
    }   
  
    IEnumerator DamagePlayerOverTime()
    {
        while (isPlayerInTrigger)
        {
            PlayerStats.Instance.TakeDamage(1);
            yield return new WaitForSeconds(1);
        }
    }
    
}


