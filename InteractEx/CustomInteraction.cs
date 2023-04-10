using System;
using System.Collections.Generic;

namespace InteractEx
{
    public class CustomInteraction : IComparable<CustomInteraction>
    {
        public Func<DecorationProxy, bool> canInteract { get; set; }
        public Func<DecorationProxy, bool> onInteract { get; set; }
        public delegate bool InteractText(DecorationProxy decoration, ref string text);
        public InteractText interactText { get; set; }
        public double orderId { get; set; }

        public CustomInteraction AddCanInteract(Func<DecorationProxy, bool> canInteract)
        {
            this.canInteract = canInteract; return this;
        }

        public CustomInteraction AddOnInteract(Func<DecorationProxy, bool> onInteract)
        {
            this.onInteract = onInteract; return this;
        }

        public CustomInteraction AddOnInteractText(InteractText interactText)
        {
            this.interactText = interactText; return this;
        }

        public CustomInteraction Order(double orderId)
        {
            this.orderId = orderId; return this;
        }

        public int CompareTo(CustomInteraction other)
        {
            int result = orderId.CompareTo(other.orderId);
            if (result == 0)
            {
                return GetHashCode().CompareTo(other.GetHashCode());
            }
            return result;
        }
    }

    public class CustomInteractions
    {
        public static Dictionary<int, SortedSet<CustomInteraction>> s_interaction = new Dictionary<int, SortedSet<CustomInteraction>>();

        public static CustomInteraction AddInteraction(int ItemID)
        {
            if(!s_interaction.ContainsKey(ItemID))
            {
                s_interaction.Add(ItemID, new SortedSet<CustomInteraction>());
            }

            CustomInteraction interaction = new CustomInteraction();
            s_interaction[ItemID].Add(interaction);
            return interaction;
        }
    }
}
