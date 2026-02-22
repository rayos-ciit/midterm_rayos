public class PauseCommand : ICommand
{
    public void Execute()
    {
        // We will hook this into the State Machine in our very next step!
        if (GameController_Messy.I != null)
        {
            GameController_Messy.I.TogglePause();
        }
    }
}