using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreighterBehavior : MonoBehaviour
{
    GameObject temp;
    Rigidbody direction;
    public GameObject explosion;
    GameObject expolsionPrefab;
   // float time;
    private void OnTriggerEnter(Collider col)
    {
       // Debug.Log("Made it");
        //if (col.gameObject.tag == "Player")
        if (col.gameObject.name == "RedLaser(Clone)")
        {
            
            temp = col.gameObject;
            direction = col.GetComponent<Rigidbody>();
            direction.velocity = 0 * temp.transform.forward;

            expolsionPrefab = Instantiate(explosion, temp.transform.position, temp.transform.rotation);
            Debug.Log("Hit!");
        }
    }
}
