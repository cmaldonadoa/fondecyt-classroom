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
        if (NetworkManager.Singleton.IsServer && allowedIn != NetworkType.Server) gameObject.SetActive(false) ;
        if (NetworkManager.Singleton.IsHost && allowedIn != NetworkType.Host) gameObject.SetActive(false);
        if (NetworkManager.Singleton.IsClient && allowedIn != NetworkType.Client) gameObject.SetActive(false);
    }
}
