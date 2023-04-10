using HarmonyLib;
using System;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using Wish;

namespace InteractEx
{
    class Hook_PlayerInteractions
    {
        public static Dictionary<Decoration, DecorationProxy> _proxyDecoration = new Dictionary<Decoration, DecorationProxy>();

        static void RemoveInteractableProxy(IInteractable interactable)
        {
            if (interactable is DecorationProxy)
            {
                _proxyDecoration.Remove((interactable as DecorationProxy).decoration);
            }
        }

        static IInteractable GetInteractionProxy(Decoration decoration)
        {
            IInteractable interactable = null;
            if (_proxyDecoration.ContainsKey(decoration))
            {
                interactable = _proxyDecoration[decoration];
            }
            else
            {
                _proxyDecoration[decoration] = new DecorationProxy(decoration);
                interactable = _proxyDecoration[decoration];
            }
            return interactable;
        }

        static IInteractable GetInteractableFromCollider(ref Collider2D collider)
        {
            IInteractable interactable = collider.attachedRigidbody?.GetComponent<IInteractable>() ?? collider.GetComponent<IInteractable>();
            if (interactable == null) // Only if we don't already have an interactable
            {
                // Search for regular decorations that arent normally interactable and create a proxy and register it
                Decoration decoration = collider.attachedRigidbody?.GetComponent<Decoration>() ?? collider.GetComponent<Decoration>();
                if (decoration == null)
                    return null;

                return GetInteractionProxy(decoration);
            }
            return null;
        }

        [HarmonyPatch(typeof(PlayerInteractions), "ClearInteractables", new Type[] { typeof(Scene) })]
        [HarmonyPostfix]
        private static void Hook_ClearInteractables(PlayerInteractions __instance, Scene scene)
        {
            _proxyDecoration.Clear();
        }

        [HarmonyPatch(typeof(PlayerInteractions), "OnTriggerEnter2D")]
        [HarmonyPrefix]
        private static bool Hook_OnTriggerEnter2D(PlayerInteractions __instance, ref Collider2D collider)
        {
            if (!__instance.isActiveAndEnabled)
                return true;

            IInteractable interactable = GetInteractableFromCollider(ref collider);
            if (interactable == null || interactable.InteractionPoint.interactionType == InteractionType.MouseOnly)
                return true;

            if (interactable.InteractionPoint.canInteract)
                __instance.AddInteractable(interactable);
            else
                __instance.RemoveInteractable(interactable);
            return true;
        }
        

        [HarmonyPatch(typeof(PlayerInteractions), "OnTriggerExit2D")]
        [HarmonyPrefix]
        private static bool Hook_OnTriggerExit2D(PlayerInteractions __instance, ref Collider2D collider)
        {
            if (!__instance.isActiveAndEnabled)
                return true;

            IInteractable interactable = GetInteractableFromCollider(ref collider);
            if (interactable == null || interactable.InteractionPoint.interactionType == InteractionType.MouseOnly || !__instance.RemoveInteractable(interactable))
                return true;

            interactable.UnTarget();
            interactable.EndInteract(0);
            RemoveInteractableProxy(interactable);
            return true;
        }
        
    }
}
