using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomerangHit : MonoBehaviour
{
    private bool boomerangIsShoot = false;
    private int damage = 11;
    private float knockback = 0.5f;
    private bool knockLeft = false;
    public GameObject boomerangController;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (boomerangIsShoot)
        {
            if (!knockLeft)
                knockback *= -1;
            if (collision.CompareTag("Piaf"))
                collision.GetComponent<PiafController>().GetDamage(damage, knockback);
            else if (collision.CompareTag("Ship"))
                collision.GetComponent<ShipController>().GetDamage(damage, knockback);
            else if (collision.CompareTag("Skeleton"))
                collision.GetComponent<SkeletonController>().GetDamage(damage, knockback);
            else if (collision.CompareTag("Zombie"))
                collision.GetComponent<Zombiecontroller>().GetDamage(damage, knockback);
            boomerangIsShoot = false;
            knockback = 0.5f;
            boomerangController.GetComponent<BoomerangController>().SetGoBack(true);
            boomerangController.GetComponent<BoomerangController>().SetBoomerangShoot(false);
        }
    }

    public void SetBoomerangIsShoot(bool val)
    {
        boomerangIsShoot=val;
    }
}
