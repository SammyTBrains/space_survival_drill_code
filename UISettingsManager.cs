using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UISettingsManager : MonoBehaviour
{
    [SerializeField]
    private GameObject musicCheck, musicCross;
    [SerializeField]
    private GameObject soundCheck, soundCross;
    [SerializeField]
    private AudioClip clickButton;
  
    private void Start()
    {
        if (PlayerPrefs.HasKey("MusicON"))
        {
            int musicON = PlayerPrefs.GetInt("MusicON");

            if (musicON == 1)
            {
                musicCheck.SetActive(true);
                musicCross.SetActive(false);
            }
            else
            {
                musicCheck.SetActive(false);
                musicCross.SetActive(true);
            }
        }
        else
        {
            musicCheck.SetActive(true);
            musicCross.SetActive(false);
        }

        if (PlayerPrefs.HasKey("SoundON"))
        {
            int soundON = PlayerPrefs.GetInt("SoundON");

            if (soundON == 1)
            {
                soundCheck.SetActive(true);
                soundCross.SetActive(false);
            }
            else
            {
                soundCheck.SetActive(false);
                soundCross.SetActive(true);
            }
        }
        else
        {
            soundCheck.SetActive(true);
            soundCross.SetActive(false);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(1);
        }
    }

    void PlayClipAtPoint()
    {
        int soundEffect = PlayerPrefs.GetInt("SoundON");
        if (soundEffect == 1)
        {
            AudioSource.PlayClipAtPoint(clickButton, new Vector3(0, 0, 0));
        }
    }

    public void MusicToggle()
    {
        PlayClipAtPoint();
        if (PlayerPrefs.HasKey("MusicON"))
        {
            int musicON = PlayerPrefs.GetInt("MusicON");

            if (musicON == 1)
            {
                musicCheck.SetActive(false);
                musicCross.SetActive(true);
                PlayerPrefs.SetInt("MusicON", 0);
            }
            else
            {
                musicCheck.SetActive(true);
                musicCross.SetActive(false);
                PlayerPrefs.SetInt("MusicON", 1);
            }
        }
        else
        {
            musicCheck.SetActive(false);
            musicCross.SetActive(true);
            PlayerPrefs.SetInt("MusicON", 0);
        }
    }

    public void SoundsToggle()
    {
        PlayClipAtPoint();
        if (PlayerPrefs.HasKey("SoundON"))
        {
            int soundON = PlayerPrefs.GetInt("SoundON");

            if (soundON == 1)
            {
                soundCheck.SetActive(false);
                soundCross.SetActive(true);
                PlayerPrefs.SetInt("SoundON", 0);
            }
            else
            {
                soundCheck.SetActive(true);
                soundCross.SetActive(false);
                PlayerPrefs.SetInt("SoundON", 1);
            }
        }
        else
        {
            soundCheck.SetActive(false);
            soundCross.SetActive(true);
            PlayerPrefs.SetInt("SoundON", 0);
        }
    }
}
