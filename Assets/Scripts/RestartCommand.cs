using UnityEngine.SceneManagement;

public class RestartCommand : ICommand
{
    public void Execute()
    {
        // Reloads the active scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}