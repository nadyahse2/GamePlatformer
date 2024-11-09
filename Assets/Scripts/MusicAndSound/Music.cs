
using UnityEngine;

public class Music : MonoBehaviour
{
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;

    public AudioClip background;
    public AudioClip death;
    public AudioClip checkpoint;
    public AudioClip walltouch;

    private void Start()
    {
        musicSource.clip = background;
        musicSource.Play();
    }
}
