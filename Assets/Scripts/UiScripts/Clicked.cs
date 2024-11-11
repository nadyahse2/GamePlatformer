using UnityEngine.SceneManagement;
using UnityEngine;

public class Clicked : MonoBehaviour
{
    [SerializeField] AudioSource Sound;
    public AudioClip OnClicked;

    private string NextScene;
    public void OnCliked(string Name)
    {
        Sound.clip = OnClicked;
        Sound.Play();
        Time.timeScale = 1f;
        if (Name != "")
        {
            Invoke("LoadNextScene", OnClicked.length * 2);
            NextScene = Name;
        }
    }
    
    private void LoadNextScene()
    {
        SceneManager.LoadScene(NextScene);
    }

    public void QuitButton()
    {
        Sound.clip = OnClicked;
        Sound.Play();
        Application.Quit();
    }

    public GameObject PausePanel;

    public void OpenMenu()
    {
        Sound.clip = OnClicked;
        Sound.Play();
        PausePanel.SetActive(true);
        Time.timeScale = 0f;
    }
    public void ContinueButtonPressed()
    {
        Sound.clip = OnClicked;
        Sound.Play();
        PausePanel.SetActive(false);
        Time.timeScale = 1f;
    }
    public void RestartButtonPressed()
    {
        Sound.clip = OnClicked;
        Sound.Play();
        PausePanel.SetActive(false);
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void BackToMenuButtonPressed()
    {
        PausePanel.SetActive(false);
        Time.timeScale = 1f;
        SceneManager.LoadScene("Levels");
    }
}
