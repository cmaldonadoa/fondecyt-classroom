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
    private AudioSource audioSource;
    private KeyControl keyControl = null;
    private bool isTalking = false;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (!IsHost) return;

        if (!!audioSource.clip && !audioSource.isPlaying && isTalking)
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

            animator.SetBool("IsHandUp", !animator.GetBool("IsHandUp"));

            Camera.main.transform.Find("Canvas").TryGetComponent(out StatsController stats);
            stats.NewLapFromInput();
        }
    }

    public void RaiseHand(Player onlyPlayer=Player.Player1)
    {
        if (player != onlyPlayer) return;

        animator.SetBool("IsHandUp", true);
        Camera.main.transform.Find("Canvas").TryGetComponent(out StatsController stats);
        stats.NewLapFromInput();
    }

    public void SetAudio(AudioClip clip)
    {
        audioSource.clip = clip;
    }

    public void AsPlayer1()
    {
        player = Player.Player1;
        keyControl = Keyboard.current.digit1Key;
    }

    public void AsPlayer2()
    {
        player = Player.Player2;
        keyControl = Keyboard.current.digit2Key;
    }

    public void AsUnplayable()
    {
        player = Player.None;
        keyControl = null;
    }
}
