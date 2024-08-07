using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SetTurnType : MonoBehaviour
{
    public ActionBasedSnapTurnProvider snapturn;
    public ActionBasedContinuousTurnProvider continuousTurn;

    public void SetTypeFrontIndex(int index)
    {
        if (index == 0)
        {
            snapturn.enabled = false;
            continuousTurn.enabled = true;
        }
        else if (index == 1) 
        {
            snapturn.enabled = true;
            continuousTurn.enabled = false;
        }
    }

}
