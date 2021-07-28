using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WormHoleLight : MonoBehaviour
{
    private new Light light;

    // Update is called once per frame
    void Update()
    {
        int num = Random.Range(20, 31);
        light = this.GetComponent<Light>();
        light.intensity = num;
    }
}
