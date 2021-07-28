using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonScriptSettingsScreen : MonoBehaviour
{
    [SerializeField]
    private GameObject areYouSure;
    [SerializeField]
    private AudioClip clickButton;

    void PlayClipAtPoint()
    {
        int soundEffect = PlayerPrefs.GetInt("SoundON");
        if (soundEffect == 1)
        {
            AudioSource.PlayClipAtPoint(clickButton, new Vector3(0, 0, 0));
        }
    }

    public void ClearScore()
    {
        PlayClipAtPoint();
        areYouSure.SetActive(true);  
    }

    public void Yes()
    {
        PlayClipAtPoint();
        PlayerPrefs.DeleteKey("HighScore");
        areYouSure.SetActive(false);
    }

    public void NO()
    {
        PlayClipAtPoint();
        areYouSure.SetActive(false);
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
