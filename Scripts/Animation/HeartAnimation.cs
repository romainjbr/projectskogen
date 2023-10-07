using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartAnimation : MonoBehaviour
{

    public Animator animator;

    public int currentHealth = 40; 


    public void LoseHealth(int damageAmount){
        animator.SetBool("Damages", true);
        animator.SetFloat("Damage", damageAmount);
    }

    public void GainHealth(int healthAmount){
        animator.SetBool("Health", true);
        animator.SetFloat("Damage", healthAmount);
    }

    public void OnHeartAnimationDone()
    {
        animator.SetBool("Damages", false);
    }

    public void OnHeartAnimationDoneHealth()
    {
        animator.SetBool("Health", false);
    }
}
