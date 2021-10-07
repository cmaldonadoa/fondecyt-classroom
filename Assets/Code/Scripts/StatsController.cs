using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatsController : MonoBehaviour
{
    public Timer clock;
    private int numberOfLaps;
    private bool handUp = false;

    void Awake()
    {
        GameObject.FindWithTag("GameController").TryGetComponent(out ServerManager server);
        transform.Find("Data/Name").GetComponent<Text>().text = server.clientData.Name;
        transform.Find("Data/Age").GetComponent<Text>().text = server.clientData.Age + " años";
        transform.Find("Data/Genre").GetComponent<Text>().text = server.clientData.Genre;
        transform.Find("Data/Experience").GetComponent<Text>().text = server.clientData.Experience + " años de experiencia";
        transform.Find("Data/Discipline").GetComponent<Text>().text = server.clientData.Discipline;

        server.StartTimer();
    }

    private void NewLap(string name)
    {
        var template = transform.Find("Registry").GetChild(numberOfLaps++);
        template.gameObject.SetActive(true);
        template.Find("Label").GetComponent<Text>().text = name;
        template.Find("Time").GetComponent<Text>().text = clock.GetTime();
    }

    public void NewLapFromInput()
    {
        var name = handUp ? "Bajar mano" : "Levantar mano";
        handUp = !handUp;
        NewLap(name);
    }

    public void SetStage(int i)
    {
        NewLap("Fin etapa " + i);
        GameObject.FindWithTag("GameController").TryGetComponent(out ServerManager server);
        server.LastStageReached += StopClockHandler;
    }

    private void StopClockHandler(object sender, EventArgs e)
    {
        clock.Stop();
    }
}
