using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordController : MonoBehaviour
{
    public int damage = 15; // Dégâts infligés par l'épée
    public string enemyTag = "Mobs"; // Le tag des objets que l'épée peut endommager

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Vérifie si l'objet en collision a le tag ennemi
        if (collision.CompareTag(enemyTag))
        {
            // Ajoutez ce script à vos ennemis pour gérer leur vie et les dégâts subis
            MobHealth mobHealth = collision.GetComponent<MobHealth>();

            // Inflige des dégâts à l'ennemi si le script EnemyHealth est présent
            if (mobHealth != null)
            {
                mobHealth.TakeDamage(damage);
            }
        }
    }
}