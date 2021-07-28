using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Advertisements;

public class PopUpScreenManager : MonoBehaviour, IUnityAdsListener
{
    [SerializeField]
    private GameObject watchAdorHealth;
    [SerializeField]
    private GameObject useHealth;
    [SerializeField]
    private Text healthText;
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private GameObject spawnManager;
    [SerializeField]
    private GameObject healthScreen;
    [SerializeField]
    private GameObject gameOverScreen;
    [SerializeField]
    private Text highScore;
    [SerializeField]
    private Text score;
    [SerializeField]
    private GameObject pauseMenu, pauseButton;
    [SerializeField]
    private AudioClip clickButton;
    [SerializeField]
    private GameObject bgSoundManager;
    [SerializeField]
    private Text WatchAdsOrUseHealthText, UseHealthText, identifierText;
    [SerializeField]
    private GameObject adsNotReady, adsError;

    private bool watchAdsClicked = false;

    private SpawnManager spawnManagerComponent;
    private Player thePlayer;
    [SerializeField]
    private Text healthNumToText;
    [SerializeField]
    private Text healthNumToText2;
    [SerializeField]
    private Text healthNumToText3;
    private int numToSpend;
    private int healthNo;
    private AudioSource playerAudio;
    private bool isStart = false;

    string GameId = "3949941";
    string GetHealthID = "GetHealth";
    string GameOverID = "GameOver";
    bool TestMode = true;

    private void Update()
    {
        if (PlayerPrefs.HasKey("healthNum"))
        {
            int healthNo = PlayerPrefs.GetInt("healthNum");
            healthNumToText.text = healthNo.ToString();
            healthNumToText2.text = healthNo.ToString();
            healthNumToText3.text = healthNo.ToString();
        }
        else
        {
            PlayerPrefs.SetInt("healthNum", 10);
            healthNumToText.text = "10";
            healthNumToText2.text = "10";
            healthNumToText3.text = "10";
        }
    }

    private void Start()
    {
        Advertisement.AddListener(this);
        Advertisement.Initialize(GameId, TestMode);

        if (PlayerPrefs.HasKey("healthUsed"))
        {
            healthText.text = PlayerPrefs.GetInt("healthUsed").ToString();
        }

        thePlayer = player.GetComponent<Player>();
        if (thePlayer == null)
        {
            Debug.LogError("the player is null!");
        }
        spawnManagerComponent = spawnManager.GetComponent<SpawnManager>();
        if (spawnManagerComponent == null)
        {
            Debug.LogError("Spawn Manager Component is null!");
        }
        playerAudio = bgSoundManager.GetComponent<AudioSource>();
        if (playerAudio == null)
        {
            Debug.LogError("Player Audio is null!");
        }
        spawnManagerComponent.AddAsteroidsToList();
        StartCoroutine(spawnManagerComponent.AsteroidRoutine());
        StartCoroutine(laserStartRoutine());
        StartCoroutine(spawnManagerComponent.SpawnDebrisRoutine());
    }

    IEnumerator laserStartRoutine()
    {
        while (true)
        {
            if(isStart == false)
            {
                int TotalCP = PlayerPrefs.GetInt("TotalCP");
                if (player.transform.position.z >= 101 || TotalCP > 1)
                {
                    isStart = true;
                    spawnManagerComponent.AddLasersToList();
                    StartCoroutine(spawnManagerComponent.LaserRoutine());
                }
            }
            yield return null;
        }
    }

    public void DisplayToContinue()
    {
        StartCoroutine(DisplayToContinueRoutine());
        if (PlayerPrefs.HasKey("DestroyedBefore"))
        {
            useHealth.SetActive(true);
            GameOverText();
            pauseButton.SetActive(false);
            if (bgSoundManager.activeSelf == true)
            {
                playerAudio.Pause();
            }
        }
        else
        {
            watchAdorHealth.SetActive(true);
            WatchAdsOrUseHealthText.text = "You couldn't survive that? Come on";
            pauseButton.SetActive(false);
            PlayerPrefs.SetInt("DestroyedBefore", 1);
            if (bgSoundManager.activeSelf == true)
            {
                playerAudio.Pause();
            }
        }
    }


    void GameOverText()
    {
        string[] Texts = {"Wahala be like bicycle, see as you just destroy expensive spaceship", "Na wa ooo, sha try again",
                          "Oops", "Alright try again", "You just blew up an expensive ship", "Try again", "Who is going to pay for the ship?",
                           "Vous venez de faire sauter un vaisseau spatial coûteux", "réessayer"};

        if (PlayerPrefs.HasKey("GameOverText"))
        {
            string CurText = PlayerPrefs.GetString("GameOverText");

            int RandNo = Random.Range(0, 9);
            if (Texts[RandNo] == CurText && RandNo < 8)
            {
                RandNo++;
            }
            else if (Texts[RandNo] == CurText && RandNo == 8)
            {
                RandNo = Random.Range(0, 8);
            }
            UseHealthText.text = Texts[RandNo];
            PlayerPrefs.SetString("GameOverText", UseHealthText.text);

            if (RandNo == 0 || RandNo == 1)
            {
                identifierText.text = "(Nigerian Pidgin)";
            }
            else if (RandNo == 7 || RandNo == 8)
            {
                identifierText.text = "(French)";
            }
            else
            {
                identifierText.text = "";
            }
        }
        else
        {
            UseHealthText.text = "Alright try again";
            identifierText.text = "";
            PlayerPrefs.SetString("GameOverText", UseHealthText.text);
        }


    }

    IEnumerator DisplayToContinueRoutine()
    {
        yield return new WaitForSeconds(1.5f);
        Time.timeScale = 0;
    }

    void AfterTimeScaleOne()
    {
        spawnManager.transform.SetParent(player.transform);
        thePlayer.health = 100;
        thePlayer.CameraParentSet();
        player.SetActive(true);
        thePlayer._damage.SetActive(false);
    }

    void PlayClipAtPoint()
    {
        int soundEffect = PlayerPrefs.GetInt("SoundON");
        if (soundEffect == 1)
        {
            Transform cameraMain = Camera.main.transform;
            Vector3 pos = new Vector3(cameraMain.position.x, cameraMain.position.y, cameraMain.position.z + 0.5f);
            AudioSource.PlayClipAtPoint(clickButton, pos);
        }
    }

    public void watchAds()
    {
        PlayClipAtPoint();
        watchAdsClicked = true;
        if (Advertisement.IsReady(GetHealthID))
        {
            Advertisement.Show(GetHealthID);
        }
        else
        {
            adsNotReady.SetActive(true);
        }
    }

    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        if (placementId == GetHealthID)
        {
            if (showResult == ShowResult.Finished)
            {
                watchAdsClicked = false;
                watchAdorHealth.SetActive(false);
                pauseButton.SetActive(true);
                Time.timeScale = 1;
                if (player.GetComponent<Player>().isMissileReady == false)
                {
                    StartCoroutine(player.GetComponent<Player>().MissileRoutine());
                }
                if (bgSoundManager.activeSelf == true)
                {
                    playerAudio.Play();
                }
                AfterTimeScaleOne();
            }
            else if (showResult == ShowResult.Failed)
            {
                Debug.LogWarning("The ad did not finish due to an error.");
            }
        }
    }

    public void OnUnityAdsReady(string placementId)
    {
        // If the ready Placement is rewarded, show the ad:
        if (placementId == GetHealthID)
        {
            // Optional actions to take when the placement becomes ready(For example, enable the rewarded ads button)
        }
    }

    public void OnUnityAdsDidError(string message)
    {
        if (watchAdsClicked == true)
        {
            adsError.SetActive(true);
        }
    }

    public void OnUnityAdsDidStart(string placementId)
    {
        // Optional actions to take when the end-users triggers an ad.
    }

    public void OkButtonError()
    {
        adsError.SetActive(false);
    }

    public void OkButtonAdsNotReady()
    {
        adsNotReady.SetActive(false);
    }

    // When the object that subscribes to ad events is destroyed, remove the listener:
    public void OnDestroy()
    {
        Advertisement.RemoveListener(this);
    }

    public void UseHealthClick()
    {
        PlayClipAtPoint();
        if (PlayerPrefs.HasKey("healthUsed"))
        {
            numToSpend = PlayerPrefs.GetInt("healthUsed");
        }
        else
        {
            numToSpend = 1;
            PlayerPrefs.SetInt("healthUsed", 1);
        }
        if (PlayerPrefs.HasKey("healthNum"))
        {
            healthNo = PlayerPrefs.GetInt("healthNum");
            if (healthNo < numToSpend)
            {
                healthScreen.SetActive(true);
            }
            else
            {
                healthNo = healthNo - numToSpend;
                PlayerPrefs.SetInt("healthNum", healthNo);
                if (PlayerPrefs.HasKey("healthUsed"))
                {
                    numToSpend = numToSpend * 2;
                    PlayerPrefs.SetInt("healthUsed", numToSpend);
                    healthText.text = numToSpend.ToString();
                }
                checkToContinueScreenActive();
                pauseButton.SetActive(true);
                Time.timeScale = 1;
                if (player.GetComponent<Player>().isMissileReady == false)
                {
                    StartCoroutine(player.GetComponent<Player>().MissileRoutine());
                }
                if (bgSoundManager.activeSelf == true)
                {
                    playerAudio.Play();
                }
                AfterTimeScaleOne();
            }
        }
    }

    void checkToContinueScreenActive()
    {
        if (watchAdorHealth.activeSelf == true)
        {
            watchAdorHealth.SetActive(false);
        }
        else if (useHealth.activeSelf == true)
        {
            useHealth.SetActive(false);
        }
    }

    public void Continue()
    {
        PlayClipAtPoint();
        PlayerPrefs.DeleteKey("DestroyedBefore");
        PlayerPrefs.DeleteKey("healthUsed");
        highScore.text = PlayerPrefs.GetInt("HighScore").ToString();
        score.text = PlayerPrefs.GetInt("TotalCP").ToString();
        PlayerPrefs.DeleteKey("TotalCP");
        if (Advertisement.IsReady(GameOverID))
        {
            Advertisement.Show(GameOverID);
        }
        gameOverScreen.SetActive(true);
        checkToContinueScreenActive();
    }

    public void ContinueToHome()
    {
        PlayClipAtPoint();
        SceneManager.LoadScene(1);
    }

    public void PauseGame()
    {
        PlayClipAtPoint();
        Time.timeScale = 0;
        pauseMenu.SetActive(true);
        pauseButton.SetActive(false);
        if (bgSoundManager.activeSelf == true)
        {
            playerAudio.Pause();
        }
        Camera.main.transform.SetParent(null);
        player.SetActive(false);
    }

    public void ContinueGame()
    {
        PlayClipAtPoint();
        Time.timeScale = 1;
        if (player.GetComponent<Player>().isMissileReady == false)
        {
            StartCoroutine(player.GetComponent<Player>().MissileRoutine());
        }
        pauseMenu.SetActive(false);
        pauseButton.SetActive(true);
        player.SetActive(true);
        if (bgSoundManager.activeSelf == true)
        {
            playerAudio.Play();
        }
        Transform playerTransform = player.transform;
        Camera.main.transform.SetParent(playerTransform);
        thePlayer._damage.SetActive(false);
    }

    public void QuitGame()
    {
        PlayClipAtPoint();
        Time.timeScale = 1;
        SceneManager.LoadScene(1);
        PlayerPrefs.DeleteKey("DestroyedBefore");
        PlayerPrefs.DeleteKey("healthUsed");
        PlayerPrefs.DeleteKey("TotalCP");
    }

    public void GetHealthPUScreenBackButton()
    {
        PlayClipAtPoint();
        healthScreen.SetActive(false);
    }
}
