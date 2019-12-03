using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class triggerEnemies : MonoBehaviour
{
    [SerializeField] GameObject Slime; 
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            Slime.SetActive(true); 
        }
    }
}
