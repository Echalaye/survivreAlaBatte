using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    public List<GameObject> inventoryObjectList = new List<GameObject>();
    private int posObject = 0;
    private int oldPosObject = 0;

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
    }
}
