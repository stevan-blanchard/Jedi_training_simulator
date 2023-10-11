using Dreamteck.Splines;
using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.XR.Interaction.Toolkit;
using static Unity.VisualScripting.Member;

public class MarkAI : MonoBehaviour
{
    private GameObject target;
    public List<GameObject> Barrel_transform;
    public GameObject Bulet;


    private readonly int _reloadtime = 1;
    public int bulllet_speed;
    private float shoottime;

    private readonly float _min = -0.25f;
    private readonly float _max = 0.25f;

    private bool ismoving;
    private bool speedAffected = false;
    private int movingspeed;
    private float movingtime;

    private gestionAnim anim;
    private SplineFollower follower;
    private bool waitbeforeremoving;
    private float waitbeforeremovingtime = 2;
    private float waitbeforeremovingtimeremaining;

    public ParticleSystem explosion;
    private bool isdead;
    private bool markready;

    // Start is called before the first frame update
    public void Awake()
    {
        this.target = GameObject.Find("Target");
        ismoving = true;
        waitbeforeremoving = true;
        shoottime = _reloadtime;
        anim = GetComponent<gestionAnim>();
        follower = GetComponent<SplineFollower>();
    }
    // Update is called once per frame
    void Update()
    {

        if (!markready)
        {
            return;
        }
        if (!isdead)
        {
            gameObject.transform.LookAt(target.transform.position); //mark regardera toujour le joueur

            foreach (GameObject barrel in Barrel_transform)
            {//on ajoute un peut d'aléatoire a nos tir
                float xNoise = Random.Range(_min, _max);
                float yNoise = Random.Range(_min, _max);
                float zNoise = Random.Range(_min, _max);

                barrel.transform.LookAt(target.transform.position + new Vector3(xNoise, yNoise, zNoise));
            }


            if (ismoving)
            {
                MovingPhase();

            }
            else if (waitbeforeremoving)
            {
                waitbeforeremovingtimeremaining -= Time.deltaTime;
                if (waitbeforeremovingtimeremaining > 0)
                {
                    return;
                }
                else
                {
                    waitbeforeremoving = false;
                    ismoving = true;
                    waitbeforeremovingtimeremaining = waitbeforeremovingtime;
                }
            }
            else
            {
                ShootLV1();


            }
        }

    }

    private void MovingPhase()
    {


        if (!speedAffected)
        {
            movingspeed = Random.Range(-2, 2);
            while (movingspeed == 0)
            { // on evite d'avoir une speed de 0
                movingspeed = Random.Range(-2, 2);
            }

            anim.MovSound();
            movingtime = Random.Range(2, 5);
            speedAffected = true;
            follower.followSpeed = movingspeed;
        }
        movingtime -= Time.deltaTime;
        if (movingtime > 0) { return; }
        else
        {

            follower.followSpeed = 0;
            ismoving = false;
            speedAffected = false;
        }

    }

    public void ShootLV1()
    {
        shoottime -= Time.deltaTime;
        if (shoottime > 0) { return; }

        anim.Ready(true);
        if (!anim.canShoot) { return; } //attent l'animation


        int barrelshooter = (int)(Random.value * 3);
        anim.TirSound();
        GameObject bulletobj = Instantiate(Bulet, Barrel_transform[barrelshooter].transform.position,
            Barrel_transform[barrelshooter].transform.rotation) as GameObject;
        bulletobj.GetComponent<bullet_script>().sender = gameObject;
        Rigidbody bulletrigid = bulletobj.GetComponent<Rigidbody>();
        bulletrigid.AddForce(bulletrigid.transform.forward * bulllet_speed);
        Destroy(bulletobj, 10f);


        anim.Ready(false);
        waitbeforeremoving = true;
        shoottime = _reloadtime;
    }

    public void SetIsMoving(bool ismoving)
    {
        this.ismoving = ismoving;
    }

    public void ready()
    {
        markready = true;

    }

    public void Explose()
    {
        Instantiate(explosion, transform.position, Quaternion.identity);
        explosion.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        explosion.Emit(100);
        follower.enabled = false;
        isdead = true;
        gameObject.GetComponent<Rigidbody>().useGravity = true;
        Destroy(gameObject, 3f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            Debug.Log("Mark touché");
            Explose();
            Destroy(other.gameObject);
            anim.DieSound();
        }
        else if (other.gameObject.CompareTag("Lightsaber")) {
            Debug.Log("Mark touché");
            Explose();
            anim.DieSound();
        }
    }
}
