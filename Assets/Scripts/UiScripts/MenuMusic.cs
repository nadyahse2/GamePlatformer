using UnityEngine;

public class MenuMusic : MonoBehaviour
{
    [SerializeField] AudioSource SFX;
    [SerializeField] AudioSource Music;
    public AudioClip Sound;
    public AudioClip ClickButton;
    public void Start()
    {
        Music.clip = Sound;
        Music.Play();
    }
    
}
