using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup_Health : MonoBehaviour
{
    private int hp = 1;

    void OnTriggerEnter(Collider coll){
        if(coll.gameObject.tag == "Player")
        {
            coll.GetComponent<Player>().Heal(hp);
            print("PICKED UP: Healing player by -> + " + hp.ToString());
            Destroy(gameObject);
        }
    }

}
