using System;
using System.Collections;
using System.Collections.Generic;

using TMPro;

using UnityEngine;

public enum DialogActionType
{
    Text,
    Buttons,
}

public class DialogActionButton
{
    public string Text;
    public Action Callback;
}

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

public class DialogSystem : MonoBehaviour
{
    public static DialogSystem Instance { get; private set; }

    [SerializeField]
    private TextMeshProUGUI textUI;

    [SerializeField]
    private GameObject dialog;

    [SerializeField]
    private GameObject buttonPrefab;

    [SerializeField]
    private Transform buttonsContainer;

    private List<DialogAction> actions;

    private void Start()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            DontDestroyOnLoad(this);
            Instance = this;
        }

        ShowDialog(
            DialogAction.NewText("This is a dialog.\nthis is a new line."),
            DialogAction.NewText("Hi this is the second text"),
            DialogAction.NewButtons(
                DialogAction.NewButton("OK", () => print("OK")),
                DialogAction.NewButton("Next", () => AddAction(DialogAction.NewText("Hi this is an additional text")))
            ),
            DialogAction.NewText("Hi this is the third text")
        );
    }

    public void AddAction(DialogAction action)
    {
        actions.Add(action);
    }

    public void ShowDialog(params DialogAction[] actionsParam)
    {
        actions = new(actionsParam);

        StartCoroutine(ShowDialogIEnumerator());
    }

    private IEnumerator ShowDialogIEnumerator()
    {
        dialog.SetActive(true);

        // Use while instead of foreach loop because array can be changed during loop
        int count = 0;

        while (actions.Count != count)
        {
            DialogAction action = actions[count];

            if (action.Type == DialogActionType.Text)
            {
                textUI.text = action.Text;

                while (!Input.GetKeyDown(KeyCode.Space))
                {
                    yield return null;
                }

                while (Input.GetKeyDown(KeyCode.Space))
                {
                    yield return null;
                }

                textUI.text = "";
            }
            else if (action.Type == DialogActionType.Buttons)
            {
                bool isClicked = false;

                foreach (DialogActionButton buttonData in action.Buttons)
                {
                    GameObject button = Instantiate(buttonPrefab, buttonsContainer);

                    DialogButton dialogButton = button.GetComponent<DialogButton>();

                    dialogButton.OnClick(buttonData.Text, delegate ()
                    {
                        isClicked = true;

                        buttonData.Callback();
                    });
                }

                while (!isClicked)
                {
                    yield return null;
                }

                foreach (Transform child in buttonsContainer)
                {
                    Destroy(child.gameObject);
                }
            }

            count += 1;
        }

        dialog.SetActive(false);
    }
}