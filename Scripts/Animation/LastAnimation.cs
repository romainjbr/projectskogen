using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class LastAnimation : MonoBehaviour
{
    public Animator animator;
    public Animator animator2;
    public CinemachineVirtualCamera vcam;
    Player player;


    private void OnTriggerEnter2D(Collider2D collision) {

        if (collision.tag == "Player"){
            animator.SetBool("Start", true);
            animator2.SetBool("Start", true);
            vcam.gameObject.SetActive(false);
            collision.GetComponent<Player>().LastAnim();
        }
    }
    
    private void TriggerEndGame(){
        PauseMenuScript.Instance.EndGame();
    }
}
