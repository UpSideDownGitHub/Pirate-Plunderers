using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static AudioClip PlayerMoveSound, PlayerDeathSound;
    static AudioSource audioSrc;

    // Start is called before the first frame update
    void Start()
    {
        PlayerDeathSound = Resources.Load<AudioClip>("DeathSound");
        PlayerMoveSound = Resources.Load<AudioClip>("PlayerMove");

        audioSrc = GetComponent<AudioSource> ();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public static void PlaySound (string clip)
    {
        switch (clip)
        {
            case "Deathsound":
                audioSrc.PlayOneShot(PlayerDeathSound);
                break;
            case "PlayerMove":
                audioSrc.PlayOneShot(PlayerMoveSound);
                break;

        }
    }
}
