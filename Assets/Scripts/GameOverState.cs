using UnityEngine;

public class GameOverState : IGameState
{
    private GameController_Messy game;

    public GameOverState(GameController_Messy gameController)
    {
        game = gameController;
    }

    public void Enter()
    {
        Time.timeScale = 0.7f; //slowmo from original code
    }

    public void Tick()
    {
        // Only listen for restart
        game.inputHandler.HandleRestartInput();
    }

    public void Exit() { }
}