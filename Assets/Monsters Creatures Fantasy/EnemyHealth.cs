using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyHealth : MonoBehaviour
{
    public Animator animator;
    public int maxHealth = 100;
    int currentHealth;
    private Rigidbody2D rb;
    private float timecount = 2;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        // Play hurt animation
        animator.SetTrigger("Hurt");

        if(currentHealth <= 0)
        {
            Die();
        }

        void Die()
        {
            // Die animation
            animator.SetBool("IsDead", true);

            // DIsable the enemy
            //GetComponent<Collider2D>().enabled = false; This will call enemies to fall out of map when they are killed
            this.enabled = false;
            if (transform.parent != null) // check if a parent exists
            {
                //transform.parent = null;
                //GameObject.Find("Flying Enemy").SetActive(false);
                GameObject.Find("Flying Enemy").GetComponent<AIDestinationSetter>().enabled = false; //NEED TO FIGURE OUT WHY FLOATING ON GROUND
                GameObject.Find("Flying Enemy").GetComponent<AIPath>().enabled = false; //NEED TO FIGURE OUT WHY FLOATING ON GROUND
            }
            else
            {
                GetComponent<EnemyFollow>().enabled = false;
            }

            if(timecount > 0)
            {
                timecount -= Time.deltaTime;
            }
            if(timecount <= 0)
            {
                GetComponent<Collider2D>().enabled = false; //change so that rigidboyd gets disabled instead, could also try a "dead" layer so that player cant interact anymore
            }

            Debug.Log("Enemy died!");
        }
    }
}
