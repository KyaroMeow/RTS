using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CloseButton : MonoBehaviour
{
    
   public void Close()
    {
        Application.Quit();
    }
    public void NextScene()
    {
        SceneManager.LoadScene("GameScene");
    }

}
