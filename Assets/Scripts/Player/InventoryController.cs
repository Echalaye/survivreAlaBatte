using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    public GameObject player;
    public List<GameObject> inventoryObjectList = new List<GameObject>();
    private int posObject = 0;
    private int oldPosObject = 0;
    private bool valLeftPlayer = false;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Mouse ScrollWheel") != 0f) // forward
        {
            oldPosObject = posObject;
            switch(Input.GetAxis("Mouse ScrollWheel") * 10)
            {
                case -1:
                    posObject += -1;
                    break;
                case 1:
                    posObject += 1;
                    break;
            }
            if(posObject < 0)
            {
                posObject = inventoryObjectList.Count - 1;
                
            }
            if(posObject > inventoryObjectList.Count - 1)
            {
                posObject = 0;
            }
            ChangeWeapon();
        }
    }

    public void ChangeWeapon()
    {
        inventoryObjectList[oldPosObject].SetActive(false);
        inventoryObjectList[posObject].SetActive(true);
        if(posObject == 0)
        {
            valLeftPlayer = player.GetComponent<Player>().GetGoLeft();
            inventoryObjectList[posObject].GetComponent<SwordController>().SetPosSword(valLeftPlayer);
        }
        else if(posObject == 1)
        {
            valLeftPlayer = player.GetComponent<Player>().GetGoLeft();
            inventoryObjectList[posObject].GetComponent<AxeController>().SetPosAxe(valLeftPlayer);
        }
        else if (posObject == 2)
        {
            valLeftPlayer = player.GetComponent<Player>().GetGoLeft();
            inventoryObjectList[posObject].GetComponent<BowController>().SetPosBow(valLeftPlayer);
        }
        else if (posObject == 3)
        {
            valLeftPlayer = player.GetComponent<Player>().GetGoLeft();
            inventoryObjectList[posObject].GetComponent<BoomerangController>().SetPosBoomerang(valLeftPlayer);
        }
        else if (posObject == 4)
        {
            valLeftPlayer = player.GetComponent<Player>().GetGoLeft();
            inventoryObjectList[posObject].GetComponent<FireController>().SetPosFire(valLeftPlayer);
        }
        else if (posObject == 5)
        {
            valLeftPlayer = player.GetComponent<Player>().GetGoLeft();
            inventoryObjectList[posObject].GetComponent<LevitationController>().SetPosLevitation(valLeftPlayer);
        }
        else if (posObject == 6)
        {
            valLeftPlayer = player.GetComponent<Player>().GetGoLeft();
            inventoryObjectList[posObject].GetComponent<BatController>().SetPosBat(valLeftPlayer);
        }
    }
}
