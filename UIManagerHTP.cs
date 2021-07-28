using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManagerHTP : MonoBehaviour
{
    [SerializeField]
    private AudioClip clickButton;
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

    public void BackToMainMenu()
    {
        PlayClipAtPoint();
        SceneManager.LoadScene(1);
        if (Application.platform == RuntimePlatform.Android)
        {
            if (Input.GetKey(KeyCode.Escape))
            {
                SceneManager.LoadScene(1);
            }
        }
    }
}
