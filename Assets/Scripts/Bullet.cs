using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Bullet : MonoBehaviour {

    private GameObject _Enemy;
    //Set how much damage for enemy to take
    public int dmg = 2; 
    public int speed = 10;

    // public float timer = 0f;
	// Use this for initialization
	void Awake () {
        
	}
	
	// Update is called once per frame
	void Update () {
        // timer += 1; 
        //checkDecay(timer);
        
	}

    void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.CompareTag("Enemy"))
        {
            _Enemy = GameObject.FindGameObjectWithTag("Enemy");
            // Accesses enemy health variable and applys damage to it 
            _Enemy.GetComponent<EnemyHealth>().TakeDamage(dmg);

            //Destroy(coll.gameObject);
            Destroy(gameObject);

        }
    }
    
    // void checkDecay(timer){
    //     if(timer >= 60){
    //         Destroy(gameObject);
            
    //     }
    // }



}
