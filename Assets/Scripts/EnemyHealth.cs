using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyHealth : MonoBehaviour {

    [SerializeField] private HealthBar hp; 
    private float totalHealth; 
    private float health;
    private float hperc; 
  
	void Awake(){
        health = 10;
        totalHealth = health;
        print("Health: " + health.ToString());
        print("Total Health: " + totalHealth.ToString());
    }

    // Damage handling 
    public void TakeDamage(float damage)
    {
        if(health > 0.01){
            health -= damage;
            hperc = health/ totalHealth + 0.05f;
            hp.SetSize(hperc);
        }
        else
        {
            Destroy(gameObject);
            hperc = 0;
            hp.SetSize(hperc);
        }
        
        print("Enemy HP: " + health.ToString()); 
        
        // Update HP bar
        // hp.SetSize(hperc);
    }

}
