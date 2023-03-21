using System;

namespace Game.Dialog
{
    [Serializable]
    public class DialogAction
    {
        public DialogActionType Type;
        public string Text;
        public DialogActionButton[] Buttons;

        public static DialogAction NewText(string text)
        {
            return new DialogAction
            {
                Type = DialogActionType.Text,
                Text = text
            };
        }

        public static DialogAction NewButtons(params DialogActionButton[] buttons)
        {
            return new DialogAction
            {
                Type = DialogActionType.Buttons,
                Buttons = buttons
            };
        }

        public static DialogActionButton NewButton(string text, Action callback)
        {
            return new DialogActionButton
            {
                Text = text,
                Callback = callback
            };
        }
    }
}