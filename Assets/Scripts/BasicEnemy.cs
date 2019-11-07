﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEngine;

// Base Class 
public class BasicEnemy : MonoBehaviour
{
    public NavMeshAgent navAgent;
    public GameObject target;
    public float impulseForce = 2.0f;
    public float maxDistance;

    public int dmg = 1; 
    float damageTimer = 0; 
    bool canAttack = true;

    Vector3 knockBack; 
    Vector3 targetPos; 



    // Start is called before the first frame update
    void Awake()
    {
        NavAgentSetup();

    }

    // Update is called once per frame
    void Update()
    {
        CheckRange();
        if(canAttack == false){
            damageCooldown();
        }
    }

    public void NavAgentSetup(){
        navAgent = GetComponent<NavMeshAgent>();
        target = GameObject.FindGameObjectWithTag("Player");
    }

    public bool CheckRange(){
        if(Vector3.Distance(transform.position,  target.transform.position) < maxDistance){
            navAgent.destination = target.transform.position;
            // print("BASIC ENEMY: Target in range. Moving towards Target.");
            return true;
        }
        else{
            // print("BASIC ENEMY: Target not in range.");
            return false;

        }
    }
    void damageCooldown(){
        damageTimer += Time.deltaTime;
        if(damageTimer > 0.5){
            canAttack = true; 
            damageTimer = 0;
            // print("can attack");
        }
        
        
    }

     // Check collision with player
    void OnCollisionEnter(Collision collision)
    {
        if( collision.gameObject.tag == "Player")
        {
            gameObject.GetComponent<Rigidbody>().AddForce(transform.forward * impulseForce * 2, ForceMode.Impulse);
            

            if(canAttack == true){
                print("Damaging Player");
                collision.gameObject.GetComponent<Player>().TakeDamage(dmg);
                canAttack = false;
           }
           
        }
    }

    void OnTriggerEnter(Collider coll)
    {
       if (coll.gameObject.CompareTag("Projectile"))
       {
           Destroy(coll.gameObject);
       }
    }

 
}
