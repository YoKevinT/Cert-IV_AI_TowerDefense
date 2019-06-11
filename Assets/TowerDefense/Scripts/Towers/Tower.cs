using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public int damage = 10;
    public float attackRate = 1f;
    public float attackRange = 2f;

    protected Enemy currentEnemy;

    private float attackTimer = 0f;

    void OnDrawGizmosSelected()
    {
        // Draw the attack sphere around Tower
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }

    // Aims at a given enemy every frame
    public virtual void Aim(Enemy e)
    {
        print("I am aiming at '" + e.name + "'");
    }
    // Attacks at a given enemy only when 'attacking'
    public virtual void Attack(Enemy e)
    {
        print("I am attacking '" + e.name + "'");
    }

    void DetectEnemies()
    {
        // Reset current enemy
        currentEnemy = null;
        // Perform OverlapSphere and get the hits
        Collider[] hits = Physics.OverlapSphere(transform.position, attackRange);
        // Loop through everything we hit
        foreach (var hit in hits)
        {
            // If the thing we hit is an enemy
            Enemy enemy = hit.GetComponent<Enemy>();
            if (enemy)
            {
                // Set current enemy to that one
                currentEnemy = enemy;
            }
        }
    }

    // Protected - Accessible to Cannon / Other Tower classes
    // Virtual - Able to change what this function does in derived classes
    protected virtual void Update()
    {
        // Detect enemies before performing attack logic
        DetectEnemies();
        // Count up the timer
        attackTimer += Time.deltaTime;
        // if there's an enemy
        if (currentEnemy)
        {
            // Aim at the enemy
            Aim(currentEnemy);
            // Is attack timer ready?
            if (attackTimer >= attackRate)
            {
                // Attack the enemy!
                Attack(currentEnemy);
                // Reset timer
                attackTimer = 0f;
            }
        }
    }
}