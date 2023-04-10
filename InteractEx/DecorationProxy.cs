using System.Collections.Generic;
using Wish;

namespace InteractEx
{
    public class DecorationProxy : IInteractable
    {
        public Decoration decoration;

        public DecorationProxy(Decoration decoration)
        {
            this.decoration = decoration;
        }
        public void Target()
        {
        }

        public void UnTarget()
        {
        }

        public int ID => decoration.placeable != null && decoration.placeable._itemData != null ? decoration.placeable._itemData.id : 0;

        public string Name => decoration.placeable != null && decoration.placeable._itemData != null ? decoration.placeable._itemData.name : null;

        SortedSet<CustomInteraction> GetInteractionList()
        {
            SortedSet<CustomInteraction> customInteractions = new SortedSet<CustomInteraction>();

            if(CustomInteractions.s_interaction.TryGetValue(ID, out var localInteractions))
            {
                customInteractions.UnionWith(localInteractions);
            }
            if (CustomInteractions.s_interaction.TryGetValue(-1, out var globalInteractions))
            {
                customInteractions.UnionWith(globalInteractions);
            }

            return customInteractions;
        }

        public virtual InteractionInfo InteractionPoint
        {
            get
            {
                string sourceText = ID != 0 ? Name : "Interact";
                bool canInteract = false;
                foreach(CustomInteraction interaction in GetInteractionList())
                {
                    bool allowed = interaction?.canInteract(this) ?? false;
                    canInteract |= allowed;
                    if (allowed)
                    {
                        if (interaction?.interactText(this, ref sourceText) ?? false)
                            break;
                    }
                }
                return new InteractionInfo
                {
                    transform = decoration.InteractionPoint.transform,
                    interactionPoint = decoration.InteractionPoint.interactionPoint,
                    canInteract = canInteract,
                    interactionText = new List<string> { sourceText },
                };
            }
        }

        public virtual void Interact(int interactType)
        {
            foreach (CustomInteraction interaction in GetInteractionList())
            {
                if ((interaction?.canInteract(this) ?? false) && (interaction?.onInteract(this) ?? false))
                {
                    break;
                }
            }
        }

        public virtual void EndInteract(int interactType)
        {
        }
    }
}
