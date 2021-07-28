using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGSoundSettingsManger : MonoBehaviour
{
    [SerializeField]
    private GameObject bgSound;


    private void Update()
    {
        int musicBg = PlayerPrefs.GetInt("MusicON");
        if (musicBg == 1)
        {
            bgSound.SetActive(true);
        }
        else
        {
            bgSound.SetActive(false);
        }
    }
}
