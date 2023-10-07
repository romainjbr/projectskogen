using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructableRigidBody : MonoBehaviour
{

    [SerializeField] Vector2 forceDirection;

    [SerializeField] float torque;

    Rigidbody2D rb2d;

    public void Start()
    {
        float randTorque = UnityEngine.Random.Range(-150, 150);
        float randForceX = UnityEngine.Random.Range(forceDirection.x-5, forceDirection.x+5);
        float randForceY = UnityEngine.Random.Range(forceDirection.y-5, forceDirection.y+5);

        rb2d = GetComponent<Rigidbody2D>();
        rb2d.AddForce(forceDirection);
        rb2d.AddTorque(torque);

        Destroy(rb2d.gameObject, 1f);
    }
}
