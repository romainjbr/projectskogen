using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class PlayerReferences 
{
    [SerializeField]
    private GameObject[] weaponObjects;

    public GameObject[] WeaponObjects {get => weaponObjects; set => weaponObjects = value;} 

}
