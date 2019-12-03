using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisaperingPlatforms : MonoBehaviour
{
    [SerializeField] GameObject greenOne;
    [SerializeField] GameObject greenTwo;
    [SerializeField] GameObject greenThree;
    [SerializeField] GameObject greenfour;

    [SerializeField] GameObject RedOne;
    [SerializeField] GameObject RedTwo;
    [SerializeField] GameObject RedThree;
    [SerializeField] GameObject RedFour; 

    private float platformPos = 1; 

    private void Start()
    {
        greenOne.SetActive(true);
        greenTwo.SetActive(false);
        greenThree.SetActive(true);
        greenfour.SetActive(false);
        StartCoroutine(waitAndChangeActiveplatforms()); 
    }

    private void Update()
    {
        if (platformPos > 4)
        {
            platformPos = 1; 
        }
        switch (platformPos)
        {
            case 1:
                greenOne.SetActive(true);
                greenTwo.SetActive(false);
                greenThree.SetActive(true);
                greenfour.SetActive(false);
                RedOne.SetActive(false);
                RedTwo.SetActive(false);
                RedThree.SetActive(false);
                RedFour.SetActive(false); 
                break;
            case 2:
                greenOne.SetActive(false);
                greenTwo.SetActive(false);
                greenThree.SetActive(false);
                greenfour.SetActive(false);
                RedOne.SetActive(true);
                RedTwo.SetActive(false);
                RedThree.SetActive(true);
                RedFour.SetActive(false); 
                break; 
            case 3:
                greenOne.SetActive(false);
                greenTwo.SetActive(true);
                greenThree.SetActive(false);
                greenfour.SetActive(true);
                RedOne.SetActive(false);
                RedTwo.SetActive(false);
                RedThree.SetActive(false);
                RedFour.SetActive(false); 
                break;
            case 4:
                greenOne.SetActive(false);
                greenTwo.SetActive(false);
                greenThree.SetActive(false);
                greenfour.SetActive(false);
                RedOne.SetActive(false);
                RedTwo.SetActive(true);
                RedThree.SetActive(false);
                RedFour.SetActive(true);
                break; 
        }
    }

    private IEnumerator waitAndChangeActiveplatforms()
    {
        yield return new WaitForSeconds(1);
        platformPos += 1;
        StartCoroutine(waitAndChangeActiveplatforms()); 
    }
}
