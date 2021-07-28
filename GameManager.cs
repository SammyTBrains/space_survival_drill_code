using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private bool isKeyPressed = false;

    private void Start()
    {
        if (!PlayerPrefs.HasKey("SoundON"))
        {
            PlayerPrefs.SetInt("SoundON", 1);
        }

        if (!PlayerPrefs.HasKey("MusicON"))
        {
            PlayerPrefs.SetInt("MusicON", 1);
        }
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(StartSceneRoutine());
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


        IEnumerator KeyPressedOnceRoutine()
        {
            yield return new WaitForSeconds(1f);
            isKeyPressed = false;
        }

    }

    IEnumerator StartSceneRoutine()
    {
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene(1);
    }
}
