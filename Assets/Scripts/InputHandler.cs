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
        //initialize commands
        shootCommand = new ShootCommand(firePoint, bulletSpeed);
        pauseCommand = new PauseCommand();
        restartCommand = new RestartCommand();
    }
    
    
    //separated methods allows the state machine to decide what the game is allowed to listen to specific inputs.

    public void HandleGameplayInput()
    {
        if (Input.GetMouseButtonDown(0)) 
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