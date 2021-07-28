using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    private SpawnManager spawnManager;
    int totalCP;

    private void Start()
    {
        spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        if(spawnManager == null)
        {
            Debug.LogError("Spawn Manager is null");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            spawnManager.SpawnCheckPoint();
            spawnManager.checkPointNum++;
            if (PlayerPrefs.HasKey("TotalCP"))
            {
                totalCP = PlayerPrefs.GetInt("TotalCP");
            }
            totalCP++;
            PlayerPrefs.SetInt("TotalCP", totalCP);
            spawnManager.totalCPText.text = totalCP.ToString();
            Destroy(this.gameObject, 0.5f);
        }
    }
}
