using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAPI;
using MLAPI.NetworkVariable;

public class GameStateGlobal : NetworkBehaviour
{
    public enum GameVariant
    {
        Lenguaje,
        Matematicas,
        Biologia,
        Informar
    }

    private static readonly AudioClip[] lenguajeClips = new AudioClip[2];
    private static readonly AudioClip[] matematicasClips = new AudioClip[2];
    private static readonly AudioClip[] biologiaClips = new AudioClip[2];
    private static readonly AudioClip[] informarClips = new AudioClip[2];
    public static GameVariant gameVariant;

    public AudioClip[] lenguaje = new AudioClip[2];
    public AudioClip[] matematicas = new AudioClip[2];
    public AudioClip[] biologia = new AudioClip[2];
    public AudioClip[] informar = new AudioClip[2];

    private void Awake()
    {
        DontDestroyOnLoad(this);
        SetDefaultLenguaje();
        SetDefaultMatematicas();
        SetDefaultBiologia();
        SetDefaultInformar();
    }

    private void SetDefaultLenguaje()
    {
        SetLenguajeClip(0, lenguaje[0]);
        SetLenguajeClip(1, lenguaje[1]);
    }
    private void SetDefaultMatematicas()
    {
        SetMatematicasClip(0, matematicas[0]);
        SetMatematicasClip(1, matematicas[1]);
    }
    private void SetDefaultBiologia()
    {
        SetBiologiaClip(0, biologia[0]);
        SetBiologiaClip(1, biologia[1]);
    }
    private void SetDefaultInformar()
    {
        SetInformarClip(0, informar[0]);
        SetInformarClip(1, informar[1]);
    }

    public static void SetLenguajeClip(int index, AudioClip clip)
    {
        lenguajeClips[index] = clip;
    }
    public static void SetMatematicasClip(int index, AudioClip clip)
    {
        matematicasClips[index] = clip;
    }
    public static void SetBiologiaClip(int index, AudioClip clip)
    {
        biologiaClips[index] = clip;
    }
    public static void SetInformarClip(int index, AudioClip clip)
    {
        informarClips[index] = clip;
    }

    public static AudioClip[] GetClips()
    {
        switch (gameVariant)
        {
            case GameVariant.Lenguaje:
                return lenguajeClips;
            case GameVariant.Matematicas:
                return matematicasClips;
            case GameVariant.Biologia:
                return biologiaClips;
            case GameVariant.Informar:
                return informarClips;
            default:
                return new AudioClip[2];
        }
    }
}