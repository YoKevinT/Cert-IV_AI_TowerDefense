using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Enemy : MonoBehaviour
{
    public int maxHealth = 100;
    public Transform target;

    private NavMeshAgent agent;
    private int health = 0;

    void Start()
    {
        // Set health to whatever maxHealth is at start
        health = maxHealth;
        // Get NavMeshAgent component
        agent = GetComponent<NavMeshAgent>();
        // Follow destination
        agent.SetDestination(target.position);
    }

    // Call this function to make Enemy take damage
    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}