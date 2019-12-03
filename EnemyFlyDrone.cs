using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//INHERITS FROM ENEMY 
public class EnemyFlyDrone : Enemy
{
    [SerializeField] GameObject RedLaser;
    [SerializeField] Transform MovePosOne, MovePosTwo;
    private float speed = 5;
    private Vector3 nextPos; 
    private Transform targetPlayer;
    private bool isMoveing;
    void Start()
    {
        targetPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        walking = true;
        nextPos = MovePosOne.position; 
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Ground" || collision.gameObject.tag == "Wall")
        {
            Flip(); 
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "TriggerDirSwitch")
        {
            Flip();
        }
    }

    private IEnumerator WaitAndToggleLaserlight()
    {
        if(RedLaser.activeInHierarchy)
        {
            yield return new WaitForSeconds(3.5f);
            RedLaser.SetActive(false);
        }
        else if(!RedLaser.activeInHierarchy)
        {
            yield return new WaitForSeconds(3.5f); 
            RedLaser.SetActive(true); 
        }
    }
    // Update is called once per frame
    void Update()
    {
        StartCoroutine(WaitAndToggleLaserlight());
        DroneMovement(); 
        if(Input.GetKeyDown(KeyCode.E))
        {
            Flip(); 
        }
    }

    private void DroneMovement()
    {
        if(transform.position.x == MovePosOne.position.x)
        { 
            nextPos = MovePosTwo.position; 
        }
        if(transform.position.x == MovePosTwo.position.x)
        {
            nextPos = MovePosOne.position; 
        }
        transform.position = Vector3.MoveTowards(transform.position, nextPos, speed * Time.deltaTime); 
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(MovePosOne.position, MovePosTwo.position); 
    }
}
