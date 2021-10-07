using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using MLAPI.Serialization.Pooled;
using MLAPI.Transports;
using MLAPI.NetworkVariable;

[Serializable]
public class NetworkVariableAudioClip : NetworkVariable<AudioClip>
{
    /// <inheritdoc />
    public NetworkVariableAudioClip() { }

    /// <inheritdoc />
    public NetworkVariableAudioClip(NetworkVariableSettings settings) : base(settings) { }

    /// <inheritdoc />
    public NetworkVariableAudioClip(AudioClip clip) : base(clip) { }

    /// <inheritdoc />
    public NetworkVariableAudioClip(NetworkVariableSettings settings, AudioClip clip) : base(settings, clip) { }
}
