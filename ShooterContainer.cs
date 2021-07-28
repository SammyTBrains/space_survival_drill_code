using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterContainer : MonoBehaviour
{
    private int rotSpeed = 30;

    public int rotateZval;

    private void Start()
    {
        StartCoroutine(ChangeDirectionRoutine());
    }

    // Update is called once per frame
    void Update()
    {
        int TotalCP = PlayerPrefs.GetInt("TotalCP");
        if (TotalCP >= 10 && TotalCP <= 20)
        {
            rotSpeed = 50;
        }
        else if (TotalCP >= 20 && TotalCP <= 30)
        {
            rotSpeed = 80;
        }
        else if (TotalCP >= 30)
        {
            rotSpeed = 120;
        }
        transform.Rotate(new Vector3(0, 0, rotateZval) * rotSpeed * Time.deltaTime);
        ClampAngle(transform.rotation.z, -53, 53);
    }

    IEnumerator ChangeDirectionRoutine()
    {
        while (true)
        {
            int randNo = Random.Range(0, 2);

            if (randNo == 0)
            {
                rotateZval = -1;
            }
            else if(randNo == 1)
            {
                rotateZval = 1;
            }
            int TotalCP = PlayerPrefs.GetInt("TotalCP");
            if (TotalCP < 10)
            {
                yield return new WaitForSeconds(0.65f);
            }
            else if (TotalCP >= 10 && TotalCP <= 20)
            {
                yield return new WaitForSeconds(0.5f);
            }
            else if (TotalCP >= 20 && TotalCP <= 30)
            {
                yield return new WaitForSeconds(0.35f);
            }
            else if (TotalCP >= 30)
            {
                yield return new WaitForSeconds(0.2f);
            }
        }
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

}
