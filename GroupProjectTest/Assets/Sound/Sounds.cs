using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sounds : MonoBehaviour
{
    public AudioSource effectSource;
    public AudioSource musicSource;
    public AudioClip[] music;

    int previous;
    public void Start()
    {
        int ranNum = Random.Range(0, music.Length);
        previous = ranNum;
        musicSource.PlayOneShot(music[ranNum]);
    }
    public void Update()
    {
        if (!musicSource.isPlaying)
        {
            int ranNum;
            do
            {
                ranNum = Random.Range(0, music.Length);
            } while (ranNum == previous);

            musicSource.PlayOneShot(music[ranNum]);
        }
    }
    public void playSoundEffect(AudioClip clip, float volume)
    {
        effectSource.PlayOneShot(clip, volume);
    }
}
