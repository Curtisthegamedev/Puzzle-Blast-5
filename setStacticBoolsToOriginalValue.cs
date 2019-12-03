using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class setStacticBoolsToOriginalValue : MonoBehaviour
{
    private void Awake()
    {
        StartingQuestionBlocks.playerOpenedAStartingBox = false;
    }
}
