using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.ParticleSystem;

public class bullet_script : MonoBehaviour
{
    public ParticleSystem deflecteffect;
    public GameObject sender;
    public bool reflected = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("MainCamera"))
        {
            Debug.Log("Touch�");
            Destroy(gameObject);

            FindObjectOfType<Spawnscenario>().bullet_received++;
        }
        else if (other.gameObject.name.Equals("LightSaber blade") && !reflected)
        {
            reflected = true;
            Debug.Log("la balle a �t� par�");
            Instantiate(deflecteffect, gameObject.transform.position, Quaternion.identity);
            deflecteffect.GetComponent<ParticleSystem>().Play();
            Debug.Log("parent " + other.gameObject.transform.parent);
            try
            {
                other.gameObject.transform.parent.GetComponent<Lightsaber>().Deflect(); //on essaye de jouer le deflect
                GetComponent<Rigidbody>().velocity = GetComponent<Rigidbody>().velocity * -1;
            }
            catch { }//j'ai pas besoins de faire qqc de sp�ciale
        }
        
    }
}
