
using UnityEngine;

public class Music : MonoBehaviour
{
    [SerializeField] AudioSource soundSource;
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;

    public AudioClip background;
    public AudioClip music;
    public AudioClip death;
    public AudioClip checkpoint;
    public AudioClip walltouch;

    private void Start()
    {
        soundSource.clip = background;
        musicSource.clip = music;
        musicSource.Play();
        soundSource.Play();
    }
}
