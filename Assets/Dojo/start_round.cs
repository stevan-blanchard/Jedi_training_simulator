using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class start_round : MonoBehaviour
{
    // Start is called before the first frame update

    public bool playerIn;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void OnTriggerEnter(Collider other)
    {
        //Debug.Log(other.tag);
        if (other.CompareTag("MainCamera"))
        {
            playerIn = true;
            return;
        }
    }
}
