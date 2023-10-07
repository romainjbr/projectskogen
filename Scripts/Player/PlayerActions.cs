using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class PlayerActions 
{
    private Player player;
    public GameObject otherGameObject;
    public GameObject GameObjectTry;

    private HeartAnimation HeartAnimation;

    public PushableObject pushableObject;


    public bool isPushing = false;
    public bool isAttacking = false;
    public bool isFlying = false;



    void Awake(){
        HeartAnimation = otherGameObject.GetComponent<HeartAnimation>();
    }

    public PlayerActions(Player player)
    {
        this.player = player;
    }

    public void GetsMove(Transform transform){
        if(!isFlying){
            Move(transform);
        }
        
        else if (isFlying)
        {
            Fly(transform);
        }
    }

    public void Move(Transform transform)
    {

        float direction = player.Stats.Direction.x;
        Vector2 velocity = new Vector2(direction * player.Stats.Speed * Time.deltaTime, player.Components.RigidBody.velocity.y);
        player.Components.RigidBody.velocity = velocity;

        if (direction != 0)
        {
            float movementOffset = -0.7f;
            float previousDirection = transform.localScale.x;

            if (direction != previousDirection)
            {
                Vector3 positionOffset = new Vector3(previousDirection * movementOffset, 0, 0);
                transform.position += positionOffset;
            }

            transform.localScale = new Vector3(direction < 0 ? -1 : 1, 1, 1);

            if (player.Utilities.IsGrounded())
            {
                player.Components.Animator.TryPlayAnimation("Body_Walk");
                player.Components.Animator.TryPlayAnimation("Arms_Walk");
            }
        }
        else if (player.Components.RigidBody.velocity == Vector2.zero)
        {
            player.Components.Animator.TryPlayAnimation("Body_Idle");
            player.Components.Animator.TryPlayAnimation("Arms_Idle");
        }
    }


    public void Fly(Transform transform)
    {
        float directionX = player.Stats.Direction.x;
        float directionY = player.Stats.Direction.y;
        Vector2 velocity = new Vector2(directionX * player.Stats.Speed * 2 * Time.deltaTime, directionY * player.Stats.Speed * 2 * Time.deltaTime);
        player.Components.RigidBody.velocity = velocity;

        if (directionX != 0)
        {
            float movementOffset = -0.7f;
            float previousDirection = transform.localScale.x;

            if (directionX != previousDirection)
            {
                Vector3 positionOffset = new Vector3(previousDirection * movementOffset, 0, 0);
                transform.position += positionOffset;
            }

            transform.localScale = new Vector3(directionX < 0 ? -1 : 1, 1, 1);

            
            player.Components.Animator.TryPlayAnimation("Body_Flying_Move");
            player.Components.Animator.TryPlayAnimation("Arms_Flying");

        }
        else if (player.Components.RigidBody.velocity == Vector2.zero)
        {
            player.Components.Animator.TryPlayAnimation("Body_Flying");
            player.Components.Animator.TryPlayAnimation("Arms_Flying");

        }

    }

    public void isMoving(){
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)){

            if (!isFlying && player.Utilities.IsGrounded() && player.canInput)
            {
                player.PlayAudio.Moving(true);
            }

            else if (player.Utilities.IsFalling() || player.Utilities.IsJumping() || !player.canInput)
                {
                    player.PlayAudio.Moving(false);
                }  
        }

        else {
            if (isFlying)
            {   
                player.Components.Animator.TryPlayAnimation("Body_Flying");
                player.Components.Animator.TryPlayAnimation("Arms_Flying");
            }
            else if (!isFlying) 
            {
                player.PlayAudio.Moving(false);
            }
        }
    }

    public void TransitionBroom(){
        
        if (!isFlying){
            player.PlayAudio.PlaySound(player.audioSource5);
            player.Components.Animator.TryPlayAnimation("Broom_Transition");
            player.Components.Animator.TryPlayAnimation("Broom_Transition_Arms");
            isFlying = true;
        }

        else {
        player.Components.Animator.TryPlayAnimation("Body_Flying");
        player.Components.Animator.TryPlayAnimation("Arms_Flying");
        }

    }

    public void TransitionOutBroom(){
        if (isFlying){
            player.PlayAudio.PlaySound(player.audioSource5);
            player.Components.Animator.TryPlayAnimation("Broom_Out_Transition");
            player.Components.Animator.TryPlayAnimation("Broom_Transition_Arms");
            isFlying = false;
            
        }
    }

    public void StartTheFly(){
        isFlying = true;
        player.Components.RigidBody.gravityScale = 0;
        player.Components.Animator.TryPlayAnimation("Body_Flying");
        player.Components.Animator.TryPlayAnimation("Arms_Flying");
    }




    public void TrySwapWeapon(WEAPON weapon)
    {
        if (player.Stats.Weapons[weapon] == true) { 
            player.Stats.Weapon = weapon;

            if (player.Stats.Weapon == WEAPON.STONE){
                player.Components.Animator.SetWeapon(2);
            }

            else if (player.Stats.Weapon == WEAPON.FIST)
            {
                player.Components.Animator.SetWeapon(0);
            }

            else {
                player.Components.Animator.SetWeapon(1);
            }

            SwapWeapon();
        } 
    }


    public void SwapWeapon()
    {
        for (int i = 1; i < player.References.WeaponObjects.Length; i++)
        {
            player.References.WeaponObjects[i].SetActive(false);             
        }
        if (player.Stats.Weapon > 0) 
        {            
            player.References.WeaponObjects[(int)player.Stats.Weapon].SetActive(true);
        }
            
           WEAPON selectedWeapon = player.Stats.Weapon;

            foreach (WEAPON weapon in Enum.GetValues(typeof(WEAPON)))
                {
                   if (weapon == selectedWeapon)
                   {
                        UIManager.Instance.SwapWeaponInBar(weapon);
                    } 
                    else if (weapon != selectedWeapon)
                    {
                        UIManager.Instance.UpdateWeapon(weapon, false);
                    }
                }
    }

    public IEnumerable<WEAPON> GetOtherWeapons(WEAPON selectedWeapon)
    {
        return player.Stats.Weapons.Keys.Where(weapon => weapon != selectedWeapon);
    }

    public void TakeHit()
    {

    }


    internal void PickUpWeapon(WEAPON weapon){
        player.Stats.Weapons[weapon] = true;
    }

    public void Jump()
    {
        if (player.Utilities.IsGrounded())
        {        
            player.Components.Animator.TryPlayAnimation("Body_Jump");
            player.Components.Animator.TryPlayAnimation("Arms_Jump");
        }

        
    }

    public void Attack()
    {
        player.Components.Animator.TryPlayAnimation("Arms_Attack");
        player.Components.Animator.TryPlayAnimation("Body_Attack");
        isAttacking = true;
    }

    


    public void Collide(Collider2D collision)
    {
        if (collision.tag == "Collectable")
        {
            collision.GetComponent<ICollectable>().Collect(); 
        }
    }

    public void Push()
    {

        if (isPushing){
            player.pushableObject.ReleaseMass();
        } 
        else if (!isPushing)
        {
            player.pushableObject.isBeingPushed = false;
            player.pushableObject.initial_mass = 23f;
            player.pushableObject.ResetMass();
 
            player.Components.Animator.OnAnimationDone("Arms_Push");
        }
        
    }

    public void PushAnim()
    {
        player.Components.Animator.TryPlayAnimation("Arms_Push");
    }

    public void ThrowStone()
    {
        GameObject stone = GameObject.Instantiate(player.throwableObject, player.throwFrom.position, Quaternion.identity);
        Vector3 direction = new Vector3(player.transform.localScale.x, 0);
        stone.GetComponent<Throwable>().Setup(direction);
    }
}
