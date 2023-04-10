using BepInEx;
using BepInEx.Logging;
using InteractEx;
using UnityEngine;
using Wish;

[BepInDependency(depGuid, BepInDependency.DependencyFlags.HardDependency)]
[BepInPlugin(pluginGuid, pluginName, pluginVersion)]
public class InteractDebugPlugin : BaseUnityPlugin
{
    private const string depGuid = "expired.sunhaven.interactex";
    private const string pluginGuid = "expired.sunhaven.interactdebug";
    private const string pluginName = "InteractDebug";
    private const string pluginVersion = "0.0.1";
    public static ManualLogSource logger;

    private void Awake()
    {
        logger = Logger;

        // Register a specific item to capture interactions for
        /*CustomInteractions.AddInteraction(10359)
            .AddCanInteract(proxy => true)
            .AddOnInteract(proxy => {
                if (!SingletonBehaviour<NPCManager>.Instance._npcs.TryGetValue("Xyla", out var npcai))
                    return false;
                TeleportToNPC(npcai);
                return true;
            }).AddOnInteractText((DecorationProxy proxy, ref string text) => {
                text = "Teleport to Xyla";
                return true;
            }).Order(0);*/

        // Greedy, capture all Decoration interactions, but specify custom handlers
        CustomInteractions.AddInteraction(-1)
            .AddCanInteract(proxy => true)
            .AddOnInteract(proxy => {
                logger.LogInfo($"Interacted with {proxy.Name}");
                return false;
            }).AddOnInteractText((DecorationProxy proxy, ref string text) =>
            {
                text = proxy.ID != 0 ? "[" + proxy.ID + "] " + proxy.Name : "Interact";
                return false;
            });
        
        logger.LogInfo($"Plugin {pluginName} is loaded!");
    }

    /*public static void TeleportToNPC(NPCAI npcai)
    {
        Vector2 destination = npcai.InteractionPoint.transform.position;
        if (npcai.Scene != ScenePortalManager.ActiveSceneName)
        {
            // NPC is in another scene, teleport us to that scene
            SingletonBehaviour<ScenePortalManager>.Instance.ChangeScene(destination, npcai.Scene, onLoad: () => { });
        }
        else
        {
            Player.Instance.SetPosition(destination);
        }
    }*/
}
