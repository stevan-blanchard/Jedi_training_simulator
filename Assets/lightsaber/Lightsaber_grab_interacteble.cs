using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Lightsaber_grab_interacteble : XRGrabInteractable
{
    // Start is called before the first frame update

    private Controler_Velocity controler_Velocity = null;
    private Vector3 Velocity = Vector3.zero;
    protected override void Awake()
    {
        base.Awake();
    }

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        base.OnSelectEntered(args);
        controler_Velocity = args.interactorObject.transform.parent.GetComponent<Controler_Velocity>();
        Debug.Log(args.interactorObject.transform.parent.name);
    }

    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        base.OnSelectExited(args);
        controler_Velocity = null;
    }
    public override void ProcessInteractable(XRInteractionUpdateOrder.UpdatePhase updatePhase)
    {
        base.ProcessInteractable(updatePhase);
        if (isSelected) { 
            if(updatePhase == XRInteractionUpdateOrder.UpdatePhase.Dynamic)
            {
                Velocity = controler_Velocity.Velocity;
            }
        }
    }

    public Vector3 GetVelocity()
    {
        return Velocity;
    }

}
