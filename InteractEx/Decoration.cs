using HarmonyLib;
using Wish;

namespace InteractEx
{
    class Hook_Decoration
    {
        [HarmonyPatch(typeof(Decoration), "OnDestroyed")]
        [HarmonyPostfix]
        private static void Hook_OnDestroyed(Decoration __instance)
        {
            Hook_PlayerInteractions._proxyDecoration.Remove(__instance);
        }
    }
}
