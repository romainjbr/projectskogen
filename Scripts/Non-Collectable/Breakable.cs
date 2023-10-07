using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakable : MonoBehaviour, ICollisionHandler
{

    [SerializeField] public GameObject fallingObject;

    public Animator animator;

    [SerializeField] int health = 1;

    public bool Hitable;
    public bool repeatedFalling;

    public GameObject destructableRef; 
    public GameObject spawnedObject;
    
    private AudioSource woodSound;
    
    private Vector3 current_pos;
    private Vector3 initial_pos;


    private void Awake(){
        woodSound = GetComponent<AudioSource>();
        current_pos = transform.position;
        initial_pos = transform.position;
    }

    private void Update(){
        current_pos = transform.position;
    }

    private void Spawning(){
        GameObject falling = (GameObject)Instantiate(fallingObject, initial_pos, transform.rotation);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Hit") && Hitable){ //if collission object is weapon
            health--;

            if(health == 0)
            {
                ExplodeThisGameObject();
            }
            
            else if (health > 0)
            {
                woodSound.Play();
            }
       } 

        else if (!Hitable && (collision.CompareTag("Player") || collision.CompareTag("Ground")))
        {
            collision.GetComponent<Player>()?.TakeDamage(1);
            ExplodeThisGameObject();
        }
    }

    private void NewFallingLog(){

        GameObject newObject = Instantiate(PrefabManager.Instance.FallingBranchPrefab, initial_pos, transform.rotation);
        Destroy(newObject, 2f);

    }

    private void ExplodeThisGameObject()
    {

        if (spawnedObject != null)
        {
            GameObject spawning = (GameObject)Instantiate(spawnedObject);
            spawning.transform.position = current_pos;
            
        }

        if(Hitable)
        {
            GameObject destructable = (GameObject)Instantiate(destructableRef, current_pos, transform.rotation);

            if (animator != null)
            {
                animator.SetBool("broken", true);
            }
        }

        else{

            if (repeatedFalling)
                {
                    GameObject newObject = Instantiate(PrefabManager.Instance.FallingBranchPrefab, initial_pos, transform.rotation);
                    Destroy(newObject, 4f);
                    
                }
            GameObject destructed = Instantiate(PrefabManager.Instance.BrokenBranchPrefab, current_pos, transform.rotation);
            Destroy(destructed, 1f);
        
        }
        Destroy(gameObject);
    }
}

