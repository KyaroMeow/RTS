using UnityEngine;
using UnityEngine.UI;

public class ButtonHandler : MonoBehaviour
{
   

    public void StartGame()
    {
        Debug.Log("Game started!");

    }

 

    public void ExitGame()
    {
        Application.Quit();
    }
}
