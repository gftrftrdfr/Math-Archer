using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySoundClick : MonoBehaviour
{
    public AudioSource sound;
    public AudioSource sound2;
    public AudioSource sound3;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlaySoundEffect()
    {
        sound.Play();
    }

    public void PlaySoundEffect2()
    {
        sound2.Play();
    }
    public void PlaySoundEffect3()
    {
        sound3.Play();
    }
}
