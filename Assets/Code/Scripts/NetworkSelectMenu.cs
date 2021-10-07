using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using MLAPI;

public class NetworkSelectMenu : MonoBehaviour
{
    public void PlayAsServer()
    {
        NetworkManager.Singleton.StartHost();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }    
    public void PlayAsClient()
    {
        NetworkManager.Singleton.StartClient();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
