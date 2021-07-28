using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private int _checkPointMax;
    [SerializeField]
    public float checkPointNum = 1;
    [SerializeField]
    private GameObject[] _blasters;
    [SerializeField]
    private GameObject _checkpoint;
    [SerializeField]
    private GameObject[] _asteroids;
    [SerializeField]
    private float asteroidZ = 20f;
    [SerializeField]
    private Text checkpointValue;
    [SerializeField]
    public Text totalCPText;
    [SerializeField]
    private GameObject wormHole;
    [SerializeField]
    private GameObject debris;
    [SerializeField]
    private GameObject TheLaser;
    [SerializeField]
    private float laserDist;

    private List<SpawnAsteroid> asteroids;
    private List<SpawnLaser> lasers;

    private float blasterDestroyTime = 6.5f;

    private void Start()
    {
        _checkPointMax = Random.Range(15, 19);
        checkpointValue.text = "0/" + (_checkPointMax + 2);
        if (PlayerPrefs.HasKey("TotalCP"))
        {
            totalCPText.text = PlayerPrefs.GetInt("TotalCP").ToString();
        }
        else
        {
            totalCPText.text = "0";
        }
    }

    private void Update()
    {
        Mathf.Round(checkPointNum);
        checkpointValue.text = checkPointNum + "/" + (_checkPointMax + 2);
        int TotalCP = PlayerPrefs.GetInt("TotalCP");
        if (TotalCP >= 10 && TotalCP <= 20)
        {
            blasterDestroyTime = 5.0f;
        }
        else if (TotalCP >= 20 && TotalCP <= 30)
        {
            blasterDestroyTime = 3.5f;
        }
        else if (TotalCP >= 30)
        {
            blasterDestroyTime = 2.4f;
        }
    }

    public IEnumerator SpawnDebrisRoutine()
    {
        while (true)
        {
            Vector3 pos = new Vector3(transform.position.x + 30.7f, transform.position.y, transform.position.z + 150);
            Vector3 pos2 = new Vector3(transform.position.x - 30.7f, transform.position.y, transform.position.z + 150);
            GameObject debris1 = Instantiate(debris, pos, Quaternion.identity);
            GameObject debris2 = Instantiate(debris, pos2, Quaternion.identity);
            yield return new WaitForSeconds(8f);
            Destroy(debris1.gameObject);
            Destroy(debris2.gameObject);
        }
    }

    public void SpawnCheckPoint()
    {
        if (checkPointNum <= _checkPointMax)
        {
            CheckPointInstantiate();
        }
        else
        {
            Vector3 pos = new Vector3(transform.position.x, transform.position.y, transform.position.z + 200);
            Instantiate(wormHole, pos, Quaternion.identity);
        }
    }


    delegate void SpawnAsteroid();
    delegate void SpawnLaser();


    public IEnumerator LaserRoutine()
    {
        while (true)
        {
            int num = Random.Range(0, 3);
            lasers[num]();
            int TotalCP = PlayerPrefs.GetInt("TotalCP");
            if (TotalCP < 10)
            {
                yield return new WaitForSeconds(Random.Range(1.0f, 1.5f));
            }
            else if (TotalCP >= 10 && TotalCP <= 20)
            {
                yield return new WaitForSeconds(1.0f);
            }
            else if (TotalCP >= 20)
            {
                yield return new WaitForSeconds(Random.Range(0.8f, 1.0f));
            }
        }
    }

    public void AddLasersToList()
    {
        lasers = new List<SpawnLaser>();
        lasers.Add(LaserRoutineOne);
        lasers.Add(LaserdRoutineTwo);
        lasers.Add(LaserRoutineThree);
    }

    void LaserRoutineOne()
    {
        Vector3 pos = new Vector3(0, 0, transform.position.z + laserDist + Random.Range(10, 20));
        Vector3 pos2 = new Vector3(0, 0, transform.position.z + laserDist + Random.Range(10, 20));
        Vector3 pos3 = new Vector3(0, 0, transform.position.z + laserDist + Random.Range(10, 20));
        GameObject laser = Instantiate(TheLaser, pos, Quaternion.Euler(0, 0, Random.Range(-77, 77)));
        GameObject laser2 = Instantiate(TheLaser, pos2, Quaternion.Euler(0, 0, Random.Range(-77, 77)));
        GameObject laser3 = Instantiate(TheLaser, pos3, Quaternion.Euler(0, 0, Random.Range(-77, 77)));
        int TotalCP = PlayerPrefs.GetInt("TotalCP");
        if (TotalCP < 10)
        {
            Destroy(laser.gameObject, 3f);
            Destroy(laser2.gameObject, 3f);
            Destroy(laser3.gameObject, 3f);
        }
        else if (TotalCP >= 10 && TotalCP <= 20)
        {
            Destroy(laser.gameObject, 2f);
            Destroy(laser2.gameObject, 2f);
            Destroy(laser3.gameObject, 2f);
        }
        else if (TotalCP > 20)
        {
            Destroy(laser.gameObject, 1f);
            Destroy(laser2.gameObject, 1f);
            Destroy(laser3.gameObject, 1f);
        }
    }

    void LaserdRoutineTwo()
    {
        Vector3 pos = new Vector3(0, 0, transform.position.z + laserDist + Random.Range(10, 20));
        Vector3 pos2 = new Vector3(0, 0, transform.position.z + laserDist + Random.Range(10, 20));
        Vector3 pos3 = new Vector3(0, 0, transform.position.z + laserDist + Random.Range(10, 20));
        Vector3 pos4 = new Vector3(0, 0, transform.position.z + laserDist + Random.Range(10, 20));
        Vector3 pos5 = new Vector3(0, 0, transform.position.z + laserDist + Random.Range(10, 20));
        GameObject laser = Instantiate(TheLaser, pos, Quaternion.Euler(0, 0, Random.Range(-77, 77)));
        GameObject laser2 = Instantiate(TheLaser, pos2, Quaternion.Euler(0, 0, Random.Range(-77, 77)));
        GameObject laser3 = Instantiate(TheLaser, pos3, Quaternion.Euler(0, 0, Random.Range(-77, 77)));
        GameObject laser4 = Instantiate(TheLaser, pos4, Quaternion.Euler(0, 0, Random.Range(-77, 77)));
        GameObject laser5 = Instantiate(TheLaser, pos5, Quaternion.Euler(0, 0, Random.Range(-77, 77)));
        int TotalCP = PlayerPrefs.GetInt("TotalCP");
        if (TotalCP < 10)
        {
            Destroy(laser.gameObject, 3f);
            Destroy(laser2.gameObject, 3f);
            Destroy(laser3.gameObject, 3f);
            Destroy(laser4.gameObject, 3f);
            Destroy(laser5.gameObject, 3f);
        }
        else if (TotalCP >= 10 && TotalCP <= 20)
        {
            Destroy(laser.gameObject, 2f);
            Destroy(laser2.gameObject, 2f);
            Destroy(laser3.gameObject, 2f);
            Destroy(laser4.gameObject, 2f);
            Destroy(laser5.gameObject, 2f);
        }
        else if (TotalCP > 20)
        {
            Destroy(laser.gameObject, 1f);
            Destroy(laser2.gameObject, 1f);
            Destroy(laser3.gameObject, 1f);
            Destroy(laser4.gameObject, 1f);
            Destroy(laser5.gameObject, 1f);
        }
    }

    void LaserRoutineThree()
    {
        Vector3 pos = new Vector3(0, 0, transform.position.z + laserDist + Random.Range(10, 20));
        Vector3 pos2 = new Vector3(0, 0, transform.position.z + laserDist + Random.Range(10, 20));
        Vector3 pos3 = new Vector3(0, 0, transform.position.z + laserDist + Random.Range(10, 20));
        Vector3 pos4 = new Vector3(0, 0, transform.position.z + laserDist + Random.Range(10, 20));
        Vector3 pos5 = new Vector3(0, 0, transform.position.z + laserDist + Random.Range(10, 20));
        Vector3 pos6 = new Vector3(0, 0, transform.position.z + laserDist + Random.Range(10, 20));
        Vector3 pos7 = new Vector3(0, 0, transform.position.z + laserDist + Random.Range(10, 20));
        GameObject laser = Instantiate(TheLaser, pos, Quaternion.Euler(0, 0, Random.Range(-77, 77)));
        GameObject laser2 = Instantiate(TheLaser, pos2, Quaternion.Euler(0, 0, Random.Range(-77, 77)));
        GameObject laser3 = Instantiate(TheLaser, pos3, Quaternion.Euler(0, 0, Random.Range(-77, 77)));
        GameObject laser4 = Instantiate(TheLaser, pos4, Quaternion.Euler(0, 0, Random.Range(-77, 77)));
        GameObject laser5 = Instantiate(TheLaser, pos5, Quaternion.Euler(0, 0, Random.Range(-77, 77)));
        GameObject laser6 = Instantiate(TheLaser, pos6, Quaternion.Euler(0, 0, Random.Range(-77, 77)));
        GameObject laser7 = Instantiate(TheLaser, pos7, Quaternion.Euler(0, 0, Random.Range(-77, 77)));
        int TotalCP = PlayerPrefs.GetInt("TotalCP");
        if (TotalCP < 10)
        {
            Destroy(laser.gameObject, 3f);
            Destroy(laser2.gameObject, 3f);
            Destroy(laser3.gameObject, 3f);
            Destroy(laser4.gameObject, 3f);
            Destroy(laser5.gameObject, 3f);
            Destroy(laser6.gameObject, 3f);
            Destroy(laser7.gameObject, 3f);
        }
        else if (TotalCP >= 10 && TotalCP <= 20)
        {
            Destroy(laser.gameObject, 2f);
            Destroy(laser2.gameObject, 2f);
            Destroy(laser3.gameObject, 2f);
            Destroy(laser4.gameObject, 2f);
            Destroy(laser5.gameObject, 2f);
            Destroy(laser6.gameObject, 2f);
            Destroy(laser7.gameObject, 2f);
        }
        else if (TotalCP > 20)
        {
            Destroy(laser.gameObject, 1f);
            Destroy(laser2.gameObject, 1f);
            Destroy(laser3.gameObject, 1f);
            Destroy(laser4.gameObject, 1f);
            Destroy(laser5.gameObject, 1f);
            Destroy(laser6.gameObject, 1f);
            Destroy(laser7.gameObject, 1f);
        }
    }

    public IEnumerator AsteroidRoutine()
    {
        while (true)
        {
            int num = Random.Range(0, 3);
            asteroids[num]();
            int TotalCP = PlayerPrefs.GetInt("TotalCP");
            if (TotalCP < 10)
            {
                yield return new WaitForSeconds(Random.Range(1.0f, 1.5f));
            }
            else if (TotalCP >= 10 && TotalCP <= 20)
            {
                yield return new WaitForSeconds(Random.Range(0.7f, 1.0f));
            }
            else if (TotalCP >= 20 && TotalCP <= 30)
            {
                yield return new WaitForSeconds(Random.Range(0.3f, 0.7f));
            }
            else if (TotalCP >= 30)
            {
                yield return new WaitForSeconds(0.3f);
            }
        }
    }

    public void AddAsteroidsToList()
    {
        asteroids = new List<SpawnAsteroid>();
        asteroids.Add(AsteroidRoutineOne);
        asteroids.Add(AsteroidRoutineTwo);
        asteroids.Add(AsteroidRoutineThree);
    }

    void AsteroidRoutineOne()
    {
        Vector3 pos = new Vector3(Random.Range(-22.5f, 22.5f), Random.Range(-6.3f, 6.3f), transform.position.z + asteroidZ);
        int num = Random.Range(0, 3);
        GameObject ast = Instantiate(_asteroids[num], pos, Quaternion.identity);
        Destroy(ast.gameObject, 5f);
    }

    void AsteroidRoutineTwo()
    {
        Vector3 pos = new Vector3(Random.Range(-22.5f, 22.5f), Random.Range(-6.3f, 6.3f), transform.position.z + asteroidZ);
        Vector3 pos2 = new Vector3(Random.Range(-22.5f, 22.5f), Random.Range(-6.3f, 6.3f), transform.position.z + asteroidZ);
        int num = Random.Range(0, 3);
        int num2 = Random.Range(0, 3);
        GameObject ast = Instantiate(_asteroids[num], pos, Quaternion.identity);
        GameObject ast2 = Instantiate(_asteroids[num2], pos2, Quaternion.identity);
        Destroy(ast.gameObject, 5f);
        Destroy(ast2.gameObject, 5f);
    }

    void AsteroidRoutineThree()
    {
        Vector3 pos = new Vector3(Random.Range(-22.5f, 22.5f), Random.Range(-6.3f, 6.3f), transform.position.z + asteroidZ);
        Vector3 pos2 = new Vector3(Random.Range(-22.5f, 22.5f), Random.Range(-6.3f, 6.3f), transform.position.z + asteroidZ);
        Vector3 pos3 = new Vector3(Random.Range(-22.5f, 22.5f), Random.Range(-6.3f, 6.3f), transform.position.z + asteroidZ);
        int num = Random.Range(0, 3);
        int num2 = Random.Range(0, 3);
        int num3 = Random.Range(0, 3);
        GameObject ast = Instantiate(_asteroids[num], pos, Quaternion.identity);
        GameObject ast2 = Instantiate(_asteroids[num2], pos2, Quaternion.identity);
        GameObject ast3 = Instantiate(_asteroids[num3], pos3, Quaternion.identity);
        Destroy(ast.gameObject, 5f);
        Destroy(ast2.gameObject, 5f);
        Destroy(ast3.gameObject, 5f);
    }

    public void CheckPointInstantiate()
    {
        int no1 = Random.Range(0, 8);
        int no2 = Random.Range(0, 8);
        int no3 = Random.Range(0, 8);
        int no4 = Random.Range(0, 8);
        int no5 = Random.Range(0, 8);

        Vector3 startPosition = new Vector3(0, 0, transform.position.z + 300);
        Instantiate(_checkpoint, startPosition, Quaternion.identity);
        GameObject blaster1 = Instantiate(_blasters[no1], new Vector3(_blasters[no1].transform.position.x, _blasters[no1].transform.position.y, transform.position.z + 301), _blasters[no1].transform.rotation);
        GameObject blaster2 = Instantiate(_blasters[no2], new Vector3(_blasters[no2].transform.position.x, _blasters[no2].transform.position.y, transform.position.z + 301), _blasters[no2].transform.rotation);
        GameObject blaster3 = Instantiate(_blasters[no3], new Vector3(_blasters[no3].transform.position.x, _blasters[no3].transform.position.y, transform.position.z + 301), _blasters[no3].transform.rotation);
        GameObject blaster4 = Instantiate(_blasters[no4], new Vector3(_blasters[no4].transform.position.x, _blasters[no4].transform.position.y, transform.position.z + 301), _blasters[no4].transform.rotation);
        GameObject blaster5 = Instantiate(_blasters[no5], new Vector3(_blasters[no5].transform.position.x, _blasters[no5].transform.position.y, transform.position.z + 301), _blasters[no5].transform.rotation);

        Destroy(blaster1.gameObject, blasterDestroyTime);
        Destroy(blaster2.gameObject, blasterDestroyTime);
        Destroy(blaster3.gameObject, blasterDestroyTime);
        Destroy(blaster4.gameObject, blasterDestroyTime);
        Destroy(blaster5.gameObject, blasterDestroyTime);

        GameObject IIblaster1 = Instantiate(_blasters[no1], new Vector3(_blasters[no1].transform.position.x + 13, _blasters[no1].transform.position.y, transform.position.z + 301), _blasters[no1].transform.rotation);
        GameObject IIblaster2 = Instantiate(_blasters[no2], new Vector3(_blasters[no2].transform.position.x + 13, _blasters[no2].transform.position.y, transform.position.z + 301), _blasters[no2].transform.rotation);
        GameObject IIblaster3 = Instantiate(_blasters[no3], new Vector3(_blasters[no3].transform.position.x + 13, _blasters[no3].transform.position.y, transform.position.z + 301), _blasters[no3].transform.rotation);
        GameObject IIblaster4 = Instantiate(_blasters[no4], new Vector3(_blasters[no4].transform.position.x + 13, _blasters[no4].transform.position.y, transform.position.z + 301), _blasters[no4].transform.rotation);
        GameObject IIblaster5 = Instantiate(_blasters[no5], new Vector3(_blasters[no5].transform.position.x + 13, _blasters[no5].transform.position.y, transform.position.z + 301), _blasters[no5].transform.rotation);

        Destroy(IIblaster1.gameObject, blasterDestroyTime);
        Destroy(IIblaster2.gameObject, blasterDestroyTime);
        Destroy(IIblaster3.gameObject, blasterDestroyTime);
        Destroy(IIblaster4.gameObject, blasterDestroyTime);
        Destroy(IIblaster5.gameObject, blasterDestroyTime);

        GameObject IIIblaster1 = Instantiate(_blasters[no1], new Vector3(_blasters[no1].transform.position.x - 13, _blasters[no1].transform.position.y, transform.position.z + 301), _blasters[no1].transform.rotation);
        GameObject IIIblaster2 = Instantiate(_blasters[no2], new Vector3(_blasters[no2].transform.position.x - 13, _blasters[no2].transform.position.y, transform.position.z + 301), _blasters[no2].transform.rotation);
        GameObject IIIblaster3 = Instantiate(_blasters[no3], new Vector3(_blasters[no3].transform.position.x - 13, _blasters[no3].transform.position.y, transform.position.z + 301), _blasters[no3].transform.rotation);
        GameObject IIIblaster4 = Instantiate(_blasters[no4], new Vector3(_blasters[no4].transform.position.x - 13, _blasters[no4].transform.position.y, transform.position.z + 301), _blasters[no4].transform.rotation);
        GameObject IIIblaster5 = Instantiate(_blasters[no5], new Vector3(_blasters[no5].transform.position.x - 13, _blasters[no5].transform.position.y, transform.position.z + 301), _blasters[no5].transform.rotation);

        Destroy(IIIblaster1.gameObject, blasterDestroyTime);
        Destroy(IIIblaster2.gameObject, blasterDestroyTime);
        Destroy(IIIblaster3.gameObject, blasterDestroyTime);
        Destroy(IIIblaster4.gameObject, blasterDestroyTime);
        Destroy(IIIblaster5.gameObject, blasterDestroyTime);

        GameObject IVblaster1 = Instantiate(_blasters[no1], new Vector3(_blasters[no1].transform.position.x, _blasters[no1].transform.position.y + 13, transform.position.z + 301), _blasters[no1].transform.rotation);
        GameObject IVblaster2 = Instantiate(_blasters[no2], new Vector3(_blasters[no2].transform.position.x, _blasters[no2].transform.position.y + 13, transform.position.z + 301), _blasters[no2].transform.rotation);
        GameObject IVblaster3 = Instantiate(_blasters[no3], new Vector3(_blasters[no3].transform.position.x, _blasters[no3].transform.position.y + 13, transform.position.z + 301), _blasters[no3].transform.rotation);
        GameObject IVblaster4 = Instantiate(_blasters[no4], new Vector3(_blasters[no4].transform.position.x, _blasters[no4].transform.position.y + 13, transform.position.z + 301), _blasters[no4].transform.rotation);
        GameObject IVblaster5 = Instantiate(_blasters[no5], new Vector3(_blasters[no5].transform.position.x, _blasters[no5].transform.position.y + 13, transform.position.z + 301), _blasters[no5].transform.rotation);

        Destroy(IVblaster1.gameObject, blasterDestroyTime);
        Destroy(IVblaster2.gameObject, blasterDestroyTime);
        Destroy(IVblaster3.gameObject, blasterDestroyTime);
        Destroy(IVblaster4.gameObject, blasterDestroyTime);
        Destroy(IVblaster5.gameObject, blasterDestroyTime);

        GameObject Vblaster1 = Instantiate(_blasters[no1], new Vector3(_blasters[no1].transform.position.x, _blasters[no1].transform.position.y - 13, transform.position.z + 301), _blasters[no1].transform.rotation);
        GameObject Vblaster2 = Instantiate(_blasters[no2], new Vector3(_blasters[no2].transform.position.x, _blasters[no2].transform.position.y - 13, transform.position.z + 301), _blasters[no2].transform.rotation);
        GameObject Vblaster3 = Instantiate(_blasters[no3], new Vector3(_blasters[no3].transform.position.x, _blasters[no3].transform.position.y - 13, transform.position.z + 301), _blasters[no3].transform.rotation);
        GameObject Vblaster4 = Instantiate(_blasters[no4], new Vector3(_blasters[no4].transform.position.x, _blasters[no4].transform.position.y - 13, transform.position.z + 301), _blasters[no4].transform.rotation);
        GameObject Vblaster5 = Instantiate(_blasters[no5], new Vector3(_blasters[no5].transform.position.x, _blasters[no5].transform.position.y - 13, transform.position.z + 301), _blasters[no5].transform.rotation);

        Destroy(Vblaster1.gameObject, blasterDestroyTime);
        Destroy(Vblaster2.gameObject, blasterDestroyTime);
        Destroy(Vblaster3.gameObject, blasterDestroyTime);
        Destroy(Vblaster4.gameObject, blasterDestroyTime);
        Destroy(Vblaster5.gameObject, blasterDestroyTime);

        GameObject VIblaster1 = Instantiate(_blasters[no1], new Vector3(_blasters[no1].transform.position.x + 13, _blasters[no1].transform.position.y + 13, transform.position.z + 301), _blasters[no1].transform.rotation);
        GameObject VIblaster2 = Instantiate(_blasters[no2], new Vector3(_blasters[no2].transform.position.x + 13, _blasters[no2].transform.position.y + 13, transform.position.z + 301), _blasters[no2].transform.rotation);
        GameObject VIblaster3 = Instantiate(_blasters[no3], new Vector3(_blasters[no3].transform.position.x + 13, _blasters[no3].transform.position.y + 13, transform.position.z + 301), _blasters[no3].transform.rotation);
        GameObject VIblaster4 = Instantiate(_blasters[no4], new Vector3(_blasters[no4].transform.position.x + 13, _blasters[no4].transform.position.y + 13, transform.position.z + 301), _blasters[no4].transform.rotation);
        GameObject VIblaster5 = Instantiate(_blasters[no5], new Vector3(_blasters[no5].transform.position.x + 13, _blasters[no5].transform.position.y + 13, transform.position.z + 301), _blasters[no5].transform.rotation);

        Destroy(VIblaster1.gameObject, blasterDestroyTime);
        Destroy(VIblaster2.gameObject, blasterDestroyTime);
        Destroy(VIblaster3.gameObject, blasterDestroyTime);
        Destroy(VIblaster4.gameObject, blasterDestroyTime);
        Destroy(VIblaster5.gameObject, blasterDestroyTime);

        GameObject VIIblaster1 = Instantiate(_blasters[no1], new Vector3(_blasters[no1].transform.position.x - 13, _blasters[no1].transform.position.y + 13, transform.position.z + 301), _blasters[no1].transform.rotation);
        GameObject VIIblaster2 = Instantiate(_blasters[no2], new Vector3(_blasters[no2].transform.position.x - 13, _blasters[no2].transform.position.y + 13, transform.position.z + 301), _blasters[no2].transform.rotation);
        GameObject VIIblaster3 = Instantiate(_blasters[no3], new Vector3(_blasters[no3].transform.position.x - 13, _blasters[no3].transform.position.y + 13, transform.position.z + 301), _blasters[no3].transform.rotation);
        GameObject VIIblaster4 = Instantiate(_blasters[no4], new Vector3(_blasters[no4].transform.position.x - 13, _blasters[no4].transform.position.y + 13, transform.position.z + 301), _blasters[no4].transform.rotation);
        GameObject VIIblaster5 = Instantiate(_blasters[no5], new Vector3(_blasters[no5].transform.position.x - 13, _blasters[no5].transform.position.y + 13, transform.position.z + 301), _blasters[no5].transform.rotation);

        Destroy(VIIblaster1.gameObject, blasterDestroyTime);
        Destroy(VIIblaster2.gameObject, blasterDestroyTime);
        Destroy(VIIblaster3.gameObject, blasterDestroyTime);
        Destroy(VIIblaster4.gameObject, blasterDestroyTime);
        Destroy(VIIblaster5.gameObject, blasterDestroyTime);

        GameObject VIIIblaster1 = Instantiate(_blasters[no1], new Vector3(_blasters[no1].transform.position.x + 13, _blasters[no1].transform.position.y - 13, transform.position.z + 301), _blasters[no1].transform.rotation);
        GameObject VIIIblaster2 = Instantiate(_blasters[no2], new Vector3(_blasters[no2].transform.position.x + 13, _blasters[no2].transform.position.y - 13, transform.position.z + 301), _blasters[no2].transform.rotation);
        GameObject VIIIblaster3 = Instantiate(_blasters[no3], new Vector3(_blasters[no3].transform.position.x + 13, _blasters[no3].transform.position.y - 13, transform.position.z + 301), _blasters[no3].transform.rotation);
        GameObject VIIIblaster4 = Instantiate(_blasters[no4], new Vector3(_blasters[no4].transform.position.x + 13, _blasters[no4].transform.position.y - 13, transform.position.z + 301), _blasters[no4].transform.rotation);
        GameObject VIIIblaster5 = Instantiate(_blasters[no5], new Vector3(_blasters[no5].transform.position.x + 13, _blasters[no5].transform.position.y - 13, transform.position.z + 301), _blasters[no5].transform.rotation);

        Destroy(VIIIblaster1.gameObject, blasterDestroyTime);
        Destroy(VIIIblaster2.gameObject, blasterDestroyTime);
        Destroy(VIIIblaster3.gameObject, blasterDestroyTime);
        Destroy(VIIIblaster4.gameObject, blasterDestroyTime);
        Destroy(VIIIblaster5.gameObject, blasterDestroyTime);

        GameObject IXblaster1 = Instantiate(_blasters[no1], new Vector3(_blasters[no1].transform.position.x - 13, _blasters[no1].transform.position.y - 13, transform.position.z + 301), _blasters[no1].transform.rotation);
        GameObject IXblaster2 = Instantiate(_blasters[no2], new Vector3(_blasters[no2].transform.position.x - 13, _blasters[no2].transform.position.y - 13, transform.position.z + 301), _blasters[no2].transform.rotation);
        GameObject IXblaster3 = Instantiate(_blasters[no3], new Vector3(_blasters[no3].transform.position.x - 13, _blasters[no3].transform.position.y - 13, transform.position.z + 301), _blasters[no3].transform.rotation);
        GameObject IXblaster4 = Instantiate(_blasters[no4], new Vector3(_blasters[no4].transform.position.x - 13, _blasters[no4].transform.position.y - 13, transform.position.z + 301), _blasters[no4].transform.rotation);
        GameObject IXblaster5 = Instantiate(_blasters[no5], new Vector3(_blasters[no5].transform.position.x - 13, _blasters[no5].transform.position.y - 13, transform.position.z + 301), _blasters[no5].transform.rotation);

        Destroy(IXblaster1.gameObject, blasterDestroyTime);
        Destroy(IXblaster2.gameObject, blasterDestroyTime);
        Destroy(IXblaster3.gameObject, blasterDestroyTime);
        Destroy(IXblaster4.gameObject, blasterDestroyTime);
        Destroy(IXblaster5.gameObject, blasterDestroyTime);
    }
}
