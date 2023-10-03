using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.XR.Interaction.Toolkit;

public class MarkAI: MonoBehaviour
{
    public GameObject player;
    public GameObject target;
    public List<GameObject> Barrel_transform;
    public GameObject Bulet;

    private readonly int _reloadtime = 2;
    private readonly int _bulllet_speed = 500;
    private float shoottime;

    private readonly float _min = -0.25f;
    private readonly float _max = 0.25f;

    gestionAnim anim;
    // Start is called before the first frame update
    public void Start()
    {

        shoottime = _reloadtime;
        anim = GetComponent<gestionAnim>();
    }
    // Update is called once per frame
    void Update()
    {
        transform.LookAt(player.transform.position); //mark regardera toujour le joueur
        foreach (GameObject barrel in Barrel_transform) {//on ajoute un peut d'aléatoire a nos tir
            float xNoise = Random.Range(_min, _max);
            float yNoise = Random.Range(_min, _max);
            float zNoise = Random.Range(_min, _max);

            barrel.transform.LookAt(target.transform.position + new Vector3(xNoise,yNoise,zNoise));
        }
        ShootLV1();
    }

    public void ShootLV1() {
        shoottime -= Time.deltaTime;
        if (shoottime > 0) { return; }

        anim.Ready(true);
        if (!anim.canShoot) { return; } //attent l'animation
       

        int barrelshooter = (int)(Random.value * 3);
        anim.TirSound();
        GameObject bulletobj = Instantiate(Bulet, Barrel_transform[barrelshooter].transform.position,
            Barrel_transform[barrelshooter].transform.rotation) as GameObject;
        Rigidbody bulletrigid = bulletobj.GetComponent<Rigidbody>();
        bulletrigid.AddForce(bulletrigid.transform.forward * _bulllet_speed);
        Destroy(bulletobj, 10f);
        anim.Ready(false);

        shoottime = _reloadtime;
    }

    
}
