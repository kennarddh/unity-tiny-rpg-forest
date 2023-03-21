using Game.Dialog;

using UnityEngine;

namespace Npc
{
    public class DialogNpc : NpcBase
    {
        [SerializeField]
        private DialogAction[] dialogActions;

        protected override void OnInteract()
        {
            DialogSystem.Instance.ShowDialog(dialogActions);
        }
    }
}
