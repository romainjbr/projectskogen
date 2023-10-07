using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kannelbulla : MonoBehaviour, ICollectable
{
    
    Player player;
    private bool isCollected = false;

    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
    }


    private void OnTriggerEnter2D(Collider2D collision){
        if (collision.tag == "Player" && !isCollected)
        {
            Collect();
        }
    }


    public void Collect(){

        isCollected = true;
        player.TakeHealth();
        Destroy(gameObject);
    }
}
