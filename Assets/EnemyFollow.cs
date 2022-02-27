using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
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
}
