using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvaderDrone : MonoBehaviour
{
    private Rigidbody2D rb;
    private int randomNumber;
    private float lastBoltSpawnTime;
    private Vector2 upVel = new Vector2(-3, 3), downVel = new Vector2(-3, -3);
    private float ShootRate = 2.0f, coundownTillShoot = 0.0f;
    private bool atTopOfMovementPath = true, lastShotWasTop, canshoot = false; 
    [SerializeField] Transform BoltSpawnTop, BoltSpawnBottom;
    [SerializeField] Rigidbody2D redbolt;
    [SerializeField] GameObject droneDestroyAnimation; 

    private void Start()
    {
        rb = this.gameObject.GetComponent<Rigidbody2D>();
    }

    private void ShootPlayer()
    {
        if (lastShotWasTop && canshoot)
        {
            Instantiate(redbolt, BoltSpawnTop.position, BoltSpawnTop.rotation);
            lastShotWasTop = false;
        }
        else if (!lastShotWasTop && canshoot)
        {
            Instantiate(redbolt, BoltSpawnBottom.position, BoltSpawnBottom.rotation);
            lastShotWasTop = true;
        }
    }

    private void moveUpAndDown()
    {
        if (atTopOfMovementPath)
        {
            rb.MovePosition(rb.position + downVel * Time.fixedDeltaTime); 
        }
        else if(!atTopOfMovementPath)
        {
            rb.MovePosition(rb.position + upVel * Time.fixedDeltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D c)
    {
        if(c.gameObject.tag == "TopDroneTrigger")
        {
            atTopOfMovementPath = true; 
        }

        if(c.gameObject.tag == "BottomDroneTrigger")
        { 
            atTopOfMovementPath = false; 
        }

        if(c.gameObject.CompareTag("Player"))
        {
            Destroy(this.gameObject);
            Instantiate(droneDestroyAnimation, transform.position, transform.rotation); 
        }

        if(c.gameObject.CompareTag("ActivateLasers"))
        {
            Debug.Log("activated"); 
            canshoot = true; 
        }
    }

    private void Update()
    { 
        moveUpAndDown(); 
        if (coundownTillShoot <=  0.0f)
        {
            ShootPlayer();
            coundownTillShoot = 3.5f / ShootRate; 
        }

        coundownTillShoot -= Time.deltaTime; 
    }
}
