using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class psDestroy : MonoBehaviour
{
    private float destroyTime = 0;

    private void Update()
    {
        destroyTime += Time.deltaTime; 
        if(destroyTime >= 1)
        {
            Destroy(this.gameObject); 
        }
    }
}
