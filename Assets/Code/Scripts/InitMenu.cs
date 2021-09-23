using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InitMenu : MonoBehaviour
{
    void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void PlayGameLenguaje()
    {
        GameState.gameVariant = GameState.GameVariant.Lenguaje;
        PlayGame();
    }
    public void PlayGameMatematicas()
    {
        GameState.gameVariant = GameState.GameVariant.Matematicas;
        PlayGame();
    }
    public void PlayGameBiologia()
    {
        GameState.gameVariant = GameState.GameVariant.Biologia;
        PlayGame();
    }
}
