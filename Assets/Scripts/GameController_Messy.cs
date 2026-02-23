using UnityEngine;

public class GameController_Messy : MonoBehaviour
{
    public static GameController_Messy I;

    [Header("Dependencies")]
    public InputHandler inputHandler;
    public EnemySpawner_Messy spawner;

    //game state variables
    private IGameState currentState;
    public PlayingState playingState;
    public PausedState pausedState;
    public GameOverState gameOverState;

    void Awake()
    {
        I = this;
    }

    void Start()
    {
        //initialize
        playingState = new PlayingState(this);
        pausedState = new PausedState(this);
        gameOverState = new GameOverState(this);

        //always starts the game in playingstate
        ChangeState(playingState);
    }

    void Update()
    {
        //gameController only runs the current state's Tick()
        if (currentState != null)
        {
            currentState.Tick();
        }
    }

    public void ChangeState(IGameState newState)
    {
        if (currentState != null)
        {
            currentState.Exit();
        }

        currentState = newState;
        currentState.Enter();
    }

    //pressing "esc" triggers pause and untriggers it based on what the current state is 
    public void TogglePause()
    {
        if (currentState == playingState)
            ChangeState(pausedState);
        else if (currentState == pausedState)
            ChangeState(playingState);
    }

    //calls gameover
    public void GameOver()
    {
        ChangeState(gameOverState);
    }
}