using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using MLAPI;
using MLAPI.NetworkVariable;

public enum Player
{
    None,
    Player1,
    Player2
}

public class StundentController : NetworkBehaviour
{
    public Player player;

    private Animator animator;
    private NetworkVariableAudioClip clip = new NetworkVariableAudioClip();
    private AudioSource audioSource;
    private KeyControl keyControl = null;
    private bool isTalking = false;

    void Start()
    {
        if (!IsServer) return;

        GameObject.FindWithTag("GameController").TryGetComponent(out ServerManager server);
        clip.Value = server.GetClip((int)player);
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();

        if (player == Player.Player1) keyControl = Keyboard.current.digit1Key;
        else if (player == Player.Player2) keyControl = Keyboard.current.digit2Key;
    }

    void Update()
    {
        if (!IsServer) return;

        if (clip.Value && !audioSource.isPlaying && isTalking)
        {
            animator.SetBool("IsTalking", false);
            isTalking = false;
        }

        if (keyControl == null) return;

            if (keyControl.wasPressedThisFrame)
            {
                if (animator.GetBool("IsHandUp"))
                {
                    audioSource.Play();
                    animator.SetBool("IsTalking", true);
                    isTalking = true;
                }
                else
                {
                    audioSource.clip = clip.Value;
                }

                animator.SetBool("IsHandUp", !animator.GetBool("IsHandUp"));

                Camera.main.transform.Find("Canvas").TryGetComponent(out StatsController stats);
                stats.NewLapFromInput();
            }
    }
}
