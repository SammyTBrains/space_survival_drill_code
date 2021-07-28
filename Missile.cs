using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
    [SerializeField]
    private float _speed = 80f;
    [SerializeField]
    private GameObject _explosion;

    // Update is called once per frame
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
        Destroy(this.gameObject, 20f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Laser")
        {
            Instantiate(_explosion, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
        if (other.tag == "Asteroid" )
        {
            Instantiate(_explosion, transform.position, Quaternion.identity);
            Instantiate(_explosion, other.transform.position, Quaternion.identity);
            Destroy(other.gameObject);
            Destroy(this.gameObject);
        }
        if(other.name == "Enemy_Weapon")
        {
            Instantiate(_explosion, transform.position, Quaternion.identity);
            Instantiate(_explosion, other.transform.position, Quaternion.identity);
            Destroy(other.transform.parent.gameObject);
            Destroy(this.gameObject);
        }
    }
}
