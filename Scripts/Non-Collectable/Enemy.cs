using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy: MonoBehaviour, ICollisionHandler, IHitable
{

    [SerializeField] Animator animator;
    [SerializeField] RuntimeAnimatorController standingWitch;
    [SerializeField] RuntimeAnimatorController flyingWitch;

    private Transform target;
    private Transform shootTarget;
    [SerializeField] private Transform shootFromPoint;


    [SerializeField] private GameObject objectShoot;
    [SerializeField] GameObject spawnedObject;


    [SerializeField] public AudioSource audioSource1;
    [SerializeField] public AudioSource audioSource2;
    [SerializeField] public AudioSource audioSource3;

    public Vector3 projectileDirection;

    public int enemyMaxHealth = 3;
    public int enemyCurrentHealth;

    [SerializeField] private float speed;
    [SerializeField] private float projectileSpeed;
    float inputHorizontal;
    float directionToTarget;
    float timer;
    float damTimer;

    bool facingRight = true;
    public bool flying;
    private bool movingToTarget = false; 


    void Start()
    {
        enemyCurrentHealth = enemyMaxHealth;

        if (flying){
            animator.runtimeAnimatorController = flyingWitch;
        }

        else{
            animator.runtimeAnimatorController = standingWitch;
        }

         timer = 0;
         damTimer = 0;
    }

    void Update()
    {
        LookAtTarget();
        timer += Time.deltaTime;
        damTimer += Time.deltaTime;
        
    }

    private void LookAtTarget()
    {
        if (target != null)
        {
            if (target.transform.position.x > (gameObject.transform.position.x + 5) || target.transform.position.x < (gameObject.transform.position.x - 5) )
            {
            Vector3 destination = movingToTarget ? target.position : transform.position;


            (float x, float y, float z) target_values = (target.position.x, transform.position.y, transform.position.z);
            Vector3 destin_pos = new Vector3(target_values.x, target_values.y, target_values.z);
            transform.position = Vector2.MoveTowards(transform.position, destin_pos, Time.deltaTime * speed);
            }
        if (!flying){
            if (target.transform.position.x < (gameObject.transform.position.x) && !facingRight)
                {
                    Flip ();
                }

            else if (target.transform.position.x > (gameObject.transform.position.x) && facingRight)
            {
                    Flip ();
                }
            }
        }    
    }

    void Flip(){

        facingRight = !facingRight;
        float movementOffset;
        float previousDirection;

        if (!flying)
            {
            Vector3 tmpScale = gameObject.transform.localScale;
            tmpScale.x *= -1;
            gameObject.transform.localScale = tmpScale;
            
            previousDirection = transform.localScale.x;
            movementOffset = 8f;
            Vector3 positionOffset = new Vector3(previousDirection * movementOffset, 0, 0);
            transform.position += positionOffset;
            }
    }


    private int HurtSound = 0;

    public void CollisionEnter(string colliderName, GameObject other, bool isEnter)
    {

            if (colliderName == "DamageArea") 
                {
                    if (isEnter){

                        if (other.CompareTag("Hit")){
                            enemyCurrentHealth--;
                            animator.SetBool("Hurt", true);

                            if(enemyCurrentHealth >= 0)
                            {
                                if (HurtSound%2 ==0)
                                {
                                audioSource1.Play();
                                }
                                else {
                                audioSource2.Play();
                                }

                                HurtSound++;
                                
                            }

                            if(enemyCurrentHealth == 0)
                                {
                                    animator.SetBool("Dead", true);
                                    audioSource3.Play();
                                }
                            }
                        

                            if(other.CompareTag("Player"))
                                {
                                other.GetComponent<Player>().TakeDamage(1);
                                }
                            }

                    else if (!isEnter && other.CompareTag("Player"))
                        {
                            if (damTimer > 2.0f)
                            {
                                other.GetComponent<Player>().TakeDamage(1);
                                damTimer = 0;

                            }
                        }
                    }
        
            if (colliderName == "SightArea" && other.tag == "Player"){
                    if (target == null)
                    {
                        this.target = other.transform;
                    }
            }

            if (colliderName == "AttackArea" && other.tag == "Player"){
                animator.SetBool("Attack", true);
                audioSource2.Play();

                }

            if (colliderName == "ShootArea" && other.tag == "Player"){

                if (timer > 3.0f){

                    shootTarget = other.transform;
                    animator.SetBool("Attack", true);
                    throwProjectile(shootTarget);
                    timer = 0;
                }
                }
        }

        public void CollisionExit(string colliderName, GameObject other)
        {
            if (colliderName == "SightArea" && other.tag == "Player")
            {
                target = null;
            }
        }

        public void DestroyWitch()
        {
            Destroy(gameObject);
        }

        public void Drop(){
            Vector3 current_pos = new Vector3(transform.position.x, transform.position.y + 3, transform.position.z);
            GameObject spawned = (GameObject)Instantiate(spawnedObject, current_pos, transform.rotation);
        }

        public void OnAnimationDone()
        {
            animator.SetBool("Hurt", false);
            animator.SetBool("Attack", false);

        }
    
        public void TakeHit()
        {
        
        }

        public void throwProjectile(Transform shootTarget)
        {
            GameObject go = Instantiate(objectShoot, shootFromPoint.position, Quaternion.identity);
            projectileDirection = shootTarget.position - go.transform.position;
            go.GetComponent<Projectile>().Move(projectileDirection);
        }

}
