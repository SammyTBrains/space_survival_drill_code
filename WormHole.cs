using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WormHole : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            int CurScene = SceneManager.GetActiveScene().buildIndex;
            int RandNo = Random.Range(2, 8);
            if (RandNo == CurScene && RandNo < 7)
            {
                RandNo++;
            }
            else if (RandNo == CurScene && RandNo == 7)
            {
                RandNo = Random.Range(2, 7);
            }
            SceneManager.LoadScene(RandNo);
        }
    }
}
