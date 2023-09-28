using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gestionAnim : MonoBehaviour
{
    public Animator animator;
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
}
