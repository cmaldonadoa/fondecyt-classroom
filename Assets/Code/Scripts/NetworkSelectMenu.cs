using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using MLAPI;
using MLAPI.SceneManagement;

public class NetworkSelectMenu : MonoBehaviour
{
    public void PlayAsServer()
    {
        NetworkManager.Singleton.StopHost();
        NetworkManager.Singleton.StartHost();
        NetworkSceneManager.SwitchScene("HostForm");
    }    
    public void PlayAsClient()
    {
        NetworkManager.Singleton.StartClient();
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
