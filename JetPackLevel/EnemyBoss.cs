using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBoss : MonoBehaviour
{
    private Rigidbody2D rb;
    private int randomNumber;
    private float lastBoltSpawnTime, speed = 3;
    private bool lastShotWasTop = true, atTopOfMovementPath = true,
        ReachedBossfightArea = false;
    public bool CanShoot = false; 
    private Vector2 upVel = new Vector2(-3, 3), downVel = new Vector2(-3, -3);
    private Vector3 nextPos; 
    private float moveRightSpeed = 1, ShootRate = 2.0f,coundownTillShoot = 0.0f;
    //[SerializeField] Transform BoltSpawnTop, BoltSpawnBottom;
    [SerializeField] Rigidbody2D redbolt;
    [SerializeField] Transform MovePosOne, MovePosTwo, BoltSpawnTop, BoltSpawnBottom; 

    private void Start()
    {
        rb = this.gameObject.GetComponent<Rigidbody2D>();
        nextPos = MovePosOne.position; 
    }

    public void ShootPlayer()
    {
        if(lastShotWasTop && CanShoot)
        {
            Instantiate(redbolt, BoltSpawnTop.position, BoltSpawnTop.rotation);
            lastShotWasTop = false;
        }
        else if(!lastShotWasTop && CanShoot)
        {
           Instantiate(redbolt, BoltSpawnBottom.position, BoltSpawnBottom.rotation);
           lastShotWasTop = true;
        }
    }

    private void Move()
    { 
        if(!ReachedBossfightArea)
        {
            if (atTopOfMovementPath)
            {
                rb.MovePosition(rb.position + downVel * Time.fixedDeltaTime);
            }
            else if (!atTopOfMovementPath)
            {
                rb.MovePosition(rb.position + upVel * Time.fixedDeltaTime);
            }
        }
        else if(ReachedBossfightArea)
        {
            if(transform.position.y == MovePosOne.position.y)
            {
                nextPos = MovePosTwo.position;
            }
            if (transform.position.y == MovePosTwo.position.y)
            {
                nextPos = MovePosOne.position;
            }
            transform.position = Vector3.MoveTowards(transform.position, nextPos, speed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D c)
    {
        if (c.gameObject.tag == "TopDroneTrigger")
        {
            atTopOfMovementPath = true;
        }

        if (c.gameObject.CompareTag("ActivateLasers"))
        {
            CanShoot = true; 
        }
        if (c.gameObject.tag == "BottomDroneTrigger")
        {
            atTopOfMovementPath = false;
        }

        if(c.gameObject.tag == "BossFight")
        {
            ReachedBossfightArea = true; 
        }
    }

    private void Update()
    {
        Move();
        if (coundownTillShoot <= 0.0f)
        {
            ShootPlayer();
            coundownTillShoot = 3.5f / ShootRate;
        }
        coundownTillShoot -= Time.deltaTime;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(MovePosOne.position, MovePosTwo.position);
    }
}
