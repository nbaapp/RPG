using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI : MonoBehaviour
{
    public BattleSystem battleSystem;
    private PlayerUnit currentPlayer;
    
    public GameObject mainPanel;
    public TextMeshProUGUI dialogueText;
    public GameObject actionPanel;
    public Button action1;
    public Button action2;
    public Button action3;
    public Button action4;

    public void SetPlayer(PlayerUnit player)
    {
        currentPlayer = player;
    }

    public void ActivateActionPanel()
    {
        mainPanel.SetActive(false);
        actionPanel.SetActive(true);
    }
    public void ActivatMainPanel()
    {
        mainPanel.SetActive(true);
        actionPanel.SetActive(false);
    }
    public void setDialogueText(string text)
    {
        dialogueText.text = text;
    }

    public void SetAction1Text(string text)
    {
        action1.GetComponentInChildren<TextMeshProUGUI>().text = text;
    }
    public void SetAction2Text(string text)
    {
        action2.GetComponentInChildren<TextMeshProUGUI>().text = text;
    }
    public void SetAction3Text(string text)
    {
        action3.GetComponentInChildren<TextMeshProUGUI>().text = text;
    }
    public void SetAction4Text(string text)
    {
        action4.GetComponentInChildren<TextMeshProUGUI>().text = text;
    }

    public void SelectAction1()
    {
        Action action = currentPlayer.ExecuteAction1();
        StartCoroutine(battleSystem.PlayerAction(action));
    }
    public void SelectAction2()
    {
        Action action = currentPlayer.ExecuteAction2();
        StartCoroutine(battleSystem.PlayerAction(action));
    }
    public void SelectAction3()
    {
        Action action = currentPlayer.ExecuteAction3();
        StartCoroutine(battleSystem.PlayerAction(action));
    }
    public void SelectAction4()
    {
        Action action = currentPlayer.ExecuteAction4();
        StartCoroutine(battleSystem.PlayerAction(action));
    }

    public string GetActionDescription(Button actionButton)
    {
        string actionName = actionButton.GetComponentInChildren<TextMeshProUGUI>().text;
        Action action = currentPlayer.GetAction(actionName);
        return action.GetActionData();
    }
}
