using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    GameObject Flock;
    flock newEnemy;
    public float health;
    void Start()
    {
        Flock = GameObject.FindGameObjectWithTag("flock");
        newEnemy = Flock.GetComponent<flock>();
        switch (gameObject.tag)
        {
            case "turret": health = 55; break;
            case "flockling": health = 20; break;
            case "Player": health = 50; break;
            case "Freighter": health = 200; break;
            case "Planet": health = 500; break;
            case "enemySprite": health = 20; break;
            default: health = 500; break;
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
                if (enemy.tag == "enemySprite")
                {
                    newEnemy.attackPlayer();
                }
               

                destroyShip.destroy(enemy, explosion);
            }
        }
    }
}
