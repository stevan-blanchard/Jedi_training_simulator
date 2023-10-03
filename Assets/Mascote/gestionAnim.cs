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

    public bool canShoot;

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

    public void CanShoot(int value)
    {
        canShoot = value == 1;
    }

    public void TirSound() { 

        source.PlayOneShot(sound_tir);

    }

    public void DeploySound()
    {
        source.PlayOneShot(sound_deploy);
    }

    public void MovSound()
    {
        source.PlayOneShot(sound_mov);
    }

    public void CloseSound()
    {
        source.PlayOneShot(sound_close);
    }
}
