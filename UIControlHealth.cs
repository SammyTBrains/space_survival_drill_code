using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIControlHealth : MonoBehaviour
{
    [SerializeField]
    private Text healthText;
    [SerializeField]
    private AudioClip clickButton;
    [SerializeField]
    private GameObject bgSound;

    private void Update()
    {
        int musicBg = PlayerPrefs.GetInt("MusicON");
        if(musicBg == 1)
        {
            bgSound.SetActive(true);
        }
        else
        {
            bgSound.SetActive(false);
        }
        if (PlayerPrefs.HasKey("healthNum"))
        {
            int healthNo = PlayerPrefs.GetInt("healthNum");
            healthText.text = healthNo.ToString();
        }
        else
        {
            PlayerPrefs.SetInt("healthNum", 10);
            healthText.text = "10";
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
        if(Application.platform == RuntimePlatform.Android)
        {
            if (Input.GetKey(KeyCode.Escape))
            {
                SceneManager.LoadScene(1);
            }
        }
    }
}
