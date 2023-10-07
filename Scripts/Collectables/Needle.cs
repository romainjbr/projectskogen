using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Needle : MonoBehaviour, ICollectable

{

    private AudioSource needleSound;
    private Transform collectNeedle;
    private bool collected = false;
    private float speed = 20;

    private void Awake(){
        needleSound = GetComponent<AudioSource>();
    }

    private void Start()
    {
        collectNeedle = GameObject.Find("CollectNeedle").transform;
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        if (collected)
        {
            transform.position = Vector3.MoveTowards(transform.position, collectNeedle.position, Time.deltaTime * speed);

        }
        if (transform.position == collectNeedle.position)
        {
            UIManager.Instance.AddNeedle();
            Destroy(gameObject);
        }

    }

    public void Collect()
    {
        collected = true;
        needleSound.Play();

    }
}
