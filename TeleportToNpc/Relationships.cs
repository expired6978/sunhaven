using HarmonyLib;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using Wish;

namespace TeleportToNpc
{
    public class Hook_Relationships
    {
        [HarmonyPatch(typeof(Relationships), "SetupRelationshipPanel")]
        [HarmonyPostfix]
        private static void Hook_SetupRelationshipPanel(Relationships __instance, Dictionary<string, RelationshipPanel> ___relationshipPanels)
        {
            foreach(var relationshipPanel in ___relationshipPanels)
            {
                // Add a Click handler to the Relationship icon
                relationshipPanel.Value.marriedImage.gameObject
                    .AddComponent<RelationButton>()
                    .SetName(relationshipPanel.Key);
            }
        }
    }

    public class RelationButton : 
        MonoBehaviour, 
        IPointerDownHandler,
        IEventSystemHandler
    {
        public string npcName { get; set; }

        public RelationButton SetName(string name)
        {
            npcName = name;
            return this;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (PlayerInput.GetMouseButtonDown(0))
            {
                if(UIHandler.InventoryOpen)
                {
                    UIHandler.Instance.CloseExternalUI();
                    UIHandler.Instance.CloseInventory();
                }

                NPCAI npc = SingletonBehaviour<NPCManager>.Instance.GetNPC(npcName);
                if (npc != null)
                {
                    TeleportToNpcPlugin.TeleportToNPC(npc);
                }
            }
        }
    }
}
