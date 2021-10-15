using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAPI;

public class CameraSwitch : MonoBehaviour
{
    public enum NetworkType {
        Client,
        Host,
        Server
    }
    
    [SerializeField]
    public NetworkType allowedIn;

    void Awake()
    {
        var IsOnlyServer = NetworkManager.Singleton.IsServer && !NetworkManager.Singleton.IsHost;
        var IsOnlyClient = NetworkManager.Singleton.IsClient && !NetworkManager.Singleton.IsHost;

        if (IsOnlyServer && allowedIn != NetworkType.Server) gameObject.SetActive(false) ;
        if (NetworkManager.Singleton.IsHost && allowedIn != NetworkType.Host) gameObject.SetActive(false);
        if (IsOnlyClient && allowedIn != NetworkType.Client) gameObject.SetActive(false);
    }
}
