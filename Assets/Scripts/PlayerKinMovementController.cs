using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerKinMovementController : MonoBehaviour
{
    public CharacterController controller; 
    // Movement Speed
    public float speed;

    // Gravity acting on the player 
    public float gravity;
    Vector3 velocity; 
    public float mass; 

    // Distance of player to ground
    public Transform groundCheck;
    public float groundDistance;
    public LayerMask groundMask;
    public bool isGrounded;

    // Jump Height
    public float jumpHeight;
    public float gscale = 2f;


    // Gun
    public GameObject bulletPrefab;
    public Transform bulletSpawn;
    public int bulletSpeed = 20; 

    void start(){
        speed = 12f; 
        gravity = -9.81f; 
        mass = 1f; 
        groundDistance = 0.4f;
        jumpHeight =3f;
        gscale = 2f;
    }

    // Update is called once per frame
    void Update()
    {
        // Casts Sphere below player to check if grounded
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if(isGrounded && velocity.y < 0)
        {
            // Force player on ground 
            velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z; 

        controller.Move(move * speed * Time.deltaTime);

        // Jump Control
        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
            print("Jumping");
        }

        if(Input.GetMouseButtonDown(0)){
            Shoot();
        }

        // Gravity
        velocity.y += gravity * gscale* Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
    }

     void Shoot(){
        // Create the Bullet from the Bullet Prefab
        var bullet = (GameObject)Instantiate(
            // Bullet prefab object
            bulletPrefab,
            // Bullet spawn position
            bulletSpawn.position,
            // Bullet spawn rotation 
            bulletSpawn.rotation);
        
        // Add velocity to the bullet so it moves 
        bullet.GetComponent<Rigidbody>().AddForce(transform.forward * bulletSpeed * 10f);

        // Destroy the bullet after 2 seconds 
        Destroy(bullet, 2.0f);
    }


}
