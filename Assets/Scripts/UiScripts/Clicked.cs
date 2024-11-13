using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class Clicked : MonoBehaviour
{
    [SerializeField] AudioSource Sound;
    public AudioClip OnClicked;

    private string NextScene;
    public GameObject player;
    int count_levels = 5;

    
    public void OnCliked(string Name)
    {
        Sound.clip = OnClicked;
        Sound.Play();
        Time.timeScale = 1f;
        if (Name != "")
        {
            Invoke("LoadNextScene", OnClicked.length * 1);
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
        Vector3 pos = player.transform.position;
        int ind_scene = SceneManager.GetActiveScene().buildIndex;
        string Posx = "PosX"+ind_scene;
        string Posy = "PosY" + ind_scene;
        string Posz = "PosZ" + ind_scene;
        PlayerPrefs.SetFloat(Posx, pos.x);
        PlayerPrefs.SetFloat(Posy, pos.y);
        PlayerPrefs.SetFloat(Posz, pos.z);
        Debug.Log(Posz);
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
    public void QuitGame()
    {
         PlayerPrefs.DeleteAll();
         #if UNITY_EDITOR
                  UnityEditor.EditorApplication.isPlaying= false;
         #else 
                  Application.Quit();
         #endif
             
    }
}
