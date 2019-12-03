using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//INHERITS FROM ENEMY SCRIPT
public class Slime : Enemy
{
    public static bool activateCoroutine = false; 
    private void Awake()
    {
        StartCoroutine(waitAndIdleOrWalk()); 
    }
    private void Update()
    {
        Movement();
        if (ThrowBool.isInRange)
        {
            //enemy attacks after a set amout of seconds
            if (Time.time > lastBulletSpawnTime + rateOfThrowing)
            { 
                Attack();
            }
        }
        if(activateCoroutine)
        {
            StartCoroutine(waitAndIdleOrWalk());
            activateCoroutine = false; 
        }
    }

    public void OnTriggerEnter2D(Collider2D c)
    {
        if (c.gameObject.tag != "Ground" && c.gameObject.tag != "LaserBolt" &&
           c.gameObject.tag != "SwordWeapon" && !c.gameObject.CompareTag("Player"))
        {
            Flip();
        }

        if(c.gameObject.CompareTag("Player"))
        {
            EnemiesAnim.SetTrigger("Attack");
        }
    }


}
