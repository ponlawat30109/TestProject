using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    [SerializeField] private Button resetButton;
    [SerializeField] private Button grid1;
    [SerializeField] private Button grid2;
    [SerializeField] private Button grid3;
    [SerializeField] private Button grid4;
    [SerializeField] private Button grid5;
    [SerializeField] private Button grid6;
    [SerializeField] private Button grid7;
    [SerializeField] private Button grid8;
    [SerializeField] private Button grid9;


    public Button ResetButton { get => resetButton; set => resetButton = value; }
    public Button Grid1 { get => grid1; set => grid1 = value; }
    public Button Grid2 { get => grid2; set => grid2 = value; }
    public Button Grid3 { get => grid3; set => grid3 = value; }
    public Button Grid4 { get => grid4; set => grid4 = value; }
    public Button Grid5 { get => grid5; set => grid5 = value; }
    public Button Grid6 { get => grid6; set => grid6 = value; }
    public Button Grid7 { get => grid7; set => grid7 = value; }
    public Button Grid8 { get => grid8; set => grid8 = value; }
    public Button Grid9 { get => grid9; set => grid9 = value; }


    [Space(10)]
    [SerializeField, ReadOnlyInspector] private List<Button> gridButtons = new();
    public List<Button> GridButtons { get => gridButtons; set => gridButtons = value; }

    [Space(10)]
    [SerializeField] private Button winnerPopup;
    public Button WinnerPopup { get => winnerPopup; set => winnerPopup = value; }


    private void Start()
    {
        GridButtons.Add(Grid1);
        GridButtons.Add(Grid2);
        GridButtons.Add(Grid3);
        GridButtons.Add(Grid4);
        GridButtons.Add(Grid5);
        GridButtons.Add(Grid6);
        GridButtons.Add(Grid7);
        GridButtons.Add(Grid8);
        GridButtons.Add(Grid9);

        foreach (Button button in GridButtons)
        {
            button.onClick.AddListener(() => GameManager.Instance.OnButtonClick(button));
        }

        ResetButton.onClick.AddListener(() => GameManager.Instance.OnReset());
        WinnerPopup.onClick.AddListener(() => GameManager.Instance.OnReset());
    }
}
