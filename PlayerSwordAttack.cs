using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSwordAttack : MonoBehaviour
{
    private float timeBetweenAttacks;
    [SerializeField] float startTimeBetweenAttacks;
    [SerializeField] Transform attackPos;
    [SerializeField] LayerMask whatIsEnemies; 
    [SerializeField] float attackRange;
    private int damage = 1; 

    private void Update()
    {
        if(timeBetweenAttacks <= 0)
        {
            if(Input.GetKeyDown(KeyCode.LeftShift))
            {
                Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, whatIsEnemies);
                Debug.Log(enemiesToDamage); 
                for (int i = 0; i < enemiesToDamage.Length; i++)
                {
                    enemiesToDamage[i].GetComponent<EnemyHealth>().health -= damage;
                    enemiesToDamage[i].GetComponent<EnemyHealth>().enemyhealthChanged = true;
                }
                timeBetweenAttacks = startTimeBetweenAttacks;
            } 
        }
        else
        {
            timeBetweenAttacks -= Time.deltaTime; 
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(attackPos.position, attackRange); 
    }
}
