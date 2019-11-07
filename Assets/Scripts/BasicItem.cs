using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicItem : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnControllerColliderHit(Collision col){ 
        if(col.gameObject.tag == "Player"){
            // Do Something
            print("Collided with Player");
            Destroy(col.gameObject);
        }
    }
}
