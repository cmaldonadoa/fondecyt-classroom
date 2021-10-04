using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using MLAPI;
using MLAPI.SceneManagement;

public class InitMenu : MonoBehaviour
{
    void PlayGame()
    {
        NetworkSceneManager.SwitchScene("Classroom");
    }
    public void PlayGameLenguaje()
    {
        GameStateGlobal.gameVariant = GameStateGlobal.GameVariant.Lenguaje;
        PlayGame();
    }
    public void PlayGameMatematicas()
    {
        GameStateGlobal.gameVariant = GameStateGlobal.GameVariant.Matematicas;
        PlayGame();
    }
    public void PlayGameBiologia()
    {
        GameStateGlobal.gameVariant = GameStateGlobal.GameVariant.Biologia;
        PlayGame();
    }
}
