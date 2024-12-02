using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUnit : Unit
{
    public void ExecuteAction1()
    {
        if (actions[0] != null)
        {
            ExecuteAction(actions[0]);
        }
    }
}
