using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayRandomTip : MonoBehaviour
{
    [SerializeField] GameObject tipOne, tipTwo, TipThree;

    private float randomNumber;

    private void Awake()
    {
        randomNumber = Random.Range(0, 3); 

        if(randomNumber == 0)
        {
            tipOne.SetActive(true); 
        }
        if(randomNumber == 1)
        {
            tipTwo.SetActive(true); 
        }
        if (randomNumber == 2)
        {
            TipThree.SetActive(true); 
        }
    }
}
