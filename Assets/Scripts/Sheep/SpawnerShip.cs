using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerShip : MonoBehaviour
{
    public GameObject Ship;
    public int maxShips = 6;

    private int currentShips = 0;
    private GameObject playerCharacter;
    private bool canSpawnShip = true;
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
        if (canSpawnShip)
        {
            currentShips = GameObject.FindGameObjectsWithTag("Zombie").Length;
        }

        if (currentShips < maxShips)
        {
            if (time > timeUntilSpawn)
            {
                GameObject spawnedShip = Instantiate(Ship);
                spawnedShip.transform.position = new Vector3(Random.Range(playerCharacter.transform.position.x - 20, playerCharacter.transform.position.x), 1, 0);
                currentShips++;
                time = 0;
            }

            time += Time.deltaTime;
        }
    }
}
