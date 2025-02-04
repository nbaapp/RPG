using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerUnit : Unit
{
    private Action selectedAction;
    public Action ExecuteAction1()
    {
        if (actions[0] != null)
        {
            selectedAction = actions[0];
            List<Unit> targets = SelectTargets(selectedAction.targetType);
            ExecuteAction(selectedAction, targets);
            return selectedAction;
        }
        Debug.Log("No action selected");
        return null;
    }
    public Action ExecuteAction2()
    {
        if (actions[1] != null)
        {
            selectedAction = actions[1];
            List<Unit> targets = SelectTargets(selectedAction.targetType);
            ExecuteAction(selectedAction, targets);
            return selectedAction;
        }
        Debug.Log("No action selected");
        return null;
    }
    public Action ExecuteAction3()
    {
        if (actions[2] != null)
        {
            selectedAction = actions[2];
            List<Unit> targets = SelectTargets(selectedAction.targetType);
            ExecuteAction(selectedAction, targets);
            return selectedAction;
        }
        Debug.Log("No action selected");
        return null;
    }
    public Action ExecuteAction4()
    {
        if (actions[3] != null)
        {
            selectedAction = actions[3];
            List<Unit> targets = SelectTargets(selectedAction.targetType);
            ExecuteAction(selectedAction, targets);
            return selectedAction;
        }
        Debug.Log("No action selected");
        return null;
    }

    public List<Unit> SelectTargets(TargetType targetType)
    {
        List<Unit> targets = new List<Unit>();
        switch (targetType)
        {
            case TargetType.Self:
                targets = new List<Unit> { this };
                break;
            case TargetType.SingleEnemy:
                targets = new List<Unit> { battleSystem.GetEnemyUnits()[0] };
                break;
            case TargetType.AllEnemies:
                targets = battleSystem.GetEnemyUnits();
                break;
            case TargetType.SingleAlly:
                targets = new List<Unit> { battleSystem.GetPlayerUnits()[0] };
                break;
            case TargetType.AllAllies:
                targets = battleSystem.GetPlayerUnits();
                break;
            case TargetType.AllUnits:
                targets = battleSystem.GetPlayerUnits().Concat(battleSystem.GetEnemyUnits()).ToList();
                break;
            default:
                Debug.Log("Invalid Target Type");
                break;
        }
        return targets;
    }
}
