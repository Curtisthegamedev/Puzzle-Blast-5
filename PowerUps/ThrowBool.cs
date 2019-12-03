using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowBool : MonoBehaviour
{
    public static bool isInRange = false;

    private void OnTriggerStay2D(Collider2D col)
    {
        if(col.gameObject.tag == "Player")
        {
            isInRange = true; 
        }
        else
        {
            isInRange = false; 
        }

    }
    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            isInRange = false;
        }
    }
}
