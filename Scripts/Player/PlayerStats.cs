using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class PlayerStats 
{
    public Vector2 Direction{get; set;} 

    [SerializeField] private float walkSpeed;
    [SerializeField] private float jumpForce;

    public float Speed{get; set;}

    private WEAPON weapon;

    public float WalkSpeed{
        get 
        {
            return walkSpeed;
        }
    }

    public float JumpForce{
        get 
        {
            return jumpForce;
        }
    }

    public WEAPON Weapon { get => weapon; set => weapon = value; } 

    public Dictionary<WEAPON, bool> Weapons {get; set;} = new Dictionary<WEAPON, bool>();

}
  