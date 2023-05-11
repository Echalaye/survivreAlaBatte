using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickHit : MonoBehaviour
{
    private int damage = 5;
    private float knockback = 0f;
    private bool attOn = false;
    public GameObject axe;

    public void setAttOn(bool val)
    {
        attOn = val;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        axe.GetComponent<AxeHit>().setAttOn(false);
        if (!collision.CompareTag("Ground") && attOn)
        {
            
            if (collision.CompareTag("Piaf"))
                collision.GetComponent<PiafController>().GetDamage(damage, knockback);
            else if (collision.CompareTag("Ship"))
                collision.GetComponent<ShipController>().GetDamage(damage, knockback);
            else if (collision.CompareTag("Skeleton"))
                collision.GetComponent<SkeletonController>().GetDamage(damage, knockback);
            else if (collision.CompareTag("Zombie"))
                collision.GetComponent<Zombiecontroller>().GetDamage(damage, knockback);
            attOn= false;
        }
    }
}
