using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Asteroid : MonoBehaviour
{
    [SerializeField]
    private float tumble;
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
        StartCoroutine(TumbleRoutine());
    }

    public void ReduceHealth(float val)
    {
        if (health <= 100 && health > 0)
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

    IEnumerator TumbleRoutine()
    {
        while (true)
        {
            GetComponent<Rigidbody>().angularVelocity = Random.insideUnitSphere * tumble;
            yield return new WaitForSeconds(2f);
        }
    }
}