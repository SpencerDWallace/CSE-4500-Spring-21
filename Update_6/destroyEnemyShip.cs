using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class destroyEnemyShip
{

    public destroyEnemyShip() { }

    public void destroy(GameObject toBeDestroyed, GameObject Explosion)
    {
        GameObject.Instantiate(Explosion, toBeDestroyed.transform.position, toBeDestroyed.transform.rotation);
        if (toBeDestroyed.tag != "Player")
        {
            UnityEngine.Object.Destroy(toBeDestroyed);
        }
    }
}
    
