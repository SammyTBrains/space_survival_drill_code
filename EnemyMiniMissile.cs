using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMiniMissile : MonoBehaviour
{
    [SerializeField]
    private float _speed = 80f;
    [SerializeField]
    private GameObject _explosion;

    private Player player;

    private float health;
    private float maxHealth = 100f;

    private void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        if(player == null)
        {
            Debug.LogError("Player is null in Enemy Mini Missile script");
        }
    }

    void Update()
    {
        int TotalCP = PlayerPrefs.GetInt("TotalCP");
        if (TotalCP < 10)
        {
            transform.Translate(Vector3.right * _speed * Time.deltaTime);
        }
        else if (TotalCP >= 10 && TotalCP <= 20)
        {
            transform.Translate(Vector3.right * (_speed + 44) * Time.deltaTime);
        }
        else if (TotalCP >= 20 && TotalCP <= 30)
        {
            transform.Translate(Vector3.right * (_speed + 88) * Time.deltaTime);
        }
        else if (TotalCP >= 30)
        {
            transform.Translate(Vector3.right * (_speed + 132) * Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Instantiate(_explosion, transform.position, Quaternion.identity); 
            int TotalCP = PlayerPrefs.GetInt("TotalCP");
            if(TotalCP < 10)
            {
                player.ReduceHealth(5);
            }
            else if (TotalCP >= 10 && TotalCP < 20)
            {
                player.ReduceHealth(10);
            }
            else if (TotalCP >= 20 && TotalCP < 30)
            {
                player.ReduceHealth(20);
            }
            else if (TotalCP >= 30 && TotalCP < 60)
            {
                player.ReduceHealth(30);
            }
            else if (TotalCP >= 60 && TotalCP < 100)
            {
                player.ReduceHealth(40);
            }
            else if (TotalCP >= 100)
            {
                player.ReduceHealth(50);
            }
            player._damage.SetActive(true);
        }
        if (other.tag == "Asteroid")
        {
            other.gameObject.GetComponent<Asteroid>().ReduceHealth(5f);
            Instantiate(_explosion, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
        if(other.tag == "Missile")
        {
            Instantiate(_explosion, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
        if (other.tag == "Laser")
        {
            Instantiate(_explosion, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
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
            Instantiate(_explosion, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
        if (health > 100)
        {
            health = maxHealth;
        }
    }

    public void DestroyMe()
    {
        Destroy(this.gameObject);
    }
}
