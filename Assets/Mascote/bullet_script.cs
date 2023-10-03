using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.ParticleSystem;

public class bullet_script : MonoBehaviour
{
    public ParticleSystem deflecteffect;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name.Equals("Main Camera"))
        {
            Debug.Log("Touché");
            Destroy(gameObject);
        }
        else if (other.gameObject.name.Equals("LightSaber blade"))
        {
            Debug.Log("la balle a été paré");
            Instantiate(deflecteffect, gameObject.transform.position, Quaternion.identity);
            deflecteffect.GetComponent<ParticleSystem>().Play();
            Debug.Log("parent "+ other.gameObject.transform.parent);
            try
            {
                other.gameObject.transform.parent.GetComponent<Lightsaber>().Deflect(); //on essaye de jouer le deflect
            }
            catch { }//j'ai pas besoins de faire qqc de spéciale
        }

    }
}
