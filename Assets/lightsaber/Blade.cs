using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Blade : MonoBehaviour
{
    public GameObject blade;
    private Vector3 FullLenght = new (0.01f, 0.01f,0.01f);


    public void Start()
    {
        blade.transform.localScale = new (FullLenght.x, 0, FullLenght.z);
    }

    public Vector3 Get_Full_lenght()
    {
        return FullLenght;
    }

    public void Showblade(bool value)
    {
        blade.GetComponent<Renderer>().enabled = value;
    }
    public void Update()
    {
    }
}
