using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public enum RIG {BODY, ARMS, STARS}; 

public class AnyStateAnimation 
{

    public RIG AnimationRig { get; private set; }

    public string[] HigherPriority {get; set;}
    
    public string Name {get; set;}

    public bool Active {get; set;}

    public AnyStateAnimation(RIG rig, string Name, params string[] higherPriority)
    {
        this.AnimationRig = rig;
        this.Name = Name;
        this.HigherPriority = higherPriority;
    }
}


