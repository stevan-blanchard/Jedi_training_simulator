using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class gestionAnim : MonoBehaviour
{
    public Animator animator;
    private AudioSource source;
    public AudioClip sound_tir;
    public AudioClip sound_mov;
    public AudioClip sound_deploy;
    public AudioClip sound_close;

    public void Start()
    {
        source = gameObject.AddComponent<AudioSource>();
        source.spatialBlend = 1;
    }

    public void Ready(bool state) {
        try
        {
            animator.SetBool("Ready", state);
        }
        catch (Exception e)
        {
            Debug.Log(e.ToString());
        }
    }

    public void Tir_sound() { 

        source.PlayOneShot(sound_tir);

    }

    public void Deploy_sound()
    {
        Debug.Log("DEPLOIEMENT");
        source.PlayOneShot(sound_deploy);
    }

    public void Mov_sound()
    {
        source.PlayOneShot(sound_mov);
    }

    public void Close_sound()
    {
        source.PlayOneShot(sound_close);
    }
}
