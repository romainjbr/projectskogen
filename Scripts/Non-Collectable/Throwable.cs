using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class Throwable : MonoBehaviour
{

    Player player;

    private Vector2 direction; 

    private Transform throwSpawnPoint;

    public float throwForce = 550f;

    private void Awake(){
        player = GameObject.Find("Player").GetComponent<Player>();
    }

    private void Start() {
        throwSpawnPoint = transform;
        Thrown();
    }

    public void Setup(Vector2 direction){
        this.direction = direction;
    }

    private void Thrown()
    {
        Rigidbody2D rb = gameObject.GetComponent<Rigidbody2D>();

        if (rb != null)
        {
            float throwDirectionX = player.Stats.Direction.x;
            rb.AddForce(direction * throwForce, ForceMode2D.Impulse);

             Destroy(gameObject, 3.0f);

        }
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "DamageArea" || collision.tag == "Ground" || collision.tag == "Box")  
        {
            Destroy(gameObject);
        }
  
    }

    private void OnBecameInvisible() {
        Destroy(gameObject);
    }
}