using NUnit.Framework;
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

        TMP_Text text = button.GetComponentInChildren<TMP_Text>();
        if (text != null)
            text.text = IsPlayerTurn ? "X" : "O";

        button.interactable = false;
        CheckForEnding();
        GetNextTurn();
    }

    public void OnReset()
    {
        foreach (Button button in UIManager.Instance.GridButtons)
        {
            button.interactable = true;
            TMP_Text text = button.GetComponentInChildren<TMP_Text>();
            if (text != null)
            {
                text.text = string.Empty;
            }
        }

        IsGameOver = false;
        IsPlayerTurn = true;

        UIManager.Instance.WinnerPopup.gameObject.SetActive(false);
    }

    public void CheckForEnding()
    {
        CheckForWinner();
        CheckForDraw();
    }

    public void CheckForWinner()
    {
        foreach (var condition in winConditions)
        {
            var grid1 = UIManager.Instance.GridButtons[condition[0]].GetComponentInChildren<TMP_Text>().text;
            var grid2 = UIManager.Instance.GridButtons[condition[1]].GetComponentInChildren<TMP_Text>().text;
            var grid3 = UIManager.Instance.GridButtons[condition[2]].GetComponentInChildren<TMP_Text>().text;

            if (string.IsNullOrEmpty(grid1) && string.IsNullOrEmpty(grid2) && string.IsNullOrEmpty(grid3))
                return;
            if (grid1 == grid2 && grid2 == grid3)
            {
                ShowWinnerPopup(grid1);
                EndGame();
                return;
            }
        }
    }

    public void CheckForDraw()
    {
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
            ShowWinnerPopup();
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
}
