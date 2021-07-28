using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidMM : MonoBehaviour
{
    bool isMoveRight = false;
    bool isMoveLeft = false;

    // Update is called once per frame
    void FixedUpdate()
    {
        if (transform.position.x <= -59)
        {
            isMoveRight = true;
        }
        else if (transform.position.x >= 59)
        {
            isMoveLeft = true;
        }

        if (isMoveRight)
        {
            transform.Translate(new Vector3(10, 0, 0) * Time.deltaTime);
            transform.Rotate(new Vector3(-10, 0, 0));
        }
        else if (isMoveLeft)
        {
            transform.Translate(new Vector3(-10, 0, 0) * Time.deltaTime);
            transform.Rotate(new Vector3(10, 0, 0));
        }
        StartCoroutine(DestroyRoutine());   
    }

    IEnumerator DestroyRoutine()
    {
        yield return new WaitForSeconds(13f);
        Destroy(this.gameObject);
    }
}
