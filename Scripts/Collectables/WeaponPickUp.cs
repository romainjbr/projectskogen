using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickUp : MonoBehaviour, ICollectable
{

    [SerializeField] private WEAPON weapon;

    [SerializeField] Animator animator;


    public WEAPON Weapon { get => weapon; set => weapon = value; }
   
    public void Collect() 
    {   
        animator.SetBool(gameObject.name, true);
        FindObjectOfType<Player>().Actions.PickUpWeapon(weapon);
        Destroy(gameObject); 
        UIManager.Instance.AddWeaponInBar(weapon);
    }

}
