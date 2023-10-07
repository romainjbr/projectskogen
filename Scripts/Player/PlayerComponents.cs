using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerComponents 
{
    [SerializeField] private Rigidbody2D rigidBody; 
    [SerializeField] private Collider2D collider;
    [SerializeField] private AnyStateAnimator animator; 

    [SerializeField] private LayerMask groundLayer;




    public Rigidbody2D RigidBody
    {

        get
        {

            return rigidBody;
        }
        
    }

    public AnyStateAnimator Animator
        {

            get
            {

                return animator;
            }
            
        }

    public LayerMask GroundLayer
        {

            get
            {

                return groundLayer;
            }
            
        }

    public Collider2D Collider
        {

            get
            {

                return collider;
            }
            
        }

}
