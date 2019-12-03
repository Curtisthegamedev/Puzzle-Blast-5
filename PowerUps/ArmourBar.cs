using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmourBar : MonoBehaviour
{
    public GameObject Bar1;
    public GameObject Bar2;
    public GameObject Bar3;

    public static int armourAmount;

    private void Start()
    {
        armourAmount = 0;
        Bar1.gameObject.SetActive(false);
        Bar2.gameObject.SetActive(false);
        Bar3.gameObject.SetActive(false); 
    }

    private void Update()
    {
        if (armourAmount > 3)
        {
            armourAmount = 3; 
        }

        switch (armourAmount)
        {
            case 3:
                Bar1.gameObject.SetActive(true);
                Bar2.gameObject.SetActive(true);
                Bar3.gameObject.SetActive(true); 
                if (!Player.hasArmour)
                {
                    Player.hasArmour = true;
                }
                break;
            case 2:
                Bar1.gameObject.SetActive(true);
                Bar2.gameObject.SetActive(true);
                Bar3.gameObject.SetActive(false);
                if (!Player.hasArmour)
                {
                    Player.hasArmour = true;
                }
                break;
            case 1:
                Bar1.gameObject.SetActive(true);
                Bar2.gameObject.SetActive(false);
                Bar3.gameObject.SetActive(false);
                if (!Player.hasArmour)
                {
                    Player.hasArmour = true;
                }
                break;
            case 0:
                Bar1.gameObject.SetActive(false);
                Bar2.gameObject.SetActive(false);
                Bar3.gameObject.SetActive(false);
                if (Player.hasArmour)
                {
                    Player.hasArmour = false; 
                }
                break; 
        }
    }
}
