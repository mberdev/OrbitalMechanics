using TMPro;
using UnityEngine;
using System;
using System.Collections.Generic;

public class FPSText : MonoBehaviour
{
    private Queue<float> frameTimes = new Queue<float>();
    private float frameTimeSum = 0;
    private float timer = 0;

    void Update()
    {
        // Add the current frame time to the queue and the sum
        float currentFrameTime = Time.deltaTime;
        frameTimes.Enqueue(currentFrameTime);
        frameTimeSum += currentFrameTime;

        // We work approximately with the last 2 seconds
        while (frameTimeSum > 2)
        {
            float oldestFrameTime = frameTimes.Dequeue();
            frameTimeSum -= oldestFrameTime;
        }

        // Calculate and log the average FPS once per second
        timer += Time.deltaTime;
        if (timer > 1)
        {
            float averageFPS = frameTimes.Count / frameTimeSum;

            var textControl = gameObject.GetComponent<TextMeshProUGUI>();
            textControl.text = $"FPS {averageFPS}";

            timer = 0;
        }
    }





}
