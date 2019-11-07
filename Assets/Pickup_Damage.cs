using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup_Damage : MonoBehaviour
{
    [SerializeField] private int dmg = 1;

    void OnTriggerEnter(Collider coll){
        if(coll.gameObject.tag == "Player")
        {
            coll.GetComponent<Player>().BoostDamage(dmg);
            print("PICKED UP: Damage boost player by -> + " + dmg.ToString());
            Destroy(gameObject);
        }
    }
}
