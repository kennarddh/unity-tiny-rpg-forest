using System;

namespace Game.Dialog
{
    [Serializable]
    public class DialogActionButton
    {
        public string Text;
        public Action Callback;
    }
}