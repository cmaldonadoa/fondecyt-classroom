using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public int maxMinutes;
    private float startTime;
    private bool stopped;
    private string currentTime;

    private string format(string n)
    {
        if (n.Length == 2) return n;
        return "0" + n;
    }

    void Start()
    {
        startTime = Time.time;
    }

    void Update()
    {
        if (stopped) return;

        float dt = Time.time - startTime;
        int minutes = (int) dt / 60;
        float seconds = dt % 60;

        currentTime = format(minutes.ToString()) + ":" + format(seconds.ToString("f2"));
        GetComponent<Text>().text = currentTime;

        if (minutes == maxMinutes) stopped = true;
    }

    public string GetTime()
    {
        return currentTime;
    }
}
