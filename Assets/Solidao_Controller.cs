using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Solidao_Controller : MonoBehaviour
{
    PlayerSoundConfig soundConfig;

    void Start()
    {
        soundConfig = FindObjectOfType<PlayerSoundConfig>();

        soundConfig.SetSolidao1();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
