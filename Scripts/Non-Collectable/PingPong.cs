using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PingPong : MonoBehaviour
{

    [SerializeField]private float speed;
    [SerializeField] private float howMuch;

    private bool movingToTarget = true; 
    
    private Vector3 initial_pos;
    private Vector3 target_pos;

    void Start(){

        initial_pos = transform.position;
        target_pos = new Vector3 (transform.position.x, transform.position.y + howMuch);

    }


    void Update()
    {

        Vector3 destination = movingToTarget ? target_pos : initial_pos;
        transform.position = Vector2.MoveTowards(transform.position, destination, Time.deltaTime * speed);

        if (transform.position == destination)
        {
            movingToTarget = !movingToTarget;
        }
    }

}

