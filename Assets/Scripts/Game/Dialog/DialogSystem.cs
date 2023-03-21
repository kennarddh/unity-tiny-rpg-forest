using System.Collections;
using System.Collections.Generic;

using TMPro;

using UnityEngine;

namespace Game.Dialog
{
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
        }

        public void AddAction(params DialogAction[] action)
        {
            actions.AddRange(action);
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
}