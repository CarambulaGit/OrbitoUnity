using System;
using Codebase.View;
using Logic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameFieldView gameFieldView;
    [SerializeField] private Button orbitoButton;
    [Header("Debug data")]
    [SerializeField] private GameData gameData;

    public Game Game { get; private set; }

    private void Start() => StartGame();

    private void Update()
    {
        if (Game != null) gameData = Game.GetGameData();
    }

    private void StartGame()
    {
        var player1 = new Player(Marble.Type.White);
        var player2 = new Player(Marble.Type.Black);
        Game = new Game(player1, player2);
        gameFieldView.Initialize();
        Subscribe();
        Debug.Log("Game created successfully");
    }
    
    private void Subscribe()
    {
        Game.GameField.OnFieldChanged += UpdateField;
        Game.OnGameEnd += OnGameEnd;
        orbitoButton.onClick.AddListener(Game.Shift);
    }

    private void Unsubscribe()
    {
        Game.GameField.OnFieldChanged -= UpdateField;
        Game.OnGameEnd -= OnGameEnd;
        orbitoButton.onClick.RemoveListener(Game.Shift);
    }

    private void OnGameEnd(PlayerBasic player) => Debug.Log($"Game end\nWinner = {player}");

    private void UpdateField() => 
        gameFieldView.Show(Game.GameField.GetArrayRepresentation());
}