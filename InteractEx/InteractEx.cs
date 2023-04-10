using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using InteractEx;

[BepInPlugin(pluginGuid, pluginName, pluginVersion)]
public class InteractExPlugin : BaseUnityPlugin
{
    private const string pluginGuid = "expired.sunhaven.interactex";
    private const string pluginName = "InteractEx";
    private const string pluginVersion = "0.0.1";
    private Harmony m_harmony = new Harmony(pluginGuid);
    public static ManualLogSource logger;

    private void Awake()
    {
        logger = Logger;
		m_harmony.PatchAll(typeof(Hook_PlayerInteractions));
        m_harmony.PatchAll(typeof(Hook_Decoration));
        logger.LogInfo($"Plugin {pluginName} is loaded!");
    }
}

