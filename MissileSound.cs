using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileSound : MonoBehaviour
{
    AudioSource soundSource;

    // Start is called before the first frame update
    void Start()
    {
        soundSource = this.gameObject.GetComponent<AudioSource>();
        int soundEffect = PlayerPrefs.GetInt("SoundON");
        if (soundEffect == 1)
        {
            soundSource.Play();
        }
    }
}
