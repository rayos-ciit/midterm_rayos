using UnityEngine;

public class PlayingState : IGameState
{
    private GameController_Messy game;

    public PlayingState(GameController_Messy gameController)
    {
        game = gameController;
    }

    public void Enter()
    {
        Time.timeScale = 1f;
    }

    public void Tick()
    {
        // Only listen for shooting and pausing while playing
        game.inputHandler.HandleGameplayInput();
        game.inputHandler.HandlePauseInput();
        
        // Tell the spawner to do its thing
        if (game.spawner != null)
        {
            game.spawner.TickSpawner();
        }
    }

    public void Exit() { }
}