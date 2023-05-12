using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerZombie : MonoBehaviour
{
    public GameObject Zombie;
    public int maxZombies = 6;

    private int currentZombies = 0;
    private GameObject playerCharacter;
    private bool canSpawnZombie = true;
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
        if (canSpawnZombie)
        {
            currentZombies = GameObject.FindGameObjectsWithTag("Zombie").Length;
        }

        if (currentZombies < maxZombies)
        {
            if (time > timeUntilSpawn)
            {
                GameObject spawnedZombie = Instantiate(Zombie);
                spawnedZombie.transform.position = new Vector3(Random.Range(playerCharacter.transform.position.x - 20, playerCharacter.transform.position.x + 20), 1, 0);
                currentZombies++;
                time = 0;
            }
            
            time += Time.deltaTime;
        }
    }

}
