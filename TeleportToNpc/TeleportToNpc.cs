using BepInEx;
using BepInEx.Logging;
using UnityEngine;
using Wish;
using HarmonyLib;
using TeleportToNpc;

[BepInPlugin(pluginGuid, pluginName, pluginVersion)]
public class TeleportToNpcPlugin : BaseUnityPlugin
{
    private const string pluginGuid = "expired.sunhaven.teleporttonpc";
    private const string pluginName = "TeleportToNPC";
    private const string pluginVersion = "0.0.1";
    private Harmony m_harmony = new Harmony(pluginGuid);
    public static ManualLogSource logger;

    private void Awake()
    {
        logger = Logger;

        m_harmony.PatchAll(typeof(Hook_Relationships));
    }
    public static void TeleportToNPC(NPCAI npcai)
    {
        Vector2 destination = npcai.InteractionPoint.transform.position;
        if(npcai.Scene != ScenePortalManager.ActiveSceneName)
        {
            // NPC is in another scene, teleport us to that scene
            SingletonBehaviour<ScenePortalManager>.Instance.ChangeScene(destination, npcai.Scene, onLoad: () => {});
        }
        else
        {
            Player.Instance.SetPosition(destination);
        }
    }

}

