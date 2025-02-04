using System.Collections;
using System.Collections.Generic;
using OpenAI;
using OpenAI.Models;
using UnityEngine;
using OpenAI.Chat;
using OpenAI.Images;
using NUnit.Framework;
using UnityEngine.Events;
using System.Threading.Tasks;
using Newtonsoft.Json;

//Using the libraries from https://github.com/RageAgainstThePixel/com.openai.unity?tab=readme-ov-file
public class OpenAIMessenger : MonoBehaviour
{
    OpenAIClient api;
    Model textModel;
    Model imageModel;

    [Header("API Settings")]
    public string textModelName = "gpt-4o";
    public string imageModelName = "dall-e-3";
    
    void Start()
    {
        api = new OpenAIClient(new OpenAIAuthentication().LoadFromPath("/Users/aaronpeercy/.openai/auth.json"));
        textModel = new Model(textModelName);
        imageModel = new Model(imageModelName);
    }

    public async Task<string> SendTextMessage(string message, List<Message> messages) {
        //add user message to the list
        messages.Add(new Message(Role.User, message));

        //send message to the API
        var chatRequest = new ChatRequest(messages, textModel);
        var response = await api.ChatEndpoint.GetCompletionAsync(chatRequest);
        var choice = response.FirstChoice;
        messages.Add(choice.Message);
        Debug.Log($"[{choice.Index}] {choice.Message.Role}: {choice.Message} | Finish Reason: {choice.FinishReason}");

        return choice.Message;
    }

    //get formatted response
    public async Task<UnitAttributes> GenerateUnit(string message, List<Message> messages) {
        //add user message to the list
        messages.Add(new Message(Role.User, message));

        //send message to the API
        var chatRequest = new ChatRequest(messages, textModel);
        var response = await api.ChatEndpoint.GetCompletionAsync<UnitResponse>(chatRequest);
        var choice = response.Item1;

        
        UnitAttributes unit = new UnitAttributes
        {
            unitName = choice.unitName,
            unitDescription = choice.unitDescription,
            unitLevel = choice.unitLevel,
            attack = choice.attack,
            magicDefence = choice.magicDefence,
            defense = choice.defense,
            maxHP = choice.maxHP,
        };

        string DebugMessage = "Unit Name: " + unit.unitName + "\n" +
            "Unit Description: " + unit.unitDescription + "\n" +
            "Unit Level: " + unit.unitLevel + "\n" +
            "Attack: " + unit.attack + "\n" +
            "Magic Defence: " + unit.magicDefence + "\n" +
            "Defense: " + unit.defense + "\n" +
            "Max HP: " + unit.maxHP + "\n";
        Debug.Log(DebugMessage);

        return unit;

    }

    public async Task<Sprite> SendImageMessage(string message, string name = "sprite") {
        //check if image with name already exits, if so return it
        string path = "Assets/Sprites/AI/" + name + ".png";
        if (System.IO.File.Exists(path)) {
            return UnityEditor.AssetDatabase.LoadAssetAtPath<Sprite>(path);
        }

        var request = new ImageGenerationRequest(message, Model.DallE_3);
        var imageResults = await api.ImagesEndPoint.GenerateImageAsync(request);

        foreach (var result in imageResults)
        {
            Debug.Log(result.ToString());
            Assert.IsNotNull(result.Texture);
        }
        
        Texture2D texture = imageResults[0].Texture;
        //Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));

        //move sprite to sprites folder
        System.IO.File.WriteAllBytes(path, texture.EncodeToPNG());
        UnityEditor.AssetDatabase.Refresh();
        UnityEditor.AssetDatabase.ImportAsset(path);
        Sprite sprite = UnityEditor.AssetDatabase.LoadAssetAtPath<Sprite>(path);

        return sprite;
    }

    public async Task<Action> GetActionResponse(string message, List<Message> actionMessages) {
        //add user message to the list
        actionMessages.Add(new Message(Role.User, message));

        //send message to the API
        var chatRequest = new ChatRequest(actionMessages, textModel);
        var response = await api.ChatEndpoint.GetCompletionAsync<ActionResponse>(chatRequest);
        var choice = response.Item1;

        Action action = new Action
        (
            choice.name,
            choice.description,
            Action.StringToTargetType(choice.targetType),
            new Damage(Damage.StringToDamageType(choice.damageType), 10),
            new List<StatusEffect>()
        );

        foreach (StatusResponse effect in choice.statusEffects) {
            action.statusEffects.Add(StatusEffect.StringToStatusEffect(effect.effectName));
        }

        string debugMessage = "Action Name: " + action.name + "\n" +
            "Action Description: " + action.description + "\n" +
            "Target Type: " + Action.TargetTypeToString(action.targetType) + "\n" +
            "Damage Type: " + Damage.DamageTypeToString(action.damage.damageType) + "\n" +
            "Effects: ";
        foreach (StatusEffect effect in action.statusEffects) {
            debugMessage += effect.name + ", ";
        }
        
        Debug.Log(debugMessage);
        actionMessages.Add(new Message(Role.Assistant, debugMessage));

        return action;
    }
}

public class UnitResponse {
    [JsonProperty ("unitName")]
    public string unitName { get; set; }
    [JsonProperty ("unitDescription")]
    public string unitDescription { get; set; }
    [JsonProperty ("unitLevel")]
    public int unitLevel { get; set; }
    [JsonProperty ("attack")]
    public int attack { get; set; }
    [JsonProperty ("magicDefence")]
    public int magicDefence { get; set; }
    [JsonProperty ("defense")]
    public int defense { get; set; }
    [JsonProperty ("maxHP")]
    public int maxHP { get; set; }

}

public class ActionResponse {
    [JsonProperty ("name")]
    public string name { get; set; }
    [JsonProperty ("description")]
    public string description { get; set; }
    [JsonProperty ("targetType")]
    public string targetType { get; set; }
    [JsonProperty ("damageType")]
    public string damageType { get; set; }
    [JsonProperty ("statusEffects")]
    public List<StatusResponse> statusEffects { get; set; }
}

public class StatusResponse {
    [JsonProperty ("name")]
    public string effectName { get; set; }
    [JsonProperty ("duration")]
    public int duration { get; set; }
}