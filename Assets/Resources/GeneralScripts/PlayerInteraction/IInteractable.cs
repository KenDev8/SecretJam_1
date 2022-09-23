using UnityEngine;

namespace KenDev
{
    public interface IInteractable
    {
        void Interact(InteractionType type = InteractionType.LeftClick);
    }
}
