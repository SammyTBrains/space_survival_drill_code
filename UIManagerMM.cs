using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManagerMM : MonoBehaviour
{
    [SerializeField]
    private Text healthText;
    [SerializeField]
    private AudioClip clickButton;
    [SerializeField]
    private GameObject bgSound;

    private bool isKeyPressed = false;

    private void Start()
    {
        Time.timeScale = 1;
    }

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

        if (isKeyPressed)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Application.Quit();
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isKeyPressed = true;
            StartCoroutine(KeyPressedOnceRoutine());
        }
    }

    IEnumerator KeyPressedOnceRoutine()
    {
        yield return new WaitForSeconds(1f);
        isKeyPressed = false;
    }

    void PlayClipAtPoint()
    {
        int soundEffect = PlayerPrefs.GetInt("SoundON");
        if (soundEffect == 1)
        {
            AudioSource.PlayClipAtPoint(clickButton, new Vector3(0, 0, 0));
        }
    }

    public void PlayGame()
    {
        PlayClipAtPoint();
        int num = Random.Range(2, 8);
        SceneManager.LoadScene(num);
    }

    public void HowToPlay()
    {
        PlayClipAtPoint();
        SceneManager.LoadScene(8);
    }

    public void GetHealth()
    {
        PlayClipAtPoint();
        SceneManager.LoadScene(9);
    }

    public void Settings()
    {
        PlayClipAtPoint();
        SceneManager.LoadScene(10);
    }
}
