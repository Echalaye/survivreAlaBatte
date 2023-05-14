using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DayNightCycleController : MonoBehaviour
{
    public GameObject spawnerZombie;
    public GameObject spawnerSkeleton;
    public GameObject spawnerPiaf;
    public GameObject spawnerShip;
    public GameObject textDay;
    public GameObject textNight;
    private double time;
    private double cycleTime = 60;
    private bool day = true;

    // Update is called once per frame
    void Update()
    {
        if(time >= cycleTime)
        {
            time = 0;
            if (day)
            {
                day= false;
                spawnerZombie.SetActive(true);
                spawnerSkeleton.SetActive(true);
                spawnerPiaf.SetActive(false);
                textDay.SetActive(false);
                textNight.SetActive(true);
            }
            else {
                day = true;
                spawnerZombie.SetActive(false);
                spawnerSkeleton.SetActive(false);
                spawnerPiaf.SetActive(true);
                textDay.SetActive(true);
                textNight.SetActive(false);
            }
            DestroyAllActor();

        }

        time += Time.deltaTime;
    }

    public void DestroyAllActor()
    {
        if (!day)
        {
            var listPiaf = GameObject.FindGameObjectsWithTag("Piaf");
            for (int i = 0; i < listPiaf.Count(); i++)
            {
                Destroy(listPiaf[i].gameObject);
            }
        }
        else
        {
            var listZombie = GameObject.FindGameObjectsWithTag("Zombie");
            var listSkeleton = GameObject.FindGameObjectsWithTag("Skeleton");
            for (int i = 0; i < listZombie.Count(); i++)
            {
                Destroy(listZombie[i].gameObject) ;
            }
            for (int i = 0; i < listSkeleton.Count(); i++)
            {
                Destroy(listSkeleton[i].gameObject);
            }
        }

    }

}
