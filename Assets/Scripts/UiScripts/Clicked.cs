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
    public Button l2;
    public Button l3;
    public Button l4;
    public Button l5;

    private void Start()
    {
        if (l2 != null) l2.interactable = false;
        if (l3 != null) l3.interactable = false;
        if (l4 != null) l4.interactable = false;
        if (l5 != null) l5.interactable = false;


        if (PlayerPrefs.GetInt("L0") == 1 )
        {
            if (l2 != null) l2.interactable = true;
        }
        if (PlayerPrefs.GetInt("L1") == 1)
        {
            if (l3 != null) l3.interactable = true;
        }
        if (PlayerPrefs.GetInt("L2") == 1)
        {
            if (l4 != null) l4.interactable = true;
        }
        if (PlayerPrefs.GetInt("L3") == 1)
        {
            if (l5 != null) l5.interactable = true;
        }

    }
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
    //Обработка выхода игрока без завершения уровня(сохранение данных игрока) 
    public void Exit()
    {
        Hero hero = player.GetComponent<Hero>();
        if(hero.Check_die == false && hero.Check_die == false)
        {
            Vector3 pos = player.transform.position;
            int ind_scene = SceneManager.GetActiveScene().buildIndex;
            string Posx = "PosX" + ind_scene;
            string Posy = "PosY" + ind_scene;
            string Posz = "PosZ" + ind_scene;
            string Coins = "coins" + ind_scene;
            string liv = "hearts" + ind_scene;
            PlayerPrefs.SetFloat(Posx, pos.x);
            PlayerPrefs.SetFloat(Posy, pos.y);
            PlayerPrefs.SetFloat(Posz, pos.z);
            PlayerPrefs.SetInt(liv, hero.lives);
            PlayerPrefs.SetInt(Coins, hero.coins);
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
