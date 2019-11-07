using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 5.0f;

    // Mouse Controller 
    private float mouseX;
    private float mouseY;
    private float yawH = 100f;
    private float pitchV = 100f;
    private float xRotation = 0f;

    public float jumpForce = 5f; 

    public bool isGrounded;

    public GameObject bulletPrefab;

    // Quad barrels 
    public Transform bulletSpawn_RT;
    public Transform bulletSpawn_RB;

    public Transform bulletSpawn_LT;
    public Transform bulletSpawn_LB;

    public int bulletSpeed = 20; 

    public float health;
    public float maxHealth; 
    private float hperc;

    public GameObject respawn; 
    Vector3 respawnOffset;

    // To be implemented 
    //public List<bool> weaponManager = new List<bool>();
    private int impWeapons = 4;
    public float damage;

    public int weaponLevel;

    [SerializeField] private bool weapon_1;
    [SerializeField] private bool weapon_2;
    [SerializeField] private bool weapon_3;
    [SerializeField] private bool weapon_4;

    Rigidbody rb; 
    Transform cameraTransform;

    Vector3 move;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
        cameraTransform = Camera.main.transform;

        health = 10;
        maxHealth = health;

        weaponLevel = 1;
        damage = 1; 

        weapon_1 = true;
        weapon_2 = true;
        weapon_3 = false;
        weapon_4 = false; 
    }

    // Update is called once per frame
    void Update()
    {
        look();
        PlayerControl();
        WeaponSwapper();
    }

    void PlayerControl()
    {
        if (Input.GetKey(KeyCode.W))
        {
            rb.MovePosition(transform.position + transform.forward * Time.fixedDeltaTime * speed);
        }

        if (Input.GetKey(KeyCode.A))
        {
            rb.MovePosition(transform.position + transform.right* -1 * Time.fixedDeltaTime * speed);
        }

        if (Input.GetKey(KeyCode.S))
        {
            rb.MovePosition(transform.position + transform.forward * -1 * Time.fixedDeltaTime * speed);
        }
        if (Input.GetKey(KeyCode.D))
        {
            rb.MovePosition(transform.position + transform.right * Time.fixedDeltaTime * speed);
        }

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded == true)
        {
            GetComponent<Rigidbody>().AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }

        if(Input.GetMouseButtonDown(1) && weapon_1)
        {
            ShootRightWeapon();
        }
        if(Input.GetMouseButtonDown(0) && weapon_2)
        {
            ShootLeftWeapon();
        }
    }

    void WeaponSwapper(){
        if (Input.GetKeyDown(KeyCode.Alpha1)){
            if(weapon_1 == false){
                weapon_1 =true;
            }
            else{
                weapon_1 = false;
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha2)){
            if(weapon_2 == false){
                weapon_2 =true;
            }
            else{
                weapon_2 = false;
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha3)){
            if(weapon_3 == false){
                weapon_3 =true;
            }
            else{
                weapon_3 = false;
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha4)){
            if(weapon_4 == false){
                weapon_4 =true;
            }
            else{
                weapon_4 = false;
            }
        }
    }

    void look()
    {
        mouseX += Input.GetAxis("Mouse X") * yawH * Time.deltaTime;
        mouseY += Input.GetAxis("Mouse Y") * pitchV *Time.deltaTime;
    
        mouseY = Mathf.Clamp(mouseY, -30f, 80f);
        transform.eulerAngles = new Vector3(-mouseY, mouseX,0f);
    }   


    // Collision for items 
    void OnCollisionEnter(Collision col){
        if(col.gameObject.tag == "Floor" ){
            isGrounded = true;
        }
        else if(col.gameObject.tag == "Damage_Item")
        {
            print("Damage item picked up!");
            Destroy(col.gameObject);
        }
    }
    void OnCollisionExit(Collision col){
        if(col.gameObject.tag == "Floor" ){
            isGrounded = false; 
        }
    }



    void ShootRightWeapon(){
        // Create the Bullet from the Bullet Prefab
        var bulletTop = (GameObject)Instantiate(
            // Bullet prefab object
            bulletPrefab,
            // Bullet spawn position
            bulletSpawn_RT.position,
            // Bullet spawn rotation 
            bulletSpawn_RT.rotation);
        // Add velocity to the bullet so it moves 
        bulletTop.GetComponent<Rigidbody>().AddForce(transform.forward * bulletSpeed * 10f);
        // Destroy the bullet after 2 seconds 
        Destroy(bulletTop, 3.0f);

        // Bottom right barrel
        if(weapon_3 == true){
            var bulletBot = (GameObject)Instantiate(bulletPrefab, bulletSpawn_RB.position, bulletSpawn_RB.rotation);
            bulletBot.GetComponent<Rigidbody>().AddForce(transform.forward * bulletSpeed * 10f);
            Destroy(bulletBot, 3.0f);
        }
    }
    void ShootLeftWeapon(){
        var bulletTop = (GameObject)Instantiate(bulletPrefab, bulletSpawn_LT.position, bulletSpawn_LT.rotation);
        bulletTop.GetComponent<Rigidbody>().AddForce(transform.forward * bulletSpeed * 10f);
        Destroy(bulletTop, 3.0f);

        if(weapon_4 == true){
            var bulletBot = (GameObject)Instantiate(bulletPrefab, bulletSpawn_LB.position, bulletSpawn_LB.rotation);
            bulletBot.GetComponent<Rigidbody>().AddForce(transform.forward * bulletSpeed * 10f);
            Destroy(bulletBot, 3.0f);
        }
       
    }

    public void WeaponUpgrade(int _upgrade){
        weaponLevel += _upgrade; 
        print("PLAYER: Weapon Level -> " + weaponLevel.ToString());
    }

    public void Heal(float _heal){
        health += _heal; 
        if( health > maxHealth ){
            health = maxHealth;
        }
    }

    public void BoostDamage(float _boost){
        damage += _boost;
        print("PLAYER: Weapon Damage -> " + damage.ToString());
    }
    
    public void TakeDamage(float damage){
        if(health > 0.01){
            health -= damage;
            hperc = health/ maxHealth + 0.05f;
            // hp.SetSize(hperc);
        }
        else
        {
            gameObject.transform.position = respawn.transform.position + respawnOffset;
            health = 10;
            print("Health reset: " + health.ToString());
            maxHealth = health;
            // Destroy(gameObject);
            //hperc = 0;
            // hp.SetSize(hperc);
        }
        
        print("Player HP: " + health.ToString()); 
    }

}
