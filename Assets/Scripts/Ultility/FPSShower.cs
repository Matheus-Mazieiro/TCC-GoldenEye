using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FPSShower : MonoBehaviour
{
    public Text fpsLabel;
    public float updateInterval = 0.5F;

    private float accum = 0; // FPS accumulated over the interval
    private int frames = 0; // Frames drawn over the interval
    private float timeleft; // Left time for current interval

    void Awake()
    {
        if (fpsLabel == null)
        {
            fpsLabel = GetComponent<Text>();
        }
    }

    void Start()
    {
        if (!fpsLabel)
        {
            Debug.Log("UtilityFramesPerSecond needs a UILabel component!");
            enabled = false;
            return;
        }
        timeleft = updateInterval;
    }

    void Update()
    {
        timeleft -= Time.deltaTime;
        accum += Time.timeScale / Time.deltaTime;
        ++frames;

        // Interval ended - update GUI text and start new interval
        if (timeleft <= 0.0)
        {
            // display two fractional digits (f2 format)
            float fps = accum / frames;
            string format = System.String.Format("{0:F2} FPS", fps);
            fpsLabel.text = format;

            if (fps < 30)
                fpsLabel.color = Color.yellow;
            else
                if (fps < 10)
                fpsLabel.color = Color.red;
            else
                fpsLabel.color = Color.green;
            timeleft = updateInterval;
            accum = 0.0F;
            frames = 0;
        }
    }
}