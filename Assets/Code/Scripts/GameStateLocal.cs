using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateLocal : MonoBehaviour
{
    public enum GameInstanceType {
        Host,
        Client
    }

    private static GameInstanceType instanceType;

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    public static void AsHost()
    {
        instanceType = GameInstanceType.Host;
    }
    public static void AsClient()
    {
        instanceType = GameInstanceType.Client;
    }

    public static GameInstanceType GetInstanceType()
    {
        return instanceType;
    }
}