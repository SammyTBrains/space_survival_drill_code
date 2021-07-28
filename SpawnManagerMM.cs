using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManagerMM : MonoBehaviour
{
    [SerializeField]
    private GameObject[] asteroidsMM;

    void Start()
    {
        StartCoroutine(SpawningAsteroidRoutine());
    }

    IEnumerator SpawningAsteroidRoutine()
    {
        while (true)
        {
            int num = Random.Range(0, 3);
            int num2 = Random.Range(0, 3);
            Vector3 pos1 = new Vector3(Random.Range(-59, -63), Random.Range(-50, 50), Random.Range(43, 60));
            Vector3 pos2 = new Vector3(Random.Range(59, 63), Random.Range(-50, 50), Random.Range(43, 60));
            Vector3 pos3 = new Vector3(Random.Range(-59, -63), Random.Range(-50, 50), Random.Range(43, 60));
            Vector3 pos4 = new Vector3(Random.Range(59, 63), Random.Range(-50, 50), Random.Range(43, 60));
            Instantiate(asteroidsMM[num], pos1, Quaternion.identity);
            Instantiate(asteroidsMM[num2], pos2, Quaternion.identity);
            Instantiate(asteroidsMM[num], pos3, Quaternion.identity);
            Instantiate(asteroidsMM[num2], pos4, Quaternion.identity);
            yield return new WaitForSeconds(6f);
        }
    }
}
