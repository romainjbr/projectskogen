using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUtilities
{

    Player player;    
    private List<Command> commands = new List<Command>();

    public PlayerUtilities(Player player) 


    {
        this.player = player;

        commands.Add(new JumpCommand(player, KeyCode.Space));
        commands.Add(new AttackCommand(player, KeyCode.K));
        commands.Add(new PushCommand(player, KeyCode.P));
        commands.Add(new OKCommand(player, KeyCode.Return));


        commands.Add(new WeaponSwapCommand(player, WEAPON.FIST, KeyCode.Alpha1));
        commands.Add(new WeaponSwapCommand(player, WEAPON.STICK, KeyCode.Alpha2));
        commands.Add(new WeaponSwapCommand(player, WEAPON.STONE, KeyCode.Alpha3));
        commands.Add(new WeaponSwapCommand(player, WEAPON.KNIFE, KeyCode.Alpha4));
        commands.Add(new WeaponSwapCommand(player, WEAPON.SWORD, KeyCode.Alpha5));
        commands.Add(new WeaponSwapCommand(player, WEAPON.AXE, KeyCode.Alpha6));
        commands.Add(new WeaponSwapCommand(player, WEAPON.BREAD, KeyCode.Alpha7));


    }

    public void HandleInput()
    {
        player.Stats.Direction = new Vector2 (Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")); // so does not change in middle, either 0 or 1, as if joystick



        foreach (Command command in commands)
        {
            if (Input.GetKeyDown(command.Key)) 
            {
                command.GetKeyDown(); 
            }

            if (Input.GetKeyUp(command.Key)) 
            {
                command.GetKeyUp();  
            }

            if (Input.GetKey(command.Key)) 
            {
                command.GetKey(); 
            }
        }    
    }

    public bool IsGrounded()
    {
        RaycastHit2D hit = Physics2D.BoxCast(player.Components.Collider.bounds.center, player.Components.Collider.bounds.size, 0, Vector2.down, 0.1f, (player.Components.GroundLayer));
        
        return hit.collider != null; 
    }


    public bool FallsBack()
    {
        RaycastHit2D hit = Physics2D.BoxCast(player.Components.Collider.bounds.center, player.Components.Collider.bounds.size, 0, Vector2.down, 2f, player.Components.GroundLayer);
        
        return hit.collider != null;
    }

    public void HandleAir()
    {
        if (IsFalling())
        {
            player.Components.Animator.TryPlayAnimation("Body_Fall");
            player.Components.Animator.TryPlayAnimation("Arms_Fall");

            if (FallsBack()){
                player.PlayAudio.FallingSound();
            }
            
        }

        if (player.Actions.isFlying){
            player.Components.Animator.SetFlying(true);
            player.Components.RigidBody.gravityScale = 0;

        }

        else if (!player.Actions.isFlying)
        {
            player.Components.Animator.SetFlying(false);
            player.Components.RigidBody.gravityScale = 5;
        }


    }

    public bool IsFalling()
    {
        if (player.Components.RigidBody.velocity.y < 0 && !IsGrounded())
        {
            return true;
        }
        else{
            return false;
        }
        
    }

    public bool IsJumping()
        {
            if (player.Components.RigidBody.velocity.y > 0 && !IsGrounded())
            {
                return true;
            }
            else{
                return false;
            }
            
        }
 
}