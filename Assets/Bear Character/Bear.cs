using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bear : MonoBehaviour
{
    [SerializeField] float m_speed = 4.0f;
    [SerializeField] float m_jumpForce = 7.5f;

    private Animator m_animator;
    private Rigidbody2D m_body2d;
    //private Sensor_Bandit m_groundSensor;
    //private bool m_grounded = false;
    //private bool m_combatIdle = false;
    private bool m_isDead = false;
    private float jump_cooldown = 3.0f;
    private float time_since_jump = 3.0f; 

    public Transform attackPoint;
    public LayerMask enemyLayers;

    public float attackRange = 0.5f;
    public int attackDamage = 40;

    // Use this for initialization
    void Start()
    {
        m_animator = GetComponent<Animator>();
        m_body2d = GetComponent<Rigidbody2D>();
        //m_groundSensor = transform.Find("GroundSensor").GetComponent<Sensor_Bandit>();
    }

    // Update is called once per frame
    void Update()
    {
        time_since_jump += Time.deltaTime; 

        // -- Handle input and movement --
        float inputX = Input.GetAxis("Horizontal");

        if(m_animator.GetBool("IsDead") != true)
        {
            // Swap direction of sprite depending on walk direction
            if (inputX < 0)
                transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
            else if (inputX > 0)
                transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);

            // Move
            m_body2d.velocity = new Vector2(inputX * m_speed, m_body2d.velocity.y);
        }
        

        

        // -- Handle Animations --
        //Death
        if (Input.GetKeyDown("k"))
        {
            if (!m_isDead)
                m_animator.SetBool("IsDead", true); 
            else
                m_animator.SetBool("IsDead", false); 

            m_isDead = !m_isDead;
        }

        //Hurt
        else if (Input.GetKeyDown("h"))
            m_animator.SetTrigger("Hurt");

        //Attack
        else if (Input.GetMouseButtonDown(0))
        {
            Attack();
        }


        //Jump
        else if (Input.GetKeyDown("space"))
        {
            if(time_since_jump > jump_cooldown)
            {
                time_since_jump = 0;
                //m_animator.SetTrigger("Jump");
                //m_grounded = false;
                //m_animator.SetBool("Grounded", m_grounded);
                if (m_animator.GetBool("IsDead") != true)
                {
                    m_body2d.velocity = new Vector2(m_body2d.velocity.x, m_jumpForce);
                }

                //m_groundSensor.Disable(0.2f);
            }
        }

        //Run
        else if (Mathf.Abs(inputX) > Mathf.Epsilon)
            m_animator.SetInteger("AnimState", 1);

        
        //Idle
        else
            m_animator.SetInteger("AnimState", 0);
        
    }

    void Attack()
    {
        m_animator.SetTrigger("Attack");

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        foreach(Collider2D enemy in hitEnemies)
        {
            //Debug.Log("We hit " + enemy.name);
            enemy.GetComponent<EnemyHealth>().TakeDamage(attackDamage);
        }
    }

    

    void OnDrawGizmosSelected()
    {
        if(attackPoint == null)
        {
            return;
        }
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
