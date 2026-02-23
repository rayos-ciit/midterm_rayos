using UnityEngine.SceneManagement;

public class RestartCommand : ICommand
{
    public void Execute()
    {
        //reloads the active scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}