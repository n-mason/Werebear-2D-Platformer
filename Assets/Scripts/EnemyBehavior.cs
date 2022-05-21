using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    #region Public Variables
    public Transform rayCast;
    public LayerMask raycastMask;
    public float rayCastLength;
    public float attackDistance; //Minimum distance for attack
    public float moveSpeed;
    public float timer; ///Timer for cooldown between attacks
    
    
    //Variables for damaging player
    public float attackRange = 0.5f;
    public int enem_attackDamage = 100;
    #endregion

    #region Private Variables
    private RaycastHit2D hit;
    private GameObject target;
    private Animator anim;
    private float distance;
    private bool attackMode;
    private bool inRange;
    private bool cooling;
    private float intTimer;
    private bool enDead = false;
    #endregion


    void Awake()
    {
        intTimer = timer;
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (!enDead)
        {
            if (inRange)
            {
                if (gameObject.transform.rotation.eulerAngles.y == 0)
                {
                    hit = Physics2D.Raycast(rayCast.position, Vector2.right, rayCastLength, raycastMask);
                }
                else
                {
                    hit = Physics2D.Raycast(rayCast.position, Vector2.left, rayCastLength, raycastMask);
                }
                    
                RaycastDebugger();
            }

            if (hit.collider != null) //Player is encountered
            {
                EnemyLogic();
            }
            else if (hit.collider == null)
            {
                inRange = false;
            }

            if (inRange == false)
            {
                anim.SetBool("Moving", false);
                StopAttack();
            }
        }
    }

    void OnTriggerEnter2D(Collider2D trig)
    {
        if(trig.gameObject.tag == "Player")
        {
            target = trig.gameObject;
            inRange = true;
        }
    }

    void EnemyLogic()
    {
        distance = Vector2.Distance(transform.position, target.transform.position);

        if(distance > attackDistance)
        {
            Move();
            StopAttack();
        }
        else if(attackDistance >= distance && cooling == false)
        {
            Attack();
        }

        
        if (cooling)
        {
            Cooldown();
            anim.SetBool("Attack", false);
        }
    }

    void Move()
    {
        anim.SetBool("Moving", true);
        if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Skeleton_attack1"))
        {
            Vector2 targetPosition = new Vector2(target.transform.position.x, transform.position.y);

            transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
        }
    }

    void Attack()
    {
        timer = intTimer; //Reset timer when player enters attack range
        attackMode = true; //Check if enemy can attack

        anim.SetBool("Moving", false);
        anim.SetBool("Attack", true);
        
    }

    void Cooldown()
    {
        timer -= Time.deltaTime;

        if(timer <= 0 && cooling && attackMode)
        {
            cooling = false;
            timer = intTimer;
        }
    }

    void StopAttack()
    {
        cooling = false;
        attackMode = false;
        anim.SetBool("Attack", false);
    }

    
    void RaycastDebugger()
    {
        //Debug.Log(gameObject.transform.rotation.eulerAngles.y);
        
        if(gameObject.transform.rotation.eulerAngles.y == 0)
        {
            if (distance > attackDistance)
            {
                Debug.DrawRay(rayCast.position, Vector2.right * rayCastLength, Color.red);
            }
            else if (attackDistance > distance)
            {
                Debug.DrawRay(rayCast.position, Vector2.right * rayCastLength, Color.green);
            }
        }
        else
        {
            if (distance > attackDistance)
            {
                Debug.DrawRay(rayCast.position, Vector2.left * rayCastLength, Color.red);
            }
            else if (attackDistance > distance)
            {
                Debug.DrawRay(rayCast.position, Vector2.left * rayCastLength, Color.green);
            }
        }
    }
    

    public void TriggerCooling()
    {
        cooling = true;
    }

    public void EnemyDead()
    {
        Debug.Log("Calling EnemyDead");
        enDead = true;
        this.enabled = false;
    }

    /* //Old Method for following the player
    [SerializeField]
    Transform player;

    [SerializeField]
    float agroRange;

    [SerializeField]
    float moveSpeed;

    private Rigidbody2D rb2d;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        //check distance to player
        float distToPlayer = Vector2.Distance(transform.position, player.position);

        if(distToPlayer < agroRange)
        {
            //chase the player
            ChasePlayer();
            animator.SetBool("Moving", true);
        }
        else
        {
            //stop chasing player
            StopChasingPlayer();
            animator.SetBool("Moving", false);
            
        }
        
    }

    private void ChasePlayer()
    {
        if(transform.position.x < player.position.x)
        {
            //enemy is to the left of player so move right
            rb2d.velocity = new Vector2(moveSpeed, rb2d.velocity.y);
            transform.localScale = new Vector2(2, 2);
        }
        else
        {
            //enemy is to the right of player so move left
            rb2d.velocity = new Vector2(-moveSpeed, rb2d.velocity.y);
            transform.localScale = new Vector2(-2, 2);
        }
    }
    private void StopChasingPlayer()
    {
        rb2d.velocity = Vector2.zero;
    }
    */
}
