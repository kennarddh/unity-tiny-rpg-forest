using System;
using System.Collections;
using System.Collections.Generic;

using TMPro;

using UnityEngine;

public enum DialogActionTypeEnum
{
    Text,
    Buttons,
}

public struct DialogActionButton
{
    public string text;
    public Action callback;
}

public struct DialogAction
{
    public DialogActionTypeEnum type;
    public string text;
    public DialogActionButton[] buttons;
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

        ShowDialog(new DialogAction[]
        {
            new DialogAction()
            {
                type = DialogActionTypeEnum.Text,
                text = "This is a dialog.\nthis is a new line.",
            },
            new DialogAction()
            {
                type = DialogActionTypeEnum.Text,
                text = "Hi this is the second text",
            },
            new DialogAction()
            {
                type = DialogActionTypeEnum.Buttons,
                buttons = new DialogActionButton[] {
                    new DialogActionButton {
                        text = "OK",
                        callback = () => print("OK")
                    },
                    new DialogActionButton {
                        text = "Next",
                        callback = () => AddAction(
                            new DialogAction()
                            {
                                type = DialogActionTypeEnum.Text,
                                text = "Hi this is an additional text",
                            }
                        )
                    }
                }
            },
            new DialogAction()
            {
                type = DialogActionTypeEnum.Text,
                text = "Hi this is the third text",
            },
        });
    }

    public void AddAction(DialogAction action)
    {
        actions.Add(action);
    }

    public void ShowDialog(DialogAction[] actionsParam)
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

            if (action.type == DialogActionTypeEnum.Text)
            {
                textUI.text = action.text;

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
            else if (action.type == DialogActionTypeEnum.Buttons)
            {
                bool isClicked = false;

                foreach (DialogActionButton buttonData in action.buttons)
                {
                    GameObject button = Instantiate(buttonPrefab, buttonsContainer);

                    DialogButton dialogButton = button.GetComponent<DialogButton>();

                    dialogButton.OnClick(buttonData.text, delegate ()
                    {
                        isClicked = true;

                        buttonData.callback();
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