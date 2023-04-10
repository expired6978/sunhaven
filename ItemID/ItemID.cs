using BepInEx;
using BepInEx.Logging;
using BepInEx.Configuration;
using HarmonyLib;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections.Generic;
using System.Reflection;
using System;
using UnityEngine.SceneManagement;
using Wish;
using Sirenix.Serialization;

namespace Polygamy;

[BepInPlugin(pluginGuid, pluginName, pluginVersion)]
public class ItemIDPlugin : BaseUnityPlugin
{
    private const string pluginGuid = "expired.sunhaven.itemid";
    private const string pluginName = "ItemID";
    private const string pluginVersion = "0.0.1";
    private Harmony m_harmony = new Harmony(pluginGuid);
    public static ManualLogSource logger;
    private static Item _tooltipItem = null;

    private void Awake()
    {
        // Plugin startup logic
        ItemIDPlugin.logger = this.Logger;
        logger.LogInfo((object) $"Plugin {pluginName} is loaded!");
		this.m_harmony.PatchAll();
	}

    [HarmonyPatch(typeof(Tooltip), "SetText")]
    class HarmonyPatch_Tooltip_GetToolTip
    {
        private static void Prefix(Tooltip __instance, ref string name, ref string description, ref int lineHeight)
        {
            if(_tooltipItem != null)
            {
                description = "<size=75%>[ItemId: " + _tooltipItem.ID() + "][Type: " + _tooltipItem.Type.ToString() + "]</size>\n" + description;
                _tooltipItem = null;
            }
        }
    }

    [HarmonyPatch(typeof(ToolItem), "GetToolTip")]
    class HarmonyPatch_ToolItem_GetToolTip
    {
        private static void Prefix(ToolItem __instance, ref Tooltip tooltip, int amount, bool shop)
        {
            _tooltipItem = __instance;
        }
    }

    [HarmonyPatch(typeof(AnimalItem), "GetToolTip")]
    class HarmonyPatch_AnimalItem_GetToolTip
    {
        private static void Prefix(AnimalItem __instance, ref Tooltip tooltip, int amount, bool shop)
        {
            _tooltipItem = __instance;
        }
    }

    [HarmonyPatch(typeof(ArmorItem), "GetToolTip")]
    class HarmonyPatch_ArmorItem_GetToolTip
    {
        private static void Prefix(ArmorItem __instance, ref Tooltip tooltip, int amount, bool shop)
        {
            _tooltipItem = __instance;
        }
    }

    [HarmonyPatch(typeof(CropItem), "GetToolTip")]
    class HarmonyPatch_CropItem_GetToolTip
    {
        private static void Prefix(CropItem __instance, ref Tooltip tooltip, int amount, bool shop)
        {
            _tooltipItem = __instance;
        }
    }

    [HarmonyPatch(typeof(FishItem), "GetToolTip")]
    class HarmonyPatch_FishItem_GetToolTip
    {
        private static void Prefix(FishItem __instance, ref Tooltip tooltip, int amount, bool shop)
        {
            _tooltipItem = __instance;
        }
    }

    [HarmonyPatch(typeof(FoodItem), "GetToolTip")]
    class HarmonyPatch_FoodItem_GetToolTip
    {
        private static void Prefix(FoodItem __instance, ref Tooltip tooltip, int amount, bool shop)
        {
            _tooltipItem = __instance;
        }
    }

    [HarmonyPatch(typeof(NormalItem), "GetToolTip")]
    class HarmonyPatch_NormalItem_GetToolTip
    {
        private static void Prefix(NormalItem __instance, ref Tooltip tooltip, int amount, bool shop)
        {
            _tooltipItem = __instance;
        }
    }

    [HarmonyPatch(typeof(PetItem), "GetToolTip")]
    class HarmonyPatch_PetItem_GetToolTip
    {
        private static void Prefix(PetItem __instance, ref Tooltip tooltip, int amount, bool shop)
        {
            _tooltipItem = __instance;
        }
    }

    [HarmonyPatch(typeof(WateringCanItem), "GetToolTip")]
    class HarmonyPatch_WateringCanItem_GetToolTip
    {
        private static void Prefix(WateringCanItem __instance, ref Tooltip tooltip, int amount, bool shop)
        {
            _tooltipItem = __instance;
        }
    }
}