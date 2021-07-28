using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField]
    private GameObject enemyMiniMissile;

    void Start()
    {
        StartCoroutine(MissileRoutine());
    }

    IEnumerator MissileRoutine()
    {
        while (true)
        {
            GameObject missile = Instantiate(enemyMiniMissile, transform.position, Quaternion.Euler(0, 90, 0));
            int TotalCP = PlayerPrefs.GetInt("TotalCP");
            if (TotalCP < 10)
            {
                yield return new WaitForSeconds(2f);
                Destroy(missile, 2.5f);
            }
            else if (TotalCP >= 10 && TotalCP <= 20)
            {
                yield return new WaitForSeconds(1.5f);
                Destroy(missile, 1.8f);
            }
            else if (TotalCP >= 20)
            {
                yield return new WaitForSeconds(1.3f);
                Destroy(missile, 1f);
            }
        }
    }
}
