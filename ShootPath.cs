using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShootPath : MonoBehaviour
{
    [SerializeField]
    private Slider slider;
    [SerializeField]
    private GameObject _explosion;

    private float maxHealth = 100f;
    private float health;

    private void Start()
    {
        health = maxHealth;
        slider.value = CalculateHealth();
    }

    public void ReduceHealth(float val)
    {
        if(health <= 100 && health > 0)
        {
            health -= val;
            slider.value = CalculateHealth();
        }
        else 
        {
            Instantiate(_explosion, this.transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }

    public void DestroyMe()
    {
        Destroy(this.gameObject);
    }

    float CalculateHealth()
    {
        return health / maxHealth;
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            Player player = other.GetComponent<Player>();

            if(player != null)
            {
                player.OnDamageExit();
            }
        }
    }
}
