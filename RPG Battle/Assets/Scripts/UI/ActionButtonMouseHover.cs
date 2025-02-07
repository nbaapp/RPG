using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

//script to handle mouse hover over buttons, using IPointerEnterHandler and IPointerExitHandler
public class ActionButtonMouseHover : MonoBehaviour, UnityEngine.EventSystems.IPointerEnterHandler, UnityEngine.EventSystems.IPointerExitHandler
{
    public TextMeshProUGUI actionDescriptionText;
    public UI ui;
    private Button button;
    string actionDescriptionDefault = "hover over an action to see its description";

    void Start()
    {
        //set action description text to default description
        actionDescriptionText.text = actionDescriptionDefault;
        button = GetComponent<Button>();
    }
    public void OnPointerEnter(UnityEngine.EventSystems.PointerEventData eventData)
    {
        //change description text to action description when mouse hovers over button
        string description = ui.GetActionDescription(button);
        actionDescriptionText.text = description;
    }

    public void OnPointerExit(UnityEngine.EventSystems.PointerEventData eventData)
    {
        //change description text back to unit description when mouse exits button
        actionDescriptionText.text = actionDescriptionDefault;
    }
}