using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeHit : MonoBehaviour
{

    private int damage = 25;
    private float knockback = 2f;
    private bool attOn = false;
    public GameObject stick;
    private bool knockLeft = false;

    public void setAttOn(bool val)
    {
        attOn = val;
    }

    public void setKnockLeft(bool val) { 
        knockLeft = val;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!knockLeft)
            knockback *= -1;
        
        stick.GetComponent<StickHit>().setAttOn(false);
        if (collision.CompareTag("Piaf"))
            collision.GetComponent<PiafController>().GetDamage(damage, knockback);
        else if (collision.CompareTag("Ship"))
            collision.GetComponent<ShipController>().GetDamage(damage, knockback);
        else if (collision.CompareTag("Skeleton"))
            collision.GetComponent<SkeletonController>().GetDamage(damage, knockback);
        else if (collision.CompareTag("Zombie"))
            collision.GetComponent<ZombieController>().GetDamage(damage, knockback);
        attOn= false;
    }
}
