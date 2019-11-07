using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup_Upgrade : MonoBehaviour
{
    [SerializeField] private int upgrade = 1;

    void OnTriggerEnter(Collider coll){
        if(coll.gameObject.tag == "Player")
        {
            coll.GetComponent<Player>().WeaponUpgrade(upgrade);
            print("PICKED UP: Weapon Upgrade Level by -> + " + upgrade.ToString());
            Destroy(gameObject);
        }
    }
}
