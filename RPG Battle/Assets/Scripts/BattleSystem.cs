using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Threading.Tasks;

public enum BattleState {START, PLAYERTURN, ENEMYTURN, NEWROUND, WON, LOST}

public class BattleSystem : MonoBehaviour
{
    public BattleState state;
    
    private SetUp setUp;
    public UI ui;
    
    private PlayerUnit playerUnit;
    private Unit enemyUnit;

    //FIXME MOVE TO UNIT
    public BattleHud playerHUD;
    public BattleHud enemyHUD;

    // Start is called before the first frame update
    void Start()
    {
        setUp = GetComponent<SetUp>();
        state = BattleState.START;
        //find object with name "Player" and "Enemy"
        playerUnit = GameObject.Find("Player").GetComponent<PlayerUnit>();
        enemyUnit = GameObject.Find("Enemy").GetComponent<Unit>();

        SetupBattle();
    }

    
    async void SetupBattle()
    {
        ui.ActivatMainPanel();

        playerUnit = (PlayerUnit) await setUp.SpawnUnit(playerUnit);
        ui.SetPlayer(playerUnit);
        ui.SetAction1Text(playerUnit.GetActions()[0].name);
        ui.SetAction2Text(playerUnit.GetActions()[1].name);
        ui.SetAction3Text(playerUnit.GetActions()[2].name);
        ui.SetAction4Text(playerUnit.GetActions()[3].name);

        UnitAttributes enemyValues = await setUp.CreateEnemyUnit();
        enemyUnit.SetBaseValues(enemyValues);
        enemyUnit = await setUp.SpawnUnit(enemyUnit);

        ui.setDialogueText(enemyUnit.GetName() + " challenges you!");

        playerHUD.SetHud(playerUnit);
        enemyHUD.SetHud(enemyUnit);

        //yield return new WaitForSeconds(2f);

        state = BattleState.PLAYERTURN;
        PlayerTurn(playerUnit);
    }

    public IEnumerator PlayerAction(Action action)
    {
        ui.ActivatMainPanel();
        ui.setDialogueText("You used " + action.name + "!");

        yield return new WaitForSeconds(2f);

        if (enemyUnit.isDead)
        {
            state = BattleState.WON;
            EndBattle();
        }
        else
        {
            state = BattleState.ENEMYTURN;
            StartCoroutine(EnemyTurn());
        }
    }

    IEnumerator EnemyTurn()
    {
        ui.ActivatMainPanel();
        Action action = enemyUnit.SelectAction();
        ui.setDialogueText(enemyUnit.GetName() + " used " + action.name + "!");

        yield return new WaitForSeconds(1f);

        enemyUnit.ExecuteAction(action, new List<Unit> { playerUnit });

        yield return new WaitForSeconds(1f);


        if (playerUnit.isDead)
        {
            state = BattleState.LOST;
            EndBattle();
        }
        else
        {
            state = BattleState.NEWROUND;
            StartCoroutine(NewRound());
        }
    }

    IEnumerator NewRound()
    {
        state = BattleState.PLAYERTURN;
        foreach (Unit unit in GetUnit())
        {
            foreach (StatusEffect effect in unit.GetStatusEffects())
            {
                effect.Tick(unit);
            }
        }
        PlayerTurn(playerUnit);
        yield return new WaitForSeconds(1f);
    }


    void EndBattle()
    {
        if (state == BattleState.WON)
        {
            ui.setDialogueText("You Won!");
        }
        else if(state == BattleState.LOST)
        {
            ui.setDialogueText("You were defeated");
        }
    }

    void PlayerTurn(PlayerUnit unit)
    {
        ui.ActivatMainPanel();
        ui.setDialogueText(unit.GetName() + "'s turn!");
    }

    public void OnActButton()
    {
        if (state != BattleState.PLAYERTURN)
        {
            return;
        }
        else
        {
            ui.ActivateActionPanel();
        }
    }

    public List<Unit> GetPlayerUnits()
    {
        return new List<Unit> { playerUnit };
    }
    public List<Unit> GetEnemyUnits()
    {
        return new List<Unit> { enemyUnit };
    }
    public List<Unit> GetUnit()
    {
        return new List<Unit> { playerUnit, enemyUnit };
    }
}
