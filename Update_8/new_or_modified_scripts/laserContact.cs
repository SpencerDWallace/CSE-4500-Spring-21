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
        expolsionPrefab = null;
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
            if (collided.tag == "enemySprite" || collided.tag == "flockling")
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
                case "turret": expolsionPrefab = explosion; break;
                case "Freighter": expolsionPrefab = bigExplosion; break;
                case "Planet": expolsionPrefab = planetExplosion; break;
                case "enemySprite": expolsionPrefab = explosion; break;
                case "flockling": expolsionPrefab = explosion; break;
                default: expolsionPrefab = smallExplosion; break;
            }
            if (expolsionPrefab != null)
            {
                health = collided.GetComponent<Health>();
                health.takeDamage(collided, destroyShip, expolsionPrefab);
            }

            //            if(collided.tag == "Freighter")
            if (collided != null && collided.GetComponent<Health>().health < 201)
                rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, collided.GetComponent<Health>().health);
            else
                rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 0f);


        }

        if (col.gameObject.tag == "Player" && gameObject.tag == "enemyLaser")
        {
            collided = col.gameObject;
            health = collided.GetComponent<Health>();
            health.takeDamage(collided, destroyShip, smallExplosion);
            Instantiate(smallExplosion, transform.position - transform.forward*2 + collided.transform.forward*1.7F, transform.rotation);
            direction = GetComponent<Rigidbody>();
            direction.velocity = 0 * transform.forward;
            UnityEngine.Object.Destroy(gameObject);
            rect = playerHealth.GetComponent<RectTransform>();
            rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, collided.GetComponent<Health>().health*4);
        }
    }

}
