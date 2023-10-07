using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushableObject : MonoBehaviour
{

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Animator animator;


    Player player;

    public bool isTree;
    public bool isBeingPushed = false;

    private float null_mass = 6.5f;
    [SerializeField] public float initial_mass;



    void Awake(){
        if (rb != null)
        {
        initial_mass = rb.mass;
        }
    }

    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
    }

    public void OnTriggerStay2D(Collider2D collision) 
    {
        if (collision.tag == "Player" && player.Actions.isPushing == true) 
        {
           isBeingPushed = true;
           ReleaseMass();
           player.Actions.PushAnim();

           if (gameObject.CompareTag("Tree"))
           {
            animator.SetBool("PushingTree", true);
           }
        }
    }


    public void ReleaseMass()
    { 
        if (rb != null)
        {
        rb.mass = null_mass;
        }
    }

    public void ResetMass()
    {
        if (rb != null)
        {
        isBeingPushed = false;
        rb.mass = initial_mass;
        }
    }
}

