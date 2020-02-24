using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// Enemy Class
public class test : MonoBehaviour
{
    public float health = 5f;
    public NavMeshAgent navAgent; 
    public GameObject target;

    void start(){
        navAgent = GetComponent<NavMeshAgent>(); 
        target = GameObject.FindGameObjectWithTag("Player");
    }
    void update(){
        navAgent.destination = target.transform.position;
    }

    // COLLISION CHECK
    void OnTriggerEnter2D(Collider2D collision){
        
        // CHECK COLLISION WITH PROJECTILE
        if(collision.gameObject.tag == "Projectile"){
            // Destroy the object that collides with enemy
            Destroy(gameObject);
            // Decrease enemy health by 1 
            health = health - 1;
        }

        // CHECK COLLISION WITH PLAYER 
        



    }


}
