using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerPiaf : MonoBehaviour
{
    public GameObject Piaf;
    public int maxPiafs = 6;

    private int currentPiafs = 0;
    private GameObject playerCharacter;
    private bool canSpawnPiaf = false;
    private double timeWaitUntilNight = 30.0;
    private double timeUntilSpawn = 10;
    private float timeWaitDayPeriod = 0;
    private float time = 0;

    // Start is called before the first frame update
    void Start()
    {
        playerCharacter = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (canSpawnPiaf)
        {
            currentPiafs = GameObject.FindGameObjectsWithTag("Piaf").Length;
        }

        if (canSpawnPiaf  && (currentPiafs < maxPiafs))
        {
            if (time > timeUntilSpawn)
            {
                GameObject spawnedPiaf = Instantiate(Piaf);
                spawnedPiaf.transform.position = transform.position + new Vector3(Random.Range(playerCharacter.transform.position.x - 20, playerCharacter.transform.position.x + 20), 5, 0);
                currentPiafs++;
                time = 0;
            }
            
            time += Time.deltaTime;
        }
        if (timeWaitDayPeriod > timeWaitUntilNight)
        {
            canSpawnPiaf = !canSpawnPiaf;
            timeWaitDayPeriod = 0;
        }
        Debug.Log("Time until night : " + timeWaitDayPeriod + "\ntime Spawn : " + time);
        timeWaitDayPeriod += Time.deltaTime;
    }

}
