using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using MLAPI;
using MLAPI.Messaging;
using MLAPI.SceneManagement;
using System;
using MLAPI.NetworkVariable;

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
    public bool clientSet = false;

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
    private readonly int[] times = { 120, 600, 120, 600, 0 };
    private bool timerIsActive = false;
    private NetworkVariableInt stage = new NetworkVariableInt(new NetworkVariableSettings { WritePermission = NetworkVariablePermission.ServerOnly }, 0);
    private float timeRemaining = 0;
    private bool handRaised = false;

    private void Awake()
    {
        DontDestroyOnLoad(this);
        stage.OnValueChanged += OnChangeStage;
        biologiaClips.FirstAudio = Resources.Load("Audios/P1_Camila_Biologia") as AudioClip;
        biologiaClips.SecondAudio = Resources.Load("Audios/P2_Jorge_Biologia") as AudioClip;
        lenguajeClips.FirstAudio = Resources.Load("Audios/P1_Camila_Lenguaje") as AudioClip;
        lenguajeClips.SecondAudio = Resources.Load("Audios/P2_Jorge_Lenguaje") as AudioClip;
        matematicasClips.FirstAudio = Resources.Load("Audios/P1_Camila_Matematicas") as AudioClip;
        matematicasClips.SecondAudio = Resources.Load("Audios/P2_Jorge_Matematicas") as AudioClip;
        informarClips.FirstAudio = Resources.Load("Audios/P1_Matias_Informar") as AudioClip;
        informarClips.SecondAudio = Resources.Load("Audios/P2_Catalina_Informar") as AudioClip;
    }

    public void SaveClientData(string name, string age, string genre, string experience, string discipline)
    {
        if (!IsHost || !IsOwner) return;

        Debug.Log("saved client data");

        clientData.Name = name;
        clientData.Age = age;
        clientData.Genre = genre;
        clientData.Experience = experience;
        clientData.Discipline = discipline;
        if (discipline == "Lenguaje") _disclipine = Discipline.Lenguaje;
        if (discipline == "Biología") _disclipine = Discipline.Biologia;
        if (discipline == "Matemáticas") _disclipine = Discipline.Matematicas;
        clientSet = true;

        StartGame();
    }

    public void StartGame()
    {
        if (!IsHost || !IsOwner) return;
        NetworkSceneManager.SwitchScene("Main");
    }

    void SetUp()
    {
        if (!IsHost || !IsOwner) return;

        GameObject.Find("Students/camila").TryGetComponent(out StundentController camila);
        GameObject.Find("Students/jorge").TryGetComponent(out StundentController jorge);
        GameObject.Find("Students/catalina").TryGetComponent(out StundentController catalina);
        GameObject.Find("Students/matias").TryGetComponent(out StundentController matias);

        if (_disclipine != Discipline.Informar)
        {
            camila.AsPlayer1();
            camila.SetAudio(GetClip(1));

            jorge.AsPlayer2();
            jorge.SetAudio(GetClip(2));

            matias.AsUnplayable();
            catalina.AsUnplayable();
        }
        else
        {
            camila.AsUnplayable();
            jorge.AsUnplayable();

            matias.AsPlayer1();
            matias.SetAudio(GetClip(1));

            catalina.AsPlayer2();
            catalina.SetAudio(GetClip(2));
        }
    }

    public void SetLenguajeClip(int index, AudioClip clip)
    {
        if (!IsHost || !IsOwner) return;
        if (index > 1) return;

        if (index == 0) lenguajeClips.FirstAudio = clip;
        else lenguajeClips.SecondAudio = clip;
    }
    public void SetBiologiaClip(int index, AudioClip clip)
    {
        if (!IsHost || !IsOwner) return;
        if (index > 1) return;

        if (index == 0) biologiaClips.FirstAudio = clip;
        else biologiaClips.SecondAudio = clip;
    }
    public void SetMatematicasClip(int index, AudioClip clip)
    {
        if (!IsHost || !IsOwner) return;
        if (index > 1) return;

        if (index == 0) matematicasClips.FirstAudio = clip;
        else matematicasClips.SecondAudio = clip;
    }
    public void SetInformarClip(int index, AudioClip clip)
    {
        if (!IsHost || !IsOwner) return;
        if (index > 1) return;

        if (index == 0) informarClips.FirstAudio = clip;
        else informarClips.SecondAudio = clip;
    }

    public void StartTimer()
    {
        if (!IsHost || !IsOwner) return;

        SetUp();
        timeRemaining = times[stage.Value];
        timerIsActive = true;
    }

    void OnChangeStage(int oldValue, int newValue)
    {
        if (IsHost) return;

        Camera.main.transform.Find("Canvas").gameObject.SetActive(newValue % 2 == 0);
    }


    private void Update()
    {
        if (!IsHost || !IsOwner) return;
        if (!timerIsActive) return;

        if (stage.Value % 2 == 1 && !handRaised && timeRemaining < times[stage.Value] / 2)
        {
            GameObject.Find("Students/camila").TryGetComponent(out StundentController camila);
            GameObject.Find("Students/jorge").TryGetComponent(out StundentController jorge);
            GameObject.Find("Students/catalina").TryGetComponent(out StundentController catalina);
            GameObject.Find("Students/matias").TryGetComponent(out StundentController matias);

            camila.RaiseHand();
            jorge.RaiseHand();
            catalina.RaiseHand();
            matias.RaiseHand();

            handRaised = true;
        }

        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
        }
        else
        {
            stage.Value++;
            timeRemaining = times[stage.Value];

            Camera.main.transform.Find("Canvas").TryGetComponent(out StatsController stats);
            stats.SetStage(stage.Value);

            if (stage.Value == 2) {
                _disclipine = Discipline.Informar;
                handRaised = false;
                SetUp();
            }

            if (stage.Value == 4)
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
    public void Disconnect()
    {
        if (IsHost)
        {
            NetworkManager.Singleton.StopHost();
        }
        else if (IsClient)
        {
            NetworkManager.Singleton.StopClient();
        }
        else if (IsServer)
        {
            NetworkManager.Singleton.StopServer();
        }
    }

    public event EventHandler LastStageReached;
}
