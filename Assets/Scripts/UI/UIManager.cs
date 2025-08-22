using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    [SerializeField] private Button resetButton;
    [SerializeField] private Button[] gridButtonsArray = new Button[9];
    [SerializeField] private Button winnerPopup;

    public Button ResetButton { get => resetButton; set => resetButton = value; }
    public Button WinnerPopup { get => winnerPopup; set => winnerPopup = value; }
    public List<Button> GridButtons { get; private set; } = new();

    private void Start()
    {
        GridButtons = new List<Button>(gridButtonsArray);

        foreach (Button button in GridButtons)
        {
            Button btn = button;
            button.onClick.AddListener(() => GameManager.Instance.OnButtonClick(btn));
        }

        ResetButton.onClick.AddListener(() => GameManager.Instance.OnReset());
        WinnerPopup.onClick.AddListener(() => GameManager.Instance.OnReset());
    }
}