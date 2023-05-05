using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerSkeleton : MonoBehaviour
{
    public GameObject Skeleton;
    public int maxSkeletons = 6;

    private int currentSkeletons= 0;
    private GameObject playerCharacter;
    private bool canSpawnSkeleton = false;
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
        if (canSpawnSkeleton)
        {
            currentSkeletons = GameObject.FindGameObjectsWithTag("Zombie").Length;
        }

        if (canSpawnSkeleton  && (currentSkeletons < maxSkeletons))
        {
            if (time > timeUntilSpawn)
            {
                GameObject spawnedZombie = Instantiate(Skeleton);
                spawnedZombie.transform.position = transform.position + new Vector3(Random.Range(playerCharacter.transform.position.x - 20, playerCharacter.transform.position.x + 20), 5, 0);
                currentSkeletons++;
                time = 0;
            }
            
            time += Time.deltaTime;
        }
        if (timeWaitDayPeriod > timeWaitUntilNight)
        {
            canSpawnSkeleton = !canSpawnSkeleton;
            timeWaitDayPeriod = 0;
        }
        Debug.Log("Time until night : " + timeWaitDayPeriod + "\ntime Spawn : " + time);
        timeWaitDayPeriod += Time.deltaTime;
    }

}
