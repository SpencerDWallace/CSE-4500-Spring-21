using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class laserContact : MonoBehaviour
{
    GameObject collided;
    Rigidbody direction;
    public GameObject smallExplosion;
    public GameObject explosion;
    public GameObject bigExplosion;
    public GameObject planetExplosion;
    Health health;
    GameObject healthbar;
    GameObject playerHealth;
    GameObject expolsionPrefab;
    destroyEnemyShip destroyShip;
    RectTransform rect;
    // float time;
    void Start()
    {
        destroyShip = new destroyEnemyShip();
        healthbar = GameObject.Find("Enemy_healthbar");
        playerHealth = GameObject.Find("Player_healthbar");
        if(healthbar != null)
        rect = healthbar.GetComponent<RectTransform>();
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.name != "Player" && gameObject.tag != "enemyLaser" && col.gameObject.tag != "enemyLaser")
        {

            collided = col.gameObject;
            if (collided.tag == "enemySprite")
                Instantiate(smallExplosion, transform.position, transform.rotation);
            else
                Instantiate(explosion, transform.position, transform.rotation);



            if (collided.tag == "planetMesh")
                collided = collided.transform.parent.gameObject;

            direction = GetComponent<Rigidbody>();
            direction.velocity = 0 * transform.forward;
            UnityEngine.Object.Destroy(gameObject);



            switch (collided.tag)
            {
                case "Freighter": expolsionPrefab = bigExplosion; break;
                case "Planet": expolsionPrefab = planetExplosion; break;
                case "enemySprite": expolsionPrefab = explosion; break;
                default: break;
            }
            if (expolsionPrefab != null)
            {
                health = collided.GetComponent<Health>();
                health.takeDamage(collided, destroyShip, expolsionPrefab);
            }

            //            if(collided.tag == "Freighter")
            rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, collided.GetComponent<Health>().health);



        }

        if (col.gameObject.tag == "Player" && gameObject.tag == "enemyLaser")
        {
            collided = col.gameObject;
            health = collided.GetComponent<Health>();
            health.takeDamage(collided, destroyShip, smallExplosion);
            Instantiate(smallExplosion, transform.position - transform.forward*2, transform.rotation);
            direction = GetComponent<Rigidbody>();
            direction.velocity = 0 * transform.forward;
            UnityEngine.Object.Destroy(gameObject);
            rect = playerHealth.GetComponent<RectTransform>();
            rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, collided.GetComponent<Health>().health);
        }
    }

}
