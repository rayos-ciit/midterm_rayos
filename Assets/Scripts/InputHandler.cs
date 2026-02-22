using UnityEngine;

public class InputHandler : MonoBehaviour
{
    private ICommand shootCommand;
    private ICommand pauseCommand;
    private ICommand restartCommand;

    [Header("Shoot Settings")]
    public Transform firePoint;
    public float bulletSpeed = 25f;

    void Start()
    {
        // Initialize the commands
        shootCommand = new ShootCommand(firePoint, bulletSpeed);
        pauseCommand = new PauseCommand();
        restartCommand = new RestartCommand();
    }

    // By separating these into methods, our upcoming State Machine can decide 
    // exactly WHEN the game is allowed to listen to these inputs!

    public void HandleGameplayInput()
    {
        if (Input.GetMouseButton(0))
        {
            shootCommand.Execute();
        }
    }

    public void HandlePauseInput()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pauseCommand.Execute();
        }
    }

    public void HandleRestartInput()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            restartCommand.Execute();
        }
    }
}