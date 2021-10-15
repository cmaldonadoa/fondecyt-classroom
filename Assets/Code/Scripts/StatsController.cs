using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public struct Registry
{
    public string time;
    public string name;
}

public struct Client
{
    public string name;
    public string age;
    public string genre;
    public string experience;
    public string discipline;
}

public class StatsController : MonoBehaviour
{
    public Timer clock;
    private int numberOfLaps;
    private bool handUp = false;
    private List<Registry> registries = new List<Registry>();
    private Client clientData;

    void Start()
    {
        var clients = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject c in clients)
        {
            c.TryGetComponent(out ServerManager server);

            if (!server.clientSet) continue;

            transform.Find("Data/Name").GetComponent<Text>().text = server.clientData.Name;
            transform.Find("Data/Age").GetComponent<Text>().text = server.clientData.Age + " años";
            transform.Find("Data/Genre").GetComponent<Text>().text = server.clientData.Genre;
            transform.Find("Data/Experience").GetComponent<Text>().text = server.clientData.Experience + " años de experiencia";
            transform.Find("Data/Discipline").GetComponent<Text>().text = server.clientData.Discipline;

            clientData.name = server.clientData.Name;
            clientData.age = server.clientData.Age + " años";
            clientData.genre = server.clientData.Genre;
            clientData.experience = server.clientData.Experience + " años de experiencia";
            clientData.discipline = server.clientData.Discipline;

            server.LastStageReached += StopClockHandler;
            server.StartTimer();
        }
    }

    private void NewLap(string name)
    {
        var template = transform.Find("Registry").GetChild(numberOfLaps++);
        var time = clock.GetTime();
        template.gameObject.SetActive(true);
        template.Find("Label").GetComponent<Text>().text = name;
        template.Find("Time").GetComponent<Text>().text = time;
        Registry reg = new Registry
        {
            time = time,
            name = name
        };
        registries.Add(reg);
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
    }

    private void StopClockHandler(object sender, EventArgs e)
    {
        clock.Stop();
        WriteFile();
    }
    private void WriteFile()
    {
        string path = Application.dataPath + "\\..\\REPORTE_" + new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds() + ".txt";
        using (StreamWriter sw = File.CreateText(path))
        {
            sw.WriteLine("Nombre: " + clientData.name);
            sw.WriteLine("Edad: " + clientData.age);
            sw.WriteLine("Género: " + clientData.genre);
            sw.WriteLine("Disciplina: " + clientData.discipline);
            sw.WriteLine("Experiencia: " + clientData.experience);
            sw.WriteLine("");

            foreach (Registry r in registries)
            {
                sw.WriteLine(r.time + " " + r.name);
            }
            sw.Flush();
            sw.Close();
        }
        Camera.main.transform.Find("Canvas").gameObject.SetActive(false);
        Camera.main.transform.Find("ExitMenu").gameObject.SetActive(true);
    }

}
