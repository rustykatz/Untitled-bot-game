using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_sample : MonoBehaviour
{
    public float speed = 5;
    public float jumpForce = 10;

    float lookHorizontal = 0;
    float lookVertical = 0;

    public GameObject orangePortalPrefab;
    public GameObject bluePortalPrefab;

    public static GameObject orangePortal;
    public static GameObject bluePortal;

    public GameObject bulletPrefab;
    public Transform bulletSpawn;
    public int bulletSpeed = 10; 

    public Text CountText;

    private int count;

    
    void Start()
    {
        count = 0;
        setCountText();
        Cursor.lockState = CursorLockMode.Locked;
    }
    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(Vector3.back * speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector3.left * speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            GetComponent<Rigidbody>().AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }

        lookHorizontal += Input.GetAxis("Mouse X");
        lookVertical += Input.GetAxis("Mouse Y");
        transform.eulerAngles = new Vector3(-lookVertical, lookHorizontal, 0f);


        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = new Ray();
            ray.origin = Camera.main.transform.position;
            ray.direction = Camera.main.transform.forward; 
            Debug.DrawRay(ray.origin, ray.direction * 100f, Color.red, 3f);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                Debug.Log(hit.collider.gameObject.name);
                Quaternion rotation = Quaternion.LookRotation(hit.normal, Vector3.up);
                if (orangePortal != null)
                {
                    Destroy(orangePortal);
                }

                orangePortal = (GameObject)Instantiate(orangePortalPrefab, hit.point + (Vector3.up * 0.5f), rotation);
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = new Ray();
            ray.origin = Camera.main.transform.position; 
            ray.direction = Camera.main.transform.forward;
            Debug.DrawRay(ray.origin, ray.direction * 100f, Color.red, 3f);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                Debug.Log(hit.collider.gameObject.name);
                Quaternion rotation = Quaternion.LookRotation(hit.normal, Vector3.up);
                if (bluePortal != null)
                {
                    Destroy(bluePortal);
                }
                bluePortal = (GameObject)Instantiate(bluePortalPrefab, hit.point + (Vector3.up * 0.5f), rotation);
            }
        }

        // Middle Mouse button 
        if (Input.GetKeyDown(KeyCode.F))
        {
            Fire();
        }

        CountText.color = Color.HSVToRGB(Random.Range(0f, 1f), 1, 1);
    }



    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("pick_up"))
        {
            other.gameObject.SetActive(false);
            count = count + 1;
            setCountText();
        }
    }

    void setCountText()
    {
        CountText.text = "Count: " + count.ToString();
    }


    void Fire()
    {
        // Create the Bullet from the Bullet Prefab
        var bullet = (GameObject)Instantiate(
            // Bullet prefab object
            bulletPrefab,
            // Bullet spawn position
            bulletSpawn.position,
            // Bullet spawn rotation 
            bulletSpawn.rotation);

        // Add velocity to the bullet so it moves 
        bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * bulletSpeed;

        // Destroy the bullet after 2 seconds 
        Destroy(bullet, 2.0f);

    }

}

