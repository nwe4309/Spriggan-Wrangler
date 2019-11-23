using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuMusicPlayer : MonoBehaviour
{
    public AudioSource music;

    // Start is called before the first frame update
    void Start()
    {
        
        music = GetComponent<AudioSource>();
        PlayMusic();

        StopGameMusic();
    }

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void PlayMusic()
    {
        if (music.isPlaying) return;
        music.Play();
    }

    public void StopGameMusic()
    {
        Destroy(GameObject.Find("GameMusicPlayer"));
    }

    public void StopMusic()
    {
        music.Stop();
    }
}
