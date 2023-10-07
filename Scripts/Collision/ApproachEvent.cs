using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApproachEvent : MonoBehaviour
{

    Player player;

    public Animator animator;

    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        
    }

    public void OnTriggerEnter2D(Collider2D collision) {
        
        if (collision.tag == "Player"){


            if (gameObject.tag == "MessageBoxTrigger")
            {
                MessageBoxScript.Instance.WhichBox(gameObject.name);
            }

            else if (gameObject.name == "EndOfBroom")
            {
                player.Actions.TransitionOutBroom();
            }
    
            else {
                    animator.SetBool("Approaching", true);
            }
        }
    }

    public void OnTriggerExit2D(Collider2D collision) {

        if (collision.tag == "Player"){
            animator.SetBool("Approaching", false);

            if (gameObject.tag == "MessageBoxTrigger")
            {
                animator.SetBool("QuitBox", true);
                animator.SetBool("Box_Appear", false);
                // gameObject.SetActive(false);
                Destroy(gameObject);
            }

        }
        
    }
}
