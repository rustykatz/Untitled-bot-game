using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyHealth : MonoBehaviour {

    [SerializeField] private HealthBar hp; 
    private float totalHealth; 
    private float health;
    private float hperc; 
  
    // public GameHandler gameHandler;
    GameObject gameHandler;

    public int points = 5;

	void Awake(){
        points = 5;
        health = 10;
        totalHealth = health;
        print("Health: " + health.ToString());
        print("Total Health: " + totalHealth.ToString());
      
        gameHandler = GameObject.FindGameObjectWithTag("Game_Handler");
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
            hp.SetSize(0);
            OnDeath(points);
            Destroy(gameObject);
        }
        
        print("Enemy HP: " + health.ToString()); 
        
    }

    void OnDeath(int points){
        gameHandler.GetComponent<GameHandler>().AddScore(points);

    }


}
