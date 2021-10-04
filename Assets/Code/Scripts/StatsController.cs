using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatsController : MonoBehaviour
{
    public Timer clock;
    private int numberOfLaps;

    public void NewLap()
    {
        var lap = transform.Find("Laps").transform.GetChild(numberOfLaps++);
        lap.Find("Time").GetComponent<Text>().text = clock.GetTime();
        lap.gameObject.SetActive(true);
    }
}
