using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
    public Button playButton;
    public string nextSceneName;

    void Start()
    {
        playButton.onClick.AddListener(delegate { LoadNextScene(); });
    }

    void LoadNextScene()
    {
        SceneManager.LoadScene(nextSceneName);
    }
}
