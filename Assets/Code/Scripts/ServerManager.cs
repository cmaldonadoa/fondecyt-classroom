using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using MLAPI;
using MLAPI.Messaging;
using MLAPI.SceneManagement;
using System;

[Serializable]
public struct ClipsHolder
{
    [field: SerializeField]
    public AudioClip FirstAudio { get; set; }
    [field: SerializeField]
    public AudioClip SecondAudio { get; set; }
}
public struct ClientData 
{ 
    public string Name { get; set; }
    public string Age { get; set; }
    public string Genre { get; set; }
    public string Experience { get; set; }
    public string Discipline { get; set; }
}

public class ServerManager : NetworkBehaviour 
{
    enum Discipline
    {
        Biologia,
        Lenguaje,
        Matematicas,
        Informar
    }

    // Client data 
    public ClientData clientData;
    private Discipline _disclipine;

    // Audio files
    [SerializeField]
    private ClipsHolder biologiaClips;
    [SerializeField]
    private ClipsHolder lenguajeClips;
    [SerializeField]
    private ClipsHolder matematicasClips;
    [SerializeField]
    private ClipsHolder informarClips;

    // Timer
    private readonly int[] times = { 10, 10, 10, 10 };
    private bool timerIsActive = false;
    private int stage = 0;
    private float timeRemaining = 0;

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    public void SaveClientData(string name, string age, string genre, string experience, string discipline)
    {
        if (!IsServer) return;

        clientData.Name = name;
        clientData.Age = age;
        clientData.Genre = genre;
        clientData.Experience = experience;
        clientData.Discipline = discipline;
        if (discipline == "Lenguaje") _disclipine = Discipline.Lenguaje;
        if (discipline == "Biología") _disclipine = Discipline.Biologia;
        if (discipline == "Matemáticas") _disclipine = Discipline.Matematicas;
    }

    [ClientRpc]
    private void GoToClassroomClientRpc()
    {
        Debug.Log("aaaa");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void StartGame()
    {
        if (!IsServer) return;
        //GoToClassroomClientRpc();
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        NetworkSceneManager.SwitchScene("Classroom");
    }

    public void SetLenguajeClip(int index, AudioClip clip)
    {
        if (!IsServer) return;
        if (index > 1) return;

        if (index == 0) lenguajeClips.FirstAudio = clip;
        else lenguajeClips.SecondAudio = clip;
    }
    public void SetBiologiaClip(int index, AudioClip clip)
    {
        if (!IsServer) return;
        if (index > 1) return;

        if (index == 0) biologiaClips.FirstAudio = clip;
        else biologiaClips.SecondAudio = clip;
    }
    public void SetMatematicasClip(int index, AudioClip clip)
    {
        if (!IsServer) return;
        if (index > 1) return;

        if (index == 0) matematicasClips.FirstAudio = clip;
        else matematicasClips.SecondAudio = clip;
    }
    public void SetInformarClip(int index, AudioClip clip)
    {
        if (!IsServer) return;
        if (index > 1) return;

        if (index == 0) informarClips.FirstAudio = clip;
        else informarClips.SecondAudio = clip;
    }


    [ClientRpc]
    private void SetAudiosClientRpc(AudioClip a1, AudioClip a2)
    {
        Debug.Log("Client audios set");

        //Camera.main.transform.Find("Canvas").TryGetComponent(out ClientManager client);
        //client.SetClips(a1, a2);
    }

    public void StartTimer()
    {
        if (!IsServer) return;
        SetAudiosClientRpc(GetClip(1), GetClip(2));
        timeRemaining = times[stage];
        timerIsActive = true;
    }

    [ClientRpc]
    private void UpdateClientRpc(int stage)
    {
        Debug.Log("Client Updated");

        //var state = stage % 2 == 0;
        //Camera.main.transform.Find("Canvas").gameObject.SetActive(state);
    }

    private void Update()
    {
        if (!IsServer) return;
        if (!timerIsActive) return;

        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
        }
        else
        {
            timeRemaining = times[stage];
            stage++;

            Camera.main.transform.Find("Canvas").TryGetComponent(out StatsController stats);
            stats.SetStage(stage);

            UpdateClientRpc(stage);

            if (stage == 2) _disclipine = Discipline.Informar;

            if (stage > 3)
            {
                timerIsActive = false;
                LastStageReached.Invoke(this, EventArgs.Empty);
            }

        }
    }

    public AudioClip GetClip(int questionNumber)
    {
        if (questionNumber == 1)
        {
            if (_disclipine == Discipline.Biologia) return biologiaClips.FirstAudio;
            if (_disclipine == Discipline.Lenguaje) return lenguajeClips.FirstAudio;
            if (_disclipine == Discipline.Matematicas) return matematicasClips.FirstAudio;
            if (_disclipine == Discipline.Informar) return informarClips.FirstAudio;
        }

        if (questionNumber == 2)
        {
            if (_disclipine == Discipline.Biologia) return biologiaClips.SecondAudio;
            if (_disclipine == Discipline.Lenguaje) return lenguajeClips.SecondAudio;
            if (_disclipine == Discipline.Matematicas) return matematicasClips.SecondAudio;
            if (_disclipine == Discipline.Informar) return informarClips.SecondAudio;
        }

        return null;
    }

    public event EventHandler LastStageReached;
}
