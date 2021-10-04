using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using MLAPI;

public class StundentController : MonoBehaviour
{
    public enum Player {
        None,
        Player1,
        Player2
    }

    public Player player;

    Animator animator;
    AudioClip[] clips = new AudioClip[2];
    AudioSource audioSource;
    KeyControl keyControl = null;


    void Start()
    {
        if (GameStateLocal.GetInstanceType() == GameStateLocal.GameInstanceType.Client) return;

        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        clips = GameStateGlobal.GetClips();

        if (player == Player.Player1)
        {
            keyControl = Keyboard.current.digit1Key;
        }
        else if (player == Player.Player2)
        {
            keyControl = Keyboard.current.digit2Key;
        }
    }

    void Update()
    {
        if (keyControl == null) return;

            if (keyControl.wasPressedThisFrame)
            {
                if (animator.GetBool("IsHandUp"))
                {
                    audioSource.Play();
                }
                else
                {
                    audioSource.clip = clips[(int)player - 1];
                }

                animator.SetBool("IsHandUp", !animator.GetBool("IsHandUp"));
            var canvas = GameObject.FindGameObjectsWithTag("HostView")[0];
            canvas.GetComponent<StatsController>().NewLap();
            }
        
    }
}
