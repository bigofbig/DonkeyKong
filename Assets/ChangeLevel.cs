using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeLevel : MonoBehaviour
{

    public void ChangeToHowHighCanYouGet()
    {
        SceneManager.LoadScene("HowHigh");
    }
    public void ChangeToGameScene()
    {
        SceneManager.LoadScene("Game");
    }
}
