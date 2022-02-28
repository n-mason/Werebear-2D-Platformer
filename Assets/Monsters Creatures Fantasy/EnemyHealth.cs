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
    //private float timecount = 2;

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
                GameObject.Find("Flying Enemy").GetComponent<AIPath>().enabled = false;
                StartCoroutine(ExecuteAfterTime_F(1)); // set to rigidbody to static after 1 second
            }
            else
            {
                GetComponent<EnemyBehavior>().enabled = false;
                StartCoroutine(ExecuteAfterTime_S(1)); // set to rigidbody to static after 1 second
            }

            

            Debug.Log("Enemy died!");
        }
    }

    IEnumerator ExecuteAfterTime_F(float time)
    {
        yield return new WaitForSeconds(time);

        // Code to execute after the delay
        GameObject.Find("Flying Enemy").GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        BoxCollider2D col = GetComponent<BoxCollider2D>();
        col.size = new Vector2(1f, 0.001f);
    }

    IEnumerator ExecuteAfterTime_S(float time)
    {
        yield return new WaitForSeconds(time);

        // Code to execute after the delay
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        BoxCollider2D col = GetComponent<BoxCollider2D>();
        col.size = new Vector2(1f, 0.001f);

        
        GameObject parent = GameObject.Find("Skeleton");
        for (int i = 0; i < parent.transform.childCount; i++)
        {
            var child = parent.transform.GetChild(i).gameObject;
            if(child != null)
            {
                child.SetActive(false);
            }
        }
        
    }
}
