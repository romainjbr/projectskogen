using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AnyStateAnimator : MonoBehaviour
{
    private Animator animator;

    [SerializeField] public GameObject throwable;

    Player player;

    private Dictionary<string, AnyStateAnimation> animations = new Dictionary<string, AnyStateAnimation>();

    private string currentAnimationArms = string.Empty;
    private string currentAnimationBody = string.Empty;
    private string currentAnimationStars = string.Empty;

    float timer;

    public bool animOver = false;

    private void Awake(){
        this.animator = GetComponent<Animator>();
        player = GameObject.Find("Player").GetComponent<Player>();
    }


    private void Update(){
        Animate();

        if (Input.GetKeyDown(KeyCode.K))
        {
            animator.SetTrigger("TriggerAttack");
        }
    }

    public void AddAnimations(params AnyStateAnimation[] newAnimations) 
    {
        for (int i = 0; i < newAnimations.Length; i++)
        {
            this.animations.Add(newAnimations[i].Name, newAnimations[i]);
        }

    }
    public void TryPlayAnimation(string newAnimation) 
    {
        switch(animations[newAnimation].AnimationRig)
        {
            case RIG.BODY:
                PlayAnimation(ref currentAnimationBody);
                break;
            case RIG.ARMS:
                PlayAnimation(ref currentAnimationArms);
                break;
            case RIG.STARS:
                PlayAnimation(ref currentAnimationStars);
                break;
        }

        void PlayAnimation(ref string currentAnimation)
        {
            if (currentAnimation == "") 
            {
                animations[newAnimation].Active = true;
                currentAnimation = newAnimation;
            }

            else if (currentAnimation != newAnimation && !animations[newAnimation].HigherPriority.Contains(currentAnimation) || !animations[currentAnimation].Active)
            {
                animations[currentAnimation].Active = false;
                animations[newAnimation].Active = true;
                currentAnimation = newAnimation;
                
            }
        }    
    }

    public void SetWeapon(float weapon)
    {
        animator.SetFloat("Weapon", weapon);
    }


    private void Animate()
    {
        foreach (string key in animations.Keys)
        {
            animator.SetBool(key, animations[key].Active);   
       }
    }

    public void OnAnimationDone(string animation)
    {
        animations[animation].Active = false;

    }

    public void StopAttack()
    {
        player.Actions.isAttacking = false;
    }

    public void TriggerHurt()
    {
        animator.SetTrigger("HurtTrigger");
    }


    public void ThrowtheStone(){

        player.Actions.ThrowStone();
    }

    public void SetFlying(bool val){
        animator.SetBool("Flying", val);
    }

    public void FinishAnim(){
        animOver = true;
        animator.SetBool("Broom_Transition", false);
        animator.SetBool("Broom_Transition_Arms", false);
        animator.SetBool("Arms_Flying", true);

    }

    public void StartJump(){
        player.Components.RigidBody.AddForce(new Vector2(0, player.Stats.JumpForce), ForceMode2D.Impulse);
    }


    public void TryAgainAnim(){
        PauseMenuScript.Instance.TryAgain();
        player.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
    }

    public void StopSound(){
        player.PlayAudio.Moving(false);
    }

}