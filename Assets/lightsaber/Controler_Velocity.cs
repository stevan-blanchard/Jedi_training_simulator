using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Velocity : MonoBehaviour
{

    public InputActionProperty velocityProperty;
    public Vector3 velocity { get; private set; } = Vector3.zero;
    // Update is called once per frame
    void Update()
    {
        velocity = velocityProperty.action.ReadValue<Vector3>();

    }
}
