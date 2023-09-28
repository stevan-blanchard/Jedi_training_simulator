using System.Collections.Generic;
using Unity.XR.OpenVR;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class Lightsaber : MonoBehaviour
{
    public bool active = false;
    public Color blade_color;
    public List<Blade> blades; // base de(s) la lame(s) 
    private AudioSource source;
    public AudioClip up;
    public AudioClip down;
    public AudioClip hum;
    public AudioClip moving;
    Rigidbody rb;
    public void ActiveOnOff()
    {
        active = !active;

        if(active) {
            source.PlayOneShot(up);
        }
        else
        {
            source.PlayOneShot(down);
        }

    }

    public void Start()
    {
        //source.volume = 1f;
        rb = GetComponent<Rigidbody>();
        source = gameObject.AddComponent<AudioSource>();
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
            Debug.Log(rb.velocity);
            if (rb.velocity.magnitude > 6)
            {
                source.PlayOneShot(moving);
            }
            else if (!source.isPlaying)
            {
                source.PlayOneShot(hum);
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
                }
            }
        }
    }
}
