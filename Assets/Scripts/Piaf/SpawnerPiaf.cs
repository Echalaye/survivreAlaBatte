using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerPiaf : MonoBehaviour
{
    public GameObject Piaf;
    public int maxPiafs = 6;

    private int currentPiafs = 0;
    private GameObject playerCharacter;
    private bool canSpawnPiaf = true;
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
        if (canSpawnPiaf)
        {
            currentPiafs = GameObject.FindGameObjectsWithTag("Zombie").Length;
        }

        if (currentPiafs < maxPiafs)
        {
            if (time > timeUntilSpawn)
            {
                GameObject spawnedPiaf = Instantiate(Piaf);
                spawnedPiaf.transform.position = new Vector3(Random.Range(playerCharacter.transform.position.x - 20, playerCharacter.transform.position.x + 20), Random.Range(5, 20), 0);
                currentPiafs++;
                time = 0;
            }

            time += Time.deltaTime;
        }
    }

}
