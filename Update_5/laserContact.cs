using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class laserContact : MonoBehaviour
{
    GameObject collided;
    Rigidbody direction;
    public GameObject explosion;
    public GameObject bigExplosion;
    public GameObject planetExplosion;
    
    GameObject healthbar;
    GameObject expolsionPrefab;
    destroyEnemyShip destroyShip;
    RectTransform rect;
    // float time;
    void Start()
    {
        destroyShip = new destroyEnemyShip();
        healthbar = GameObject.Find("Enemy_healthbar");
        rect = healthbar.GetComponent<RectTransform>();
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.name != "Player")
        {
            collided = col.gameObject;
            direction = GetComponent<Rigidbody>();
            direction.velocity = 0 * transform.forward;

            expolsionPrefab = Instantiate(explosion, transform.position, transform.rotation);
           // Debug.Log("Hit!");
            UnityEngine.Object.Destroy(gameObject);
            rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, rect.rect.width - 20);
            if(rect.rect.width <= 0)
            {
                if (destroyShip == null)
                    Debug.Log("ship is null");
                else
                {
                    if (collided.name == "Freighter")
                        destroyShip.destroy(collided, bigExplosion);
                    else if(collided.transform.parent.gameObject.name == "planet")
                        destroyShip.destroy(collided.transform.parent.gameObject, planetExplosion);                    
                    else
                        destroyShip.destroy(collided, explosion);

                }
                }


        }
    }

    /*
    // Update is called once per frame
    void Update()
    {
        
    }
    */
}
