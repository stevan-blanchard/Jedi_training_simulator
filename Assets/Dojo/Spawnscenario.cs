using Dreamteck.Splines;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Spawnscenario : MonoBehaviour
{
    public List<SplineComputer> spawnerlist;
    public float initialSpawnTimeInterval;
    public float minimalSpawnTimeInterval;
    public float decreaseInterval;

    public List<int> dificultyMultiplicator;
    public List<int> maxAlivePerDificulty;
    public List<int> countPerDifficulty;

    [SerializeField]
    public GameObject spawnobjectPrefab;

    public GameObject placemarker;
    public GameObject playzone;

    private int dificulty; //between 0 and 3; 
    private float waitBeforSpawn;
    private bool difficultyset;
    private int numofmark;
    private int totalMark;
    private bool player_placed;

    private AudioSource source;
    public AudioClip sound_zone;
    private bool soundplayed;

    public SplineComputer circlepath;


    public int bullet_received;
    // nombre de balles pouvant atteindre 
    // le joueur avant de relancer la partie
    private int admissible_bullets; 

    // Start is called before the first frame update
    void Start()
    {
        difficultyset = false;
        waitBeforSpawn = 0;
        numofmark = 0;
        pointsDeVie

        bullet_received = 0;
        admissible_bullets = 5;

        source = gameObject.AddComponent<AudioSource>();
        source.spatialBlend = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (placemarker.GetComponent<start_round>().playerIn)
        {
            placemarker.GetComponent<start_round>().playerIn = false;
            player_placed = true;
            totalMark = 0;
        }

        numofmark = GameObject.FindGameObjectsWithTag("Marksman").Length;

        if (difficultyset)
        {
            placemarker.SetActive(true);
        }
        if(totalMark >= countPerDifficulty[dificulty] && numofmark ==0) 
        {
            Win();
        }

        if(bullet_received >= admissible_bullets) {
            Debug.Log("Looooser");
            RestartGame();
        }


        if (player_placed) {
            Debug.Log("Placeeee");
            playzone.SetActive(true);
            if (!soundplayed) { source.PlayOneShot(sound_zone); soundplayed = true; }
            
            placemarker.SetActive(false);
            waitBeforSpawn -= Time.deltaTime;
            if (waitBeforSpawn > 0)
            {
                return;

            }
            if (numofmark < maxAlivePerDificulty[dificulty] && !(totalMark == countPerDifficulty[dificulty])) {
                SpawnMark();
                initialSpawnTimeInterval -= decreaseInterval;
                if (initialSpawnTimeInterval < minimalSpawnTimeInterval)
                { waitBeforSpawn = minimalSpawnTimeInterval; }
                else
                { waitBeforSpawn = initialSpawnTimeInterval; }
            }
        }
    }

    private void Win()
    {
        player_placed = false;
        difficultyset = false;
        playzone.SetActive(false);
        soundplayed = false;
    }

    public void InitialiseDificulty(int dificulty)
    {
        this.dificulty = dificulty;
        difficultyset = true;


    }

    public void SpawnMark()
    {
        GameObject newmark = Instantiate(spawnobjectPrefab);
        newmark.GetComponent<SplineFollower>().spline = spawnerlist[Random.Range(0, 2)];
        newmark.GetComponent<MarkAI>().circlepath = circlepath;
        totalMark++;
        }

    private void RestartGame()
    {
        bullet_received = 0;
        totalMark = 0;


        foreach (GameObject mark in GameObject.FindGameObjectsWithTag("Marksman")) {
            Destroy(mark);
        }
        player_placed = false;
        playzone.SetActive(false);
        difficultyset = false;
        placemarker.SetActive(false);

        waitBeforSpawn = initialSpawnTimeInterval;
    }
}
