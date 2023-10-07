using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class Player : MonoBehaviour
{

    [SerializeField] private PlayerStats stats;
    [SerializeField] private PlayerComponents components;
    [SerializeField] private PlayerReferences references; 
    [SerializeField] private PlayerUtilities utilities;
    private PlayerActions actions;

    
    [SerializeField] public AudioSource audioSource1;
    [SerializeField] public AudioSource audioSource2;
    [SerializeField] public AudioSource audioSource3;
    [SerializeField] public AudioSource audioSource4;
    [SerializeField] public AudioSource audioSource5;
    private PlayAudio playAudio;


    [SerializeField] public HealthBar healthBar;
    HeartAnimation heartAnimation;


    [SerializeField] public GameObject heart;
    [SerializeField] public Transform throwFrom;
    [SerializeField] public GameObject throwableObject;
    public GameObject otherGameObject;


    public int maxHealth = 30;
    public int currentHealth;

    public bool canInput;

    public PushableObject pushableObject; 

    

    public PlayAudio PlayAudio
    {
        get
        {

            return playAudio;
        }
        
    }


    public PlayerComponents Components
    {
        get
        {

            return components;
        }
        
    }

  public PlayerReferences References
    {
        get
        {

            return references;
        }
        
    }


    public PlayerStats Stats
    {
        get
        {

            return stats;
        }
        
    }

     public PlayerUtilities Utilities
    {
        get
        {

            return utilities;
        }
        
    }

    public PlayerActions Actions
    {
        get
        {

            return actions;
        }
        
    }

    public float timer;



    private void Awake()
    {
        stats.Weapons.Add(WEAPON.FIST, true);
        stats.Weapons.Add(WEAPON.STICK, false);
        stats.Weapons.Add(WEAPON.STONE, false);
        stats.Weapons.Add(WEAPON.KNIFE, false);
        stats.Weapons.Add(WEAPON.SWORD, false);
        stats.Weapons.Add(WEAPON.AXE, false);
        stats.Weapons.Add(WEAPON.BREAD, false);

        heartAnimation = heart.GetComponent<HeartAnimation>();
        canInput = true;
    }


    private void Start()
    {

        actions = new PlayerActions(this);
        utilities = new PlayerUtilities(this);
        playAudio = new PlayAudio(this);
        
        currentHealth = maxHealth;
        timer = 0;
        stats.Speed = stats.WalkSpeed;

        AnyStateAnimation[] animations = new AnyStateAnimation[]
        {
            new AnyStateAnimation(RIG.BODY, "Body_Idle", "Body_Attack", "Body_Jump", "Body_Flying", "Broom_Transition", "Broom_Out_Transition", "Death_Standing", "last_anim"),
            new AnyStateAnimation(RIG.BODY, "Body_Walk", "Body_Jump",  "Body_Flying", "Broom_Transition", "Broom_Out_Transition", "Death_Standing", "last_anim"),
            new AnyStateAnimation(RIG.BODY, "Body_Jump", "Body_Flying", "Broom_Transition", "Broom_Out_Transition", "Death_Standing", "last_anim"),
            new AnyStateAnimation(RIG.BODY, "Body_Fall", "Body_Flying", "Broom_Transition","Broom_Out_Transition", "Death_Standing", "last_anim"),
            new AnyStateAnimation(RIG.BODY, "Body_Attack","Body_Fall","Body_Jump","Body_Walk", "Death_Standing", "last_anim"),
            new AnyStateAnimation(RIG.BODY, "Body_Flying", "Broom_Transition", "Broom_Out_Transition", "Death_Flying"),
            new AnyStateAnimation(RIG.BODY, "Body_Flying_Move", "Broom_Transition","Broom_Out_Transition","Death_Flying"),
            new AnyStateAnimation(RIG.BODY, "Broom_Transition"),
            new AnyStateAnimation(RIG.BODY, "Broom_Out_Transition"),
            new AnyStateAnimation(RIG.BODY, "Death_Standing", "last_anim"),
            new AnyStateAnimation(RIG.BODY, "Death_Flying"),
            new AnyStateAnimation(RIG.BODY, "last_anim"),

            new AnyStateAnimation(RIG.ARMS, "Arms_Idle", "Arms_Jump",  "Arms_Push","Arms_Flying", "Arms_Attack", "Broom_Transition_Arms"),
            new AnyStateAnimation(RIG.ARMS, "Arms_Walk", "Arms_Jump", "Arms_Push", "Arms_Flying", "Arms_Attack", "Broom_Transition_Arms"),
            new AnyStateAnimation(RIG.ARMS, "Arms_Jump", "Arms_Attack", "Broom_Transition_Arms"),
            new AnyStateAnimation(RIG.ARMS, "Arms_Fall", "Arms_Attack", "Arms_Flying", "Broom_Transition_Arms","Broom_Out_Transition_Arms"),
            new AnyStateAnimation(RIG.ARMS, "Arms_Attack", "Broom_Transition_Arms"),
            new AnyStateAnimation(RIG.ARMS, "Arms_Push", "Arms_Flying", "Broom_Transition_Arms"),
            new AnyStateAnimation(RIG.ARMS, "Arms_Flying","Arms_Attack", "Broom_Transition_Arms"),
            new AnyStateAnimation(RIG.ARMS, "Broom_Transition_Arms"),


            new AnyStateAnimation(RIG.STARS, "Stars_Hurt")
        };
        
        components.Animator.AddAnimations(animations);

    }

    private void Update()
    {

        if (canInput){
            utilities.HandleInput();   
            Utilities.HandleAir();
            utilities.FallsBack();
            actions.isMoving();
        }

        timer += Time.deltaTime;
    }


    public void TakeDamage(int damage)
    {
        Components.Animator.TriggerHurt();
        Components.Animator.TryPlayAnimation("Stars_Hurt");

        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
        heartAnimation.LoseHealth(currentHealth);
        
        if (currentHealth <= 0){
            string deathAnimation = Actions.isFlying ? "Death_Flying" : "Death_Standing";
            Components.Animator.TryPlayAnimation(deathAnimation);
        }
    }

    public void TakeHealth()
    {
        audioSource4.Play();
        currentHealth = currentHealth+1; 
        heartAnimation.GainHealth(currentHealth);
        healthBar.SetHealth(currentHealth); 

    }

    private void FixedUpdate()
    {
        actions.GetsMove(transform);

    } 

    private void OnTriggerEnter2D(Collider2D collision)
    {
        actions.Collide(collision);
    }

    public void LastAnim()
    {
        canInput = false;
        Components.Animator.TryPlayAnimation("last_anim");
    }
}
