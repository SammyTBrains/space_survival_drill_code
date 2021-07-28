using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField]
    private GameObject _shootSpot;
    [SerializeField]
    private GameObject _joystick;
    [SerializeField]
    private float _turnSpeed = 1f, _joystickTurnSpeed = 1f;
    [SerializeField]
    private AudioClip _hit;
    [SerializeField]
    private float _enemyBarReductionValue = 2f;
    [SerializeField]
    private GameObject HUD;
    [SerializeField]
    private Slider slider;
    [SerializeField]
    private GameObject _gun;
    [SerializeField]
    private GameObject _explosion;
    [SerializeField]
    private GameObject _missile;
    [SerializeField]
    private GameObject missileLight, gunLight;
    [SerializeField]
    private GameObject disabledImage;
    [SerializeField]
    private GameObject spawnMangerGameObject;
    public float health;
    private float _barReductionValue = 0.5f;
    public GameObject _damage;

    RaycastHit hitInfo;
    private SpawnManager spawnManager;
    private PopUpScreenManager PUSM;
    private float maxHealth = 100f;

    public bool onPath = false;
    public bool isMissileReady = true;
    private bool missileFire = false;
    private bool gunFire = false;
    private bool isSelectRight = false;
    private bool isSelectLeft = false;
    private bool isShoot = false;
    private bool isMissile = false;
   
    [SerializeField]
    private float _speed = 36.0f;

    private void Start()
    {
        transform.position = new Vector3(0, 0, 0);
        spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        if (spawnManager != null)
        {
           spawnManager.CheckPointInstantiate();
        }
        PUSM = GameObject.Find("PopUpScreenManager").GetComponent<PopUpScreenManager>();
        if(PUSM == null)
        {
            Debug.LogError("PopUpScreenManager is null!");
        }

        health = maxHealth;
        slider.value = CalculateHealth();
    }

    private void OnApplicationPause(bool pause)
    {
        Time.timeScale = 1;

        PlayerPrefs.DeleteKey("TotalCP");
        PlayerPrefs.DeleteKey("DestroyedBefore");
        PlayerPrefs.DeleteKey("healthUsed");
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector3(0, 0, 1) * _speed * Time.deltaTime);
        Camera.main.transform.position = Camera.main.transform.position;
        slider.value = CalculateHealth();
        if (onPath)
        {
            ReduceHealth(_barReductionValue);
        }
        PCMovement();
        if (isSelectRight)
        {
            Movement(1);
        }
        if (isSelectLeft)
        {
            Movement(-1);
        }
        Fire();
        if (isShoot)
        {
            if (Physics.Raycast(_gun.transform.position, transform.TransformDirection(Vector3.forward), out hitInfo, Mathf.Infinity))
            {
                Shoot();
            }
        }
        if (isMissile)
        {
            Missile();
        }
        if (gunFire)
        {
            gunLight.SetActive(true);
        }
        else
        {
            gunLight.SetActive(false);
        }
        if (missileFire)
        {
            missileLight.SetActive(true);
        }
        else
        {
            missileLight.SetActive(false);
        }

        int TotalCP = PlayerPrefs.GetInt("TotalCP");
        if(TotalCP >= 10 && TotalCP < 20)
        {
            _speed = 70;
            _barReductionValue = 1f;
        }
        else if (TotalCP >= 20 && TotalCP < 30)
        {
            _speed = 104;
            _barReductionValue = 1.8f;
        }
        else if (TotalCP >= 30 && TotalCP < 60)
        {
            _speed = 118;
            _barReductionValue = 3.5f;
        }
        else if(TotalCP >= 60 && TotalCP < 100)
        {
            _barReductionValue = 5f;
        }
        else if(TotalCP >= 100)
        {
            _barReductionValue = 9f;
        }
        UpdateHighScore();
    }

    public void playHitSound()
    {
        AudioSource.PlayClipAtPoint(_hit, transform.position);
    }

    public void MoveRight()
    {
        isSelectRight = true;
    }

    public void MoveLeft()
    {
        isSelectLeft = true;
    }

    public void StopRight()
    {
        isSelectRight = false;
    }

    public void StopLeft()
    {
        isSelectLeft = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Laser")
        {
            Instantiate(_explosion, transform.position, Quaternion.identity);
            int TotalCP = PlayerPrefs.GetInt("TotalCP");
            if (TotalCP < 20)
            {
                ReduceHealth(5f);
            }
            else if (TotalCP >= 20 && TotalCP < 30)
            {
                ReduceHealth(10f);
            }
            else if (TotalCP >= 30 && TotalCP < 60)
            {
                ReduceHealth(15f);
            }
            else if (TotalCP >= 60 && TotalCP < 100)
            {
                ReduceHealth(25f);
            }
            else if (TotalCP >= 100)
            {
                ReduceHealth(40f);
            }
            _damage.SetActive(true);
        }
        if (other.tag == "Asteroid")
        {
            ReduceHealth(10f);
            _damage.SetActive(true);
            Instantiate(_explosion, other.transform.position, Quaternion.identity);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Laser")
        {
            onPath = false;
            _damage.SetActive(false);
        }
        if (other.tag == "Asteroid")
        {
            _damage.SetActive(false);
            other.transform.gameObject.GetComponent<Asteroid>().DestroyMe();
        }
        if (other.tag == "Blaster")
        {
            _damage.SetActive(false);
            Destroy(other.transform.parent.gameObject);
        }
        if(other.tag == "EMM")
        {
            _damage.SetActive(false);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Blaster")
        {
            ReduceHealth(0.1f);
            _damage.SetActive(true);
            Instantiate(_explosion, other.transform.position, Quaternion.identity);
        }
    }

    public void ReduceHealth(float val)
    {
        if (health <= 100 && health > 0)
        {
            health -= val;
        }
        else if (health <= 0)
        {
            Camera.main.transform.SetParent(null);
            Instantiate(_explosion, transform.position, Quaternion.identity);
            PUSM.DisplayToContinue();
            spawnMangerGameObject.transform.SetParent(null);
            this.gameObject.SetActive(false);
        }
        if (health > 100)
        {
            health = maxHealth;
        }
        slider.value = CalculateHealth();
    }

    void UpdateHighScore()
    {
        if (PlayerPrefs.HasKey("HighScore"))
        {
            int highScore = PlayerPrefs.GetInt("HighScore");
            int totalCp = PlayerPrefs.GetInt("TotalCP");
            if(highScore < totalCp)
            {
                PlayerPrefs.SetInt("HighScore", totalCp);
            }
        }
        else
        {
            int totalCp = PlayerPrefs.GetInt("TotalCP");
            PlayerPrefs.SetInt("HighScore", totalCp);
        }
    }

    public void CameraParentSet()
    {
        Camera.main.transform.SetParent(this.gameObject.transform);
    }

    float CalculateHealth()
    {
        return health / maxHealth;
    }

    public void OnShootPressed()
    {
        isShoot = true;
    }

    public void OnShootRelease()
    {
        isShoot = false;
        gunFire = false;
    }

    public void OnMissile()
    {
        isMissile = true;
    }

    public void OnMissileRelease()
    {
        isMissile = false;
        StartCoroutine(missileFireRoutine());
    }

    void Shoot()
    {
        Vector3 pos = new Vector3(transform.position.x, transform.position.y, transform.position.z + 20);
        gunFire = true;
        GameObject fireEffect = Instantiate(_shootSpot, pos, Quaternion.identity);
        GameObject hitMark = Instantiate(_shootSpot, hitInfo.point, Quaternion.identity);
        if (hitInfo.transform.name == "Enemy_Weapon")
        {
            hitInfo.transform.parent.gameObject.GetComponent<ShootPath>().ReduceHealth(_enemyBarReductionValue);
            int TotalCP = PlayerPrefs.GetInt("TotalCP");
            if (TotalCP >= 30)
            {
                hitInfo.transform.parent.gameObject.GetComponent<ShootPath>().DestroyMe();
                Instantiate(_explosion, hitInfo.transform.position, Quaternion.identity);
            }
        }
        else if (hitInfo.transform.tag == "Asteroid")
        {
            hitInfo.transform.gameObject.GetComponent<Asteroid>().ReduceHealth(80f);
            int TotalCP = PlayerPrefs.GetInt("TotalCP");
            if (TotalCP >= 30)
            {
                hitInfo.transform.gameObject.GetComponent<Asteroid>().DestroyMe();
                Instantiate(_explosion, hitInfo.transform.position, Quaternion.identity);
            }
        }
        else if (hitInfo.transform.tag == "EMM")
        {
            hitInfo.transform.gameObject.GetComponent<EnemyMiniMissile>().ReduceHealth(80f);
            int TotalCP = PlayerPrefs.GetInt("TotalCP");
            if (TotalCP >= 30)
            {
                hitInfo.transform.gameObject.GetComponent<EnemyMiniMissile>().DestroyMe();
                Instantiate(_explosion, hitInfo.transform.position, Quaternion.identity);
            }
        }
        int soundEffect = PlayerPrefs.GetInt("SoundON");
        if(soundEffect == 1)
        {
            AudioSource.PlayClipAtPoint(_hit, pos);
            AudioSource.PlayClipAtPoint(_hit, hitInfo.point);
        }
        Destroy(hitMark.gameObject, 1f);
        Destroy(fireEffect.gameObject, 2f);
    }

    void Missile() {
        missileFire = true;
        if (isMissileReady == true)
        {
            isMissileReady = false;
            Vector3 missilepos = new Vector3(transform.position.x, transform.position.y - 2.33f, transform.position.z);
            Instantiate(_missile, missilepos, Quaternion.Euler(0, -90, 0));
            disabledImage.SetActive(true);
            StartCoroutine(MissileRoutine());
        }
    }

    void Fire()
    {
        if (Physics.Raycast(_gun.transform.position, transform.TransformDirection(Vector3.forward), out hitInfo, Mathf.Infinity))
        {
            if (Input.GetKey(KeyCode.Space))
            {
                Shoot();
            }
            if (Input.GetKeyUp(KeyCode.Space))
            {
                gunFire = false;
            }
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                Missile();
            }
            if (Input.GetKeyUp(KeyCode.Tab))
            {
                StartCoroutine(missileFireRoutine());
            }
        }
    }

    IEnumerator missileFireRoutine()
    {
        yield return new WaitForSeconds(1f);
        missileFire = false;
    }

    public IEnumerator MissileRoutine()
    {
        yield return new WaitForSeconds(6f);
        disabledImage.SetActive(false);
        isMissileReady = true;
    }

    void PCMovement()
    {
            float Horizontal = Input.GetAxis("Horizontal");      
            Movement(Horizontal);
    }

    void Movement(float Horizontal)
    {
        transform.Rotate(new Vector3(0, 0, Horizontal) * _turnSpeed * Time.deltaTime);
        Vector3 newRotation = _joystick.transform.localEulerAngles;
        newRotation.z += (-Horizontal * _joystickTurnSpeed);
        newRotation.z = ClampAngle(newRotation.z, -18, 18);
        _joystick.transform.localEulerAngles = newRotation;
        transform.Translate(new Vector3(Horizontal, 0, 0) * _turnSpeed * Time.deltaTime);
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -13.0f, 13.0f), Mathf.Clamp(transform.position.y, -15.0f, 15.0f), transform.position.z);
    }



    public static float ClampAngle(float angle, float min, float max)
    {
        angle = Mathf.Repeat(angle, 360);
        min = Mathf.Repeat(min, 360);
        max = Mathf.Repeat(max, 360);
        bool inverse = false;
        var tmin = min;
        var tangle = angle;
        if (min > 180)
        {
            inverse = !inverse;
            tmin -= 180;
        }
        if (angle > 180)
        {
            inverse = !inverse;
            tangle -= 180;
        }
        var result = !inverse ? tangle > tmin : tangle < tmin;
        if (!result)
            angle = min;

        inverse = false;
        tangle = angle;
        var tmax = max;
        if (angle > 180)
        {
            inverse = !inverse;
            tangle -= 180;
        }
        if (max > 180)
        {
            inverse = !inverse;
            tmax -= 180;
        }

        result = !inverse ? tangle < tmax : tangle > tmax;
        if (!result)
            angle = max;
        return angle;
    }

    public void OnDamageExit()
    {
            onPath = false;
            _damage.SetActive(false);
    }
}