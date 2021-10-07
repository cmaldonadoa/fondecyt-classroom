using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAPI;

public class ClientManager : NetworkBehaviour
{
    private AudioClip firstAudio;
    private AudioClip secondAudio;

    public void SetClips(AudioClip a1, AudioClip a2)
    {
        firstAudio = a1;
        secondAudio = a2;
    }
}
