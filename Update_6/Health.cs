using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float health;
    void Start()
    {
        switch (gameObject.tag)
        {
            case "Player": health = 20; break;
            case "Freighter": health = 200; break;
            case "Planet": health = 90; break;
            case "enemySprite": health = 20; break;
            default: health = 10000; break;
        }
    }

    public void takeDamage(GameObject enemy, destroyEnemyShip destroyShip, GameObject explosion)
    {
        if (health != 10000)
        {
            if (health > 0)
                health -= 5;

            if (health <= 0)
            {
                destroyShip.destroy(enemy, explosion);
            }
        }
    }
}
