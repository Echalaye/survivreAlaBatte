using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerSkeleton : MonoBehaviour
{
    public GameObject Skeleton;
    public int maxSkeletons = 2;

    private int currentSkeletons= 0;
    private GameObject playerCharacter;
    private bool canSpawnSkeleton = true;
    private double timeUntilSpawn = 10;
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

        if (currentSkeletons < maxSkeletons)
        {
            if (time > timeUntilSpawn)
            {
                GameObject spawnedSkeleton = Instantiate(Skeleton);
                spawnedSkeleton.transform.position = new Vector3(Random.Range(playerCharacter.transform.position.x - 20, playerCharacter.transform.position.x + 20), 1, 0);
                currentSkeletons++;
                time = 0;
            }

            time += Time.deltaTime;
        }
    }

}
