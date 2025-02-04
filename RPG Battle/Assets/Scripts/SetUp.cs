using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using System;
using OpenAI;
using OpenAI.Chat;

public class SetUp : MonoBehaviour
{
    public OpenAIMessenger openAIMessenger;

    List<Message> actionMessages = new List<Message>();
    List<Message> unitMessages = new List<Message>();

    [TextArea(3, 10)]
    public string encounterGeneratorSystemMessage = "You will be given an enviornment. Create the name, description and stats for a single enemy that may be encountered by an adventurer in such an enviornment. The description should be a detailed prompt for DallE-3 to generate an image of the entity. Ensure that the image generated will always be of only one character. The style of each image should be pixel art. Additionally, be certain that the prompt emphasizes that there should be no text in the image.";

    [TextArea(3, 10)]
    public string actionGeneratorSystemMessage = "You will be given the name and description of an enemy that will be encountered by an adventurer. Create a single action that the entity can perform. Each action should have a name, description, target type, and a list of effects. The target type must be one of the following: Self, SingleEnemy, SingleAlly, AllEnemies, AllAllies, AllUnits. The effects must be one of the following: Damage, Heal, or PersistantEffect. Each effect should have a value and a duration. Do not add extra details or options that are not provided in the prompt.";

    public async Task<UnitAttributes> CreateEnemyUnit(string enviornment = "Forest") {
        //set response format to the unit's name and stats
        ResetUnitMessages();
        UnitAttributes unit = await openAIMessenger.GenerateUnit(enviornment, unitMessages);
        return unit;
    }

    public async Task<Action> GenerateAction(Unit entity)
    {
        return await openAIMessenger.GetActionResponse("Name: " + entity.GetName() + "\nDescription: " + entity.GetDescription(), actionMessages);
    }

    public async Task<Unit> SpawnUnit(Unit unit)
    {
        Sprite unitSprite = await openAIMessenger.SendImageMessage(unit.GetDescription(), unit.GetName());
        unitSprite.name = unit.GetName();
        ResetActionMessages();
        for (int i = 0; i < 4; i++)
        {
            unit.addAction(await GenerateAction(unit));
        }
        SpriteRenderer renderer = unit.gameObject.GetComponent<SpriteRenderer>();
        renderer.sprite = unitSprite;

        return unit;
    }
    public void ResetActionMessages()
    {
        actionMessages.Clear();
        actionMessages.Add(new Message(Role.System, actionGeneratorSystemMessage));
    }
    public void ResetUnitMessages()
    {
        unitMessages.Clear();
        unitMessages.Add(new Message(Role.System, encounterGeneratorSystemMessage));
    }
}
