using Dreamteck.Splines;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawnscenario : MonoBehaviour
{
    public List<SplineComputer> spawnerlist;
    public float initialSpawnTimeInterval;
    public float minimalSpawnTimeInterval;
    public float decreaseInterval;

    public List<int> dificultyMultiplicator;
    public List<int> maxAlivePerDificulty;

    [SerializeField]
    public GameObject spawnobjectPrefab;

    private int dificulty; //between 0 and 3; 
    private float waitBeforSpawn;
    private bool difficultyset;
    private int numofmark;

    // Start is called before the first frame update
    void Start()
    {
        difficultyset = false;
        waitBeforSpawn = 0;
        numofmark = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
        numofmark = GameObject.FindGameObjectsWithTag("Marksman").Length;
        

        if (difficultyset) {
            waitBeforSpawn -= Time.deltaTime;
            if (waitBeforSpawn > 0)
            {
                return;
            }
            if (numofmark < maxAlivePerDificulty[dificulty]) {
                SpawnMark();
                initialSpawnTimeInterval =- decreaseInterval;
                waitBeforSpawn = initialSpawnTimeInterval;
            }
        }

    }

    public void InitialiseDificulty(int dificulty)
    {
        this.dificulty = dificulty;
        difficultyset = true;


    }

    public void SpawnMark()
    {
        GameObject newmark = Instantiate(spawnobjectPrefab);
        newmark.GetComponent<SplineFollower>().spline = spawnerlist[Random.Range(0, 1)];
        }
}
