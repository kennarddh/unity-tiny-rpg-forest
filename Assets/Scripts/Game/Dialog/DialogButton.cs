using System;

using TMPro;

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class DialogButton : MonoBehaviour
{
    public TextMeshProUGUI textUI;

    [SerializeField]
    private Button button;

    private void Awake()
    {
        button = GetComponent<Button>();
    }

    public void OnClick(string text, Action callback)
    {
        textUI.text = text;

        button.onClick.AddListener(new UnityAction(callback));
    }
}