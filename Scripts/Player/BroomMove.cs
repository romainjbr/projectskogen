using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BroomMove : MonoBehaviour
{

    Player player;


    private float direction;
    public float moveSpeed;
    private Vector2 moveDirection;

    public bool Flying = false;

    public void OnTriggerEnter2D(Collider2D collision){
        if(collision.CompareTag("Player"))
        { 
            player = GameObject.Find("Player").GetComponent<Player>();
            player.Actions.TransitionBroom();
            
            player.Components.RigidBody.gravityScale = 0;
            Flying = true;
            Destroy(gameObject);
            
        }
    }

}
