using System.Collections.Generic;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class Lightsaber : MonoBehaviour
{
    public bool active = false;
    public Color blade_color;
    public List<Blade> blades; // base de(s) la lame(s) 
    private AudioSource sourceprincipale;
    private AudioSource sourcesecondaire;
    public AudioClip up;
    public AudioClip down;
    public AudioClip hum;
    public AudioClip moving;
    public List<AudioClip> DeflectSound;

    private Lightsaber_grab_interacteble lightsaber_Grab_Interacteble;
    public void ActiveOnOff()
    {
        active = !active;

        if(active) 
        {
            sourceprincipale.PlayOneShot(up);
        }
        else 
        {
            sourceprincipale.PlayOneShot(down);
        }

    }
    public void Start() 
    {
        lightsaber_Grab_Interacteble = GetComponent<Lightsaber_grab_interacteble>();
        sourceprincipale = gameObject.AddComponent<AudioSource>();
        sourcesecondaire = gameObject.AddComponent<AudioSource>();
        sourceprincipale.spatialBlend = 1 ;
        sourcesecondaire.spatialBlend = 1 ;
        foreach (Blade blade in blades) {
            blade.Showblade(false);
        }
    }

    public void Update()
    {
        if (active)
        {
            foreach (Blade blade in blades)
            {
                blade.Showblade(true);
                if (blade.blade.transform.localScale.y < blade.Get_Full_lenght().y)
                {
                    blade.blade.transform.localScale += new Vector3(0, 0.0005f, 0);
                }
                
            }
            Vector3 velocity = lightsaber_Grab_Interacteble.GetVelocity();

            //Debug.Log(velocity);
            if (!sourcesecondaire.isPlaying && velocity.magnitude > 6)
            {
                sourcesecondaire.PlayOneShot(moving);
            }
            else if (!sourceprincipale.isPlaying)
            {
                sourceprincipale.PlayOneShot(hum);
            }
            
        }
        else
        {
            foreach (Blade blade in blades)
            {
                if (blade.blade.transform.localScale.y > 0)
                {
                    blade.blade.transform.localScale += new Vector3(0, -0.0002f, 0);
                }

                if (blade.blade.transform.localScale.y <= 0)
                {
                    blade.Showblade(false);
                    sourceprincipale.Stop();
                }
            }
        }
    }

    internal void Deflect()
    {
        sourceprincipale.PlayOneShot(DeflectSound[Random.Range(0,3)]);
    }
}
