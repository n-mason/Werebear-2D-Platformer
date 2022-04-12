using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public Animator animator;
    public int maxHealth = 500;
    int currentHealth;
    private Rigidbody2D rb;
    public bool isDead = false;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    public void PlayerTakeDamage(int damage)
    {
        currentHealth -= damage;
        

        if(isDead == false)
        {
            // Play hurt animation
            animator.SetTrigger("Hurt");
            Debug.Log("Current health:" + currentHealth);
        }
        
        if (currentHealth <= 0)
        {
            Die();
            isDead = true;
        }

        void Die()
        {
            // Die animation
            animator.SetBool("IsDead", true);

            // Need to create code for end game screen

            Debug.Log("Player died!");
        }
    }
}
