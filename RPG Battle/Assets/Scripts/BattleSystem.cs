using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Threading.Tasks;

public enum BattleState {START, PLAYERTURN, ENEMYTURN, WON, LOST}

public class BattleSystem : MonoBehaviour
{
    public BattleState state;
    
    private SetUp setUp;

    public Transform playerLocation;
    public Transform enemyLocation;
    
    private Unit playerUnit;
    private Unit enemyUnit;

    public TextMeshProUGUI dialogueText;

    //FIXME MOVE TO UNIT
    public BattleHud playerHUD;
    public BattleHud enemyHUD;

    // Start is called before the first frame update
    void Start()
    {
        setUp = GetComponent<SetUp>();
        state = BattleState.START;
        //find object with name "Player" and "Enemy"
        playerUnit = GameObject.Find("Player").GetComponent<Unit>();
        enemyUnit = GameObject.Find("Enemy").GetComponent<Unit>();
        SetupBattle();
    }

    
    async void SetupBattle()
    {
        playerUnit = await setUp.SpawnUnit(playerUnit);

        UnitAttributes enemyValues = await setUp.CreateEnemyUnit();
        enemyUnit.SetValues(enemyValues);
        enemyUnit = await setUp.SpawnUnit(enemyUnit);

        dialogueText.text = enemyUnit.GetName() + " challenges you!";

        playerHUD.SetHud(playerUnit);
        enemyHUD.SetHud(enemyUnit);

        //yield return new WaitForSeconds(2f);

        state = BattleState.PLAYERTURN;
        PlayerTurn();
    }

    IEnumerator PlayerAttack()
    {
        enemyUnit.TakeDamage(playerUnit.GetAttack());
        enemyHUD.setHP(enemyUnit.GetAttack());   //FIXME Hud should be handled by the unit
        dialogueText.text = "Attack Successful!";

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
        Action action = enemyUnit.SelectAction();
        dialogueText.text = enemyUnit.GetName() + " used " + action.name + "!";

        yield return new WaitForSeconds(1f);

        enemyUnit.ExecuteAction(action, new List<Unit> { playerUnit });
        playerHUD.setHP(playerUnit.GetCurrentHP());    //FIXME Hud should be handled by the unit

        yield return new WaitForSeconds(1f);


        if (playerUnit.isDead)
        {
            state = BattleState.LOST;
            EndBattle();
        }
        else
        {
            state = BattleState.PLAYERTURN;
            PlayerTurn();
        }
    }


    void EndBattle()
    {
        if (state == BattleState.WON)
        {
            dialogueText.text = "You Won!";
        }
        else if(state == BattleState.LOST)
        {
            dialogueText.text = "You were defeated";
        }
    }

    void PlayerTurn()
    {
        dialogueText.text = "Your Turn!";
    }

    public void OnAttackButton()
    {
        if (state != BattleState.PLAYERTURN)
        {
            return;
        }
        else
        {
            StartCoroutine(PlayerAttack());
        }
    }
}
