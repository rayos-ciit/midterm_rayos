using UnityEngine;

public class GameController_Messy : MonoBehaviour
{
    public static GameController_Messy I;

    [Header("Dependencies")]
    public InputHandler inputHandler;
    public EnemySpawner_Messy spawner;

    // State Machine Variables
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
        // Initialize States
        playingState = new PlayingState(this);
        pausedState = new PausedState(this);
        gameOverState = new GameOverState(this);

        // Start the game in the playing state
        ChangeState(playingState);
    }

    void Update()
    {
        // The GameController only runs the current state's Tick() now!
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

    // --- METHODS CALLED BY COMMANDS OR OTHER SCRIPTS ---

    // THIS FIXES YOUR COMPILER ERROR!
    public void TogglePause()
    {
        if (currentState == playingState)
            ChangeState(pausedState);
        else if (currentState == pausedState)
            ChangeState(playingState);
    }

    public void GameOver()
    {
        ChangeState(gameOverState);
    }
}