using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager>
{
    [SerializeField, ReadOnlyInspector] private bool isGameOver = false;
    [SerializeField, ReadOnlyInspector] private bool isPlayerTurn = true;
    [SerializeField, ReadOnlyInspector] private bool isDraw = false;

    public bool IsGameOver { get => isGameOver; set => isGameOver = value; }
    public bool IsPlayerTurn { get => isPlayerTurn; set => isPlayerTurn = value; }

    private readonly int[][] winConditions =
    {
        new[] { 0, 1, 2 },
        new[] { 3, 4, 5 },
        new[] { 6, 7, 8 },
        new[] { 0, 3, 6 },
        new[] { 1, 4, 7 },
        new[] { 2, 5, 8 },
        new[] { 0, 4, 8 },
        new[] { 2, 4, 6 }
    };

    private void Start()
    {
        OnReset();
    }

    public void OnButtonClick(Button button)
    {
        if (IsGameOver || !button.interactable)
            return;

        SetButtonText(button, IsPlayerTurn ? "X" : "O");
        button.interactable = false;
        CheckForEnding();
        GetNextTurn();
    }

    public void OnReset()
    {
        foreach (Button button in UIManager.Instance.GridButtons)
        {
            button.interactable = true;
            SetButtonText(button, string.Empty);
        }

        IsGameOver = false;
        IsPlayerTurn = true;
        isDraw = false;

        UIManager.Instance.WinnerPopup.gameObject.SetActive(false);
    }

    public void CheckForEnding()
    {
        if (!CheckForWinner())
        {
            CheckForDraw();
        }
    }

    public bool CheckForWinner()
    {
        foreach (var condition in winConditions)
        {
            string grid1 = GetButtonText(UIManager.Instance.GridButtons[condition[0]]);
            string grid2 = GetButtonText(UIManager.Instance.GridButtons[condition[1]]);
            string grid3 = GetButtonText(UIManager.Instance.GridButtons[condition[2]]);

            if (!string.IsNullOrEmpty(grid1) && grid1 == grid2 && grid2 == grid3)
            {
                isDraw = false;
                ShowWinnerPopup(grid1);
                EndGame();
                return true;
            }
        }
        return false;
    }

    public void CheckForDraw()
    {
        Debug.Log("Checking for draw...");
        isDraw = true;
        foreach (Button button in UIManager.Instance.GridButtons)
        {
            if (button.interactable)
            {
                isDraw = false;
                break;
            }
        }

        if (isDraw)
        {
            ShowWinnerPopup();
            EndGame();
        }
    }

    public void EndGame()
    {
        IsGameOver = true;
    }

    public void ShowWinnerPopup(string winner = "")
    {
        UIManager.Instance.WinnerPopup.gameObject.SetActive(true);
        TMP_Text winnerText = UIManager.Instance.WinnerPopup.GetComponentInChildren<TMP_Text>();
        if (winnerText != null)
        {
            winnerText.text = isDraw ? "Draw!" : $"Player {winner} Wins!";
        }
    }

    public bool GetNextTurn()
    {
        if (IsGameOver)
            return false;

        IsPlayerTurn = !IsPlayerTurn;
        return IsPlayerTurn;
    }

    private void SetButtonText(Button button, string text)
    {
        TMP_Text tmp = button.GetComponentInChildren<TMP_Text>();
        if (tmp != null)
            tmp.text = text;
    }

    private string GetButtonText(Button button)
    {
        TMP_Text tmp = button.GetComponentInChildren<TMP_Text>();
        return tmp != null ? tmp.text : string.Empty;
    }
}