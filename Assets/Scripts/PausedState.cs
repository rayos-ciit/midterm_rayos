using UnityEngine;

public class PausedState : IGameState
{
    private GameController_Messy game;

    public PausedState(GameController_Messy gameController)
    {
        game = gameController;
    }

    public void Enter()
    {
        Time.timeScale = 0f; // Freeze game
    }

    public void Tick()
    {
        // Only listen for the unpause button
        game.inputHandler.HandlePauseInput();
    }

    public void Exit() { }
}