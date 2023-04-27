using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombiecontroller : MonoBehaviour
{
    private int health;
    private int damage;
    private GameObject targetPlayer;

    // Start is called before the first frame update
    void Start()
    {
        targetPlayer = GameObject.FindGameObjectWithTag("Player");

        if (targetPlayer == null)
        {
            Debug.Log("Aucun GameObject avec le tag 'Player' n'a été trouvé.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
