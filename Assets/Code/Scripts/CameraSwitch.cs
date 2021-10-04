using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitch : MonoBehaviour
{

    public GameStateLocal.GameInstanceType allowedIn;

    void Awake()
    {
        gameObject.SetActive(GameStateLocal.GetInstanceType() == allowedIn);
    }
}
