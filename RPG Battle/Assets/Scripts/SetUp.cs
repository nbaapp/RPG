using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using System;
using OpenAI;

public class SetUp : MonoBehaviour
{
    OpenAIMessenger openAIMessenger;

    [TextArea(3, 10)]
    public string spritePromptSystemMessage = "Your purpose is to generate a prompt for DallE-3 to help generate sprites of different entities that will be used in a game. You will be provided the name of an entitiy that will be encountered, then you will create a detailed prompt of the entity's physical description that will be used by DallE to generate the image.";

    [TextArea(3, 10)]
    public string encounterGeneratorSystemMessage = "You will be given an enviornment. Create the name, description and stats for a single enemy that may be encountered by an adventurer in such an enviornment. The description should be a detailed prompt for DallE-3 to generate an image of the entity. Ensure that the image generated will always be of only one character. The style should be pixel art.";

    [TextArea(3, 10)]
    public string actionGeneratorSystemMessage = "You will be given the name and description of an enemy that will be encountered by an adventurer. Create a single action that the entity can perform. Each action should have a name, description, target type, and a list of effects. The target type must be one of the following: Self, SingleEnemy, SingleAlly, AllEnemies, AllAllies, AllUnits. The effects must be one of the following: Damage, Heal, Buff, Debuff. Each effect should have a value and a duration. Do not add extra details or options that are not provided in the prompt.";
    void Start()
    {
        openAIMessenger = FindObjectOfType<OpenAIMessenger>();
    }

    public async Task<UnitAttributes> CreateEnemyUnit(string enviornment = "Forest") {
        //set response format to the unit's name and stats
        openAIMessenger.ResetAndSetSystemMessage(encounterGeneratorSystemMessage);
        UnitAttributes unit = await openAIMessenger.GenerateUnit(enviornment);
        return unit;
    }

    public async Task<string> GenerateUnitDescription(string unitName)
    {
        openAIMessenger.ResetAndSetSystemMessage(spritePromptSystemMessage);
        return await openAIMessenger.SendTextMessage(unitName);
    }

    public async Task<Action> GenerateAction(string entityName)
    {
        //openAIMessenger.ResetAndSetSystemMessage(actionGeneratorSystemMessage);
        return await openAIMessenger.GetActionResponse(entityName);
    }

    public async Task<Unit> SpawnUnit(Unit unit)
    {
        Sprite unitSprite = await openAIMessenger.SendImageMessage(unit.GetDescription(), unit.GetName());
        unitSprite.name = unit.GetName();
        openAIMessenger.ResetAndSetSystemMessage(actionGeneratorSystemMessage);
        for (int i = 0; i < 4; i++)
        {
            unit.addAction(await GenerateAction(unit.GetName()));
        }
        SpriteRenderer renderer = unit.gameObject.GetComponent<SpriteRenderer>();
        renderer.sprite = unitSprite;

        return unit;
    }
}
